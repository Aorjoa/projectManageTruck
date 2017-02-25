/* This project is under development
 * All rights reserved © Bhuridech Sudsee
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteConnection dbManager;
        String recorder = "";
        public MainWindow()
        {
            connectDb();
            InitializeComponent();
        }

        private void payPartly_Checked(object sender, RoutedEventArgs e)
        {
            payPartlyPrice.IsEnabled = true;
        }

        private void payPart_Checked(object sender, RoutedEventArgs e)
        {
            //set input pay partly enable only selected partly
            if (payPartlyPrice != null)
            {
                payPartlyPrice.IsEnabled = false;
                payPartlyPrice.Clear();
            }
        }

        private void payFull_Checked(object sender, RoutedEventArgs e)
        {
            //set input pay partly enable only selected partly
            if (payPartlyPrice != null)
            {
                payPartlyPrice.IsEnabled = false;
                payPartlyPrice.Clear();
            }
        }

        private void calPrice_Click(object sender, RoutedEventArgs e)
        {
            payPrice.Text = calculatePrice().ToString();
        }

        private int calculatePrice()
        {
            int calculatePrice = 0;
            try
            {
                int ihvArea = int.Parse(hvArea.Text);
                int ihvPriceArea = int.Parse(hvPriceArea.Text);
                int ibhHours = int.Parse(bhHours.Text);
                int ibhPriceHours = int.Parse(bhPriceHours.Text);
                int itrNum = int.Parse(trNum.Text);
                int itrPriceNum = int.Parse(trPriceNum.Text);
                calculatePrice = ((ihvArea * ihvPriceArea) + (ibhHours * ibhPriceHours) + (itrNum * itrPriceNum));

            }
            catch (FormatException ex)
            {
                MessageBox.Show("กรุณากรอกข้อมูลที่ถูกต้อง", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
                payPrice.Clear();
            }
            return calculatePrice;
        }


        private void connectDb()
        {
            //Initial
            if (!File.Exists("db.sqlite"))
            {
                SQLiteConnection.CreateFile("db.sqlite");
            }
            dbManager = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            dbManager.Open();
            string createTableRecord = "create table if not exists records (ctId text, ctName text, ctPhone text, ctAddress text, dateOp text, hvName text, hvArea int, hvPriceArea int, bhName text, bhHours int, bhPriceHours int, trName text, trNum int, trPriceNum int, price int, pay int,recorder text)";
            SQLiteCommand command = new SQLiteCommand(createTableRecord, dbManager);
            command.ExecuteNonQuery();
            string createTableMember = "create table if not exists members (mbUsername text, mbName text, mbPassword text, unique (mbUsername))";
            command = new SQLiteCommand(createTableMember, dbManager);
            command.ExecuteNonQuery();

            //default password admin, OGoUIAHGBK
            string createAdmin = "insert or ignore into members (mbUsername, mbName, mbPassword) values ('admin','admin','0B143597BAC95DA7BF4696A41D8C19CA')";
            command = new SQLiteCommand(createAdmin, dbManager);
            command.ExecuteNonQuery();

        }

        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            //validate field
            if (recorder == "") { return; }
            if (calculatePrice() < 1) { return; }
            if (dateOp.Text.Length < 1) { MessageBox.Show("กรุณาตรวจสอบวันที่", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            try
            {
                int paid = 0;
                if (payPartly.IsChecked.Value) { int.TryParse(payPartlyPrice.Text, out paid); }
                if (payFull.IsChecked.Value) { int.TryParse(payPrice.Text, out paid); }
                string sql = genInsertSql(new String[] { ctId.Text, ctName.Text, ctPhone.Text, ctAddress.Text, dateOp.Text, hvName.Text, hvArea.Text, hvPriceArea.Text, bhName.Text, bhHours.Text, bhPriceHours.Text, trName.Text, trNum.Text, trPriceNum.Text, payPrice.Text, paid.ToString(), recorder });
                SQLiteCommand command = new SQLiteCommand(sql, dbManager);
                command.ExecuteNonQuery();
                MessageBox.Show("บันทึกข้อมูลของสมาชิก " + ctId.Text + " : " + ctName.Text + " เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("กรุณากรอกข้อมูลที่ถูกต้อง", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private String genInsertSql(String[] str)
        {
            String init = "insert into records (ctId, ctName, ctPhone, ctAddress, dateOp, hvName, hvArea, hvPriceArea, bhName, bhHours, bhPriceHours, trName, trNum , trPriceNum, price, pay, recorder) values (";

            foreach (String element in str)
            {
                int tryParse = 0;
                if (int.TryParse(element, out tryParse))
                {
                    init += element + ",";
                }
                else
                {
                    init += "\"" + element + "\",";
                }
            }
            init = (init.Remove(init.Length - 1)) + ")";

            return init;
        }

        private void searchRecord_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(dbManager);
            sw.Show();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            String password = createMD5(passwordInput.Password);
            string foundMember = "select count(*) from members where mbUsername=='" + usernameInput.Text + "' and mbPassword=='" + password + "'";
            SQLiteCommand cmd = new SQLiteCommand(foundMember,dbManager);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if ( count < 1) { MessageBox.Show("ไม่พบข้อมูลผู้ใช้งาน!", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            recorder = usernameInput.Text;
            userNameShow.Content = recorder;
            addRecord.IsEnabled = true;
            searchRecord.IsEnabled = true;
            login.Visibility = Visibility.Hidden;
        }

        private static string createMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
