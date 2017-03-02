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
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        SQLiteConnection dbManager;
        public SearchWindow(SQLiteConnection conn)
        {
            dbManager = conn;
            InitializeComponent();
        }

        private void seachNow_Click(object sender, RoutedEventArgs e)
        {

            DataSet dataSet = new DataSet();
            string query = "select ROWID as เลขที่รายการ, ctId as รหัสลูกค้า, ctName as ชื่อลูกค้า, ctPhone as เบอร์โทร, ctAddress as ที่อยู่, dateOp as วันเวลา, hvName as คนเกี่ยว, hvArea as จำนวนไร่, hvPriceArea as ไร่ละ, hvAddress as เกี่ยวที่, bhName as คนขับแบคโฮ, bhHours as ชั่วโมว, bhPriceHours as ชั่วโมงละ, trName as คนขับสิบล้อ, trNum as จำนวนรอบ, trPriceNum as เกรียนละ, ttName as คนขับแทรกเตอร์, ttNum as _จำนวนรอบ, ttPriceNum as _เกรียนละ, price as ราคารวม, recorder as คนบันทึก from records where ctId like '%" + ctIdSearch.Text+"%' and ctName like '%"+ ctNameSearch.Text + "%'";

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, dbManager);
            dataAdapter.Fill(dataSet);
            dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void showRecord_Click(object sender, RoutedEventArgs e)
        {
                object[] selected = ((System.Data.DataRowView)dataGrid.SelectedItem).Row.ItemArray;
                Report rp = new Report(selected);
                rp.Show();
           
        }
    }
}
