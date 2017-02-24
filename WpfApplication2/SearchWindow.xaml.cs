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
            string query = "select * from records where ctId like '%"+ctIdSearch.Text+"%' and ctName like '%"+ ctNameSearch.Text + "%'";

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, dbManager);
            dataAdapter.Fill(dataSet);
            dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
        }
    }
}
