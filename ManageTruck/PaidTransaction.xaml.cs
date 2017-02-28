using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for PaidTransaction.xaml
    /// </summary>
    public partial class PaidTransaction : Window
    {
        SQLiteConnection dbManager;
        string recordId = "";
        string recorder = "";
        public PaidTransaction(SQLiteConnection conn,string _recorder)
        {
            dbManager = conn;
            recorder = _recorder;
            InitializeComponent();
        }

        private void searchRecord_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = new DataSet();
            string query = "select ROWID as เลขที่ชำระเงิน, recordId as เลขที่รายการ, pay as จำนวนเงินที่จ่าย, recorder as คนบันทึก from transactions where recordId like '%" + inputRecordId.Text + "%'";
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, dbManager);
            dataAdapter.Fill(dataSet);
            dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;

            recordId = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            if(recordId != "")
            {
                lblRecoardId.Content = recordId;
            }
            query = "select((sum(price) / count(price)) - sum(pay)) from(select recordId, price, pay from records left join transactions on records.ROWID = transactions.recordId) where recordId = '"+recordId+"' group by recordId";
            DataSet remainSet = new DataSet();
            dataAdapter = new SQLiteDataAdapter(query, dbManager);
            dataAdapter.Fill(remainSet);
            string remainCost = remainSet.Tables[0].Rows[0].ItemArray[0].ToString();
            if (remainCost != "")
            {
                lblRemainCost.Content = remainCost;
            }
        }

        private void payMore_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (recordId != "" && recorder != "") {
                    string sqlAddPaidTransactions = "insert into transactions (recordId,pay,recorder) values ('" + recordId + "'," + inputPaidAmont.Text + ",'" +recorder+"')";
                    SQLiteCommand command = new SQLiteCommand(sqlAddPaidTransactions, dbManager);
                    command.ExecuteNonQuery();
                    MessageBox.Show("เพิ่มรายการชำระเงินเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("กรุณากรอกข้อมูลที่ถูกต้อง", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            searchRecord_Click(sender, e);
        }
    }
}
