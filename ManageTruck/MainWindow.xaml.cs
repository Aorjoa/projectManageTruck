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
using System.Management;
using SharpAdbClient;
using System.Data;

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
            checkComputerUsage();
            InitializeComponent();
        }

        private void checkComputerUsage()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"].ToString();
            }

            File.AppendAllText(@"snd.dll", "");
            string[] sn = File.ReadLines(@"snd.dll").ToArray();

                int pos = Array.IndexOf(sn, id);
                if (pos < 0)
                {
                    if (sn.Length < 3)
                    {
                        File.AppendAllText(@"snd.dll", id + "\n");
                    }else
                    {
                        this.Close();
                    }
                }
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
                int ittNum = int.Parse(ttNum.Text);
                int ittPriceNum = int.Parse(ttPriceNum.Text);
                calculatePrice = ((ihvArea * ihvPriceArea) + (ibhHours * ibhPriceHours) + (itrNum * itrPriceNum) + (ittNum * ittPriceNum));
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
            string createTableRecord = "create table if not exists records (recordId text, checkSync int, ctId text, ctName text, ctPhone text, ctAddress text, dateOp text, hvName text, hvArea int, hvPriceArea int, hvAddress text, bhName text, bhHours int, bhPriceHours int, trName text, trNum int, trPriceNum int, ttName text, ttNum int, ttPriceNum int, price int, recorder text)";
            SQLiteCommand command = new SQLiteCommand(createTableRecord, dbManager);
            command.ExecuteNonQuery();

            string addFirstRowForTest = "insert into records (recordId) select ('') where not exists (select * from records)";
            command = new SQLiteCommand(addFirstRowForTest, dbManager);
            command.ExecuteNonQuery();
            
            string createTableTransaction = "create table if not exists transactions (recordId text, checkSync int, pay int, recordDate text, recorder text)";
            command = new SQLiteCommand(createTableTransaction, dbManager);
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
                string sqlAddRecord = genInsertSql(new String[] { ctId.Text, ctName.Text, ctPhone.Text, ctAddress.Text, dateOp.Text, hvName.Text, hvArea.Text, hvPriceArea.Text, hvAddress.Text, bhName.Text, bhHours.Text, bhPriceHours.Text, trName.Text, trNum.Text, trPriceNum.Text, ttName.Text, ttNum.Text, ttPriceNum.Text, payPrice.Text, recorder });
                SQLiteCommand command = new SQLiteCommand(sqlAddRecord, dbManager);
                command.ExecuteNonQuery();
                string sqlAddPaidTransactions = "insert into transactions (recordId,checkSync,pay,recordDate,recorder) select recordId,1," + paid.ToString() + ",dateOp,'" + recorder + "' from records order by ROWID desc limit 1";
                command = new SQLiteCommand(sqlAddPaidTransactions, dbManager);
                command.ExecuteNonQuery();
                
                MessageBox.Show("บันทึกข้อมูลของสมาชิก " + ctId.Text + " : " + ctName.Text + " เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButton.OK, MessageBoxImage.Information);
                clearInput();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("กรุณากรอกข้อมูลที่ถูกต้อง", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void clearInput()
        {
            ctId.Clear();
            ctName.Clear();
            ctPhone.Clear();
            ctAddress.Clear();
            dateOp.SelectedDate = null;

            hvName.Clear();
            hvArea.Clear();
            hvPriceArea.Clear();
            hvAddress.Clear();

            bhName.Clear();
            bhHours.Clear();
            bhPriceHours.Clear();

            trName.Clear();
            trNum.Clear();
            trPriceNum.Clear();

            ttName.Clear();
            ttNum.Clear();
            ttPriceNum.Clear();
        }

        private String genInsertSql(String[] str)
        {
            String init = "insert into records (recordId, checkSync, ctId, ctName, ctPhone, ctAddress, dateOp, hvName, hvArea, hvPriceArea, hvAddress, bhName, bhHours, bhPriceHours, trName, trNum , trPriceNum, ttName, ttNum, ttPriceNum, price, recorder) select printf('PC%s',ROWID),1,";

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
            init = (init.Remove(init.Length - 1)) + " from records order by ROWID desc limit 1";

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
            if ( count < 1) { MessageBox.Show("ไม่พบข้อมูลผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง!", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            recorder = usernameInput.Text;
            userNameShow.Content = recorder;
            addRecord.IsEnabled = true;
            searchRecord.IsEnabled = true;
            addMember.IsEnabled = true;
            paid.IsEnabled = true;
            login.Visibility = Visibility.Hidden;
        }

        public static string createMD5(string input)
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

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            Members m = new Members(dbManager);
            m.Show();
        }

        private void paid_Click(object sender, RoutedEventArgs e)
        {
            PaidTransaction p = new PaidTransaction(dbManager,recorder);
            p.Show();
        }

        private void syncBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ADBClass adb = new ADBClass();
                adb.getServer();
                if (adb.downloadFile() && File.Exists(@"sqlite.db.m"))
                {
                    SQLiteConnection dbManagerOnMobile = new SQLiteConnection("Data Source=sqlite.db.m;Version=3;");
                    dbManagerOnMobile.Open();
                    syncRecords(dbManagerOnMobile);
                    syncTransaction(dbManagerOnMobile);
                    if (adb.uploadFile())
                    {
                        //string dirpath = Directory.GetCurrentDirectory();
                        //File.Copy(dirpath+"\\sqlite.db.m", dirpath+"\\sqlite.db.m.bak",true);
                        MessageBox.Show("โอนข้อมูลเรียบร้อย", "สำเร็จ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }else
                    {
                        MessageBox.Show("ไม่สามารถอัพโหลดได้", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception xe)
            {
                MessageBox.Show("พบปัญหาระหว่างโอนถ่ายข้อมูล : " + xe.Message , "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void syncTransaction(SQLiteConnection dbManagerOnMobile)
        {
            DataSet dataSet = new DataSet();
            string query = "select * from transactions where checkSync = '0'";

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, dbManagerOnMobile);
            dataAdapter.Fill(dataSet);
            DataRowCollection rows = dataSet.Tables[0].Rows;
            int colCount = dataSet.Tables[0].Columns.Count;
            SQLiteCommand command = new SQLiteCommand("", dbManager);
            foreach (DataRow row in rows)
            {
                String init = "insert into transactions (recordId,checkSync,pay,recordDate,recorder) values (";
                for (int i = 0; i < colCount; i++)
                {
                    if (i == 1)
                    {
                        init += "\"1\",";
                    }
                    else
                    {
                        init += "\"" + row[i] + "\",";
                    }
                }
                init = (init.Remove(init.Length - 1)) + ")";
                command = new SQLiteCommand(init, dbManager);
                command.ExecuteNonQuery();
            }
        }

        private void syncRecords(SQLiteConnection dbManagerOnMobile)
        {
            DataSet dataSet = new DataSet();
            string query = "select * from records where checkSync = '0'";

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, dbManagerOnMobile);
            dataAdapter.Fill(dataSet);
            DataRowCollection rows = dataSet.Tables[0].Rows;
            int colCount = dataSet.Tables[0].Columns.Count;
            SQLiteCommand command = new SQLiteCommand("", dbManager);
            foreach (DataRow row in rows)
            {
                String init = "insert into records (recordId, checkSync, ctId, ctName, ctPhone, ctAddress, dateOp, hvName, hvArea, hvPriceArea, hvAddress, bhName, bhHours, bhPriceHours, trName, trNum , trPriceNum, ttName, ttNum, ttPriceNum, price, recorder) values (";
                for (int i = 0; i < colCount; i++)
                {
                    if (i == 1)
                    {
                        init += "\"1\",";
                    }
                    else
                    {
                        init += "\"" + row[i] + "\",";
                    }

                }
                init = (init.Remove(init.Length - 1)) + ")";
                command = new SQLiteCommand(init, dbManager);
                command.ExecuteNonQuery();
            }
        }
    }
}