using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Members.xaml
    /// </summary>
    public partial class Members : Window
    {
        SQLiteConnection dbManager;
        public Members(SQLiteConnection conn)
        {
            dbManager = conn;
            InitializeComponent();
        }

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = MainWindow.createMD5(passwordAdd.Password);
                string createMember = "insert or ignore into members (mbUsername, mbName, mbPassword) values ('" + usernameAdd.Text + "','" + nameAdd.Text + "','" + password + "')";
                SQLiteCommand command = new SQLiteCommand(createMember, dbManager);
                command.ExecuteNonQuery();
                MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("การบันทึกข้อมูลผิดพลาดหรือข้อมูลมีอยู่แล้ว", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
