using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    /// 

    public partial class Report : Window
    {
        object[] dataRow;
        public Report(object[] dataRow)
        {
            this.dataRow = dataRow;
            InitializeComponent();
            recordId.Text = dataRow[0].ToString();
            ctId.Text = dataRow[1].ToString();
            ctName.Text = dataRow[2].ToString();
            ctPhone.Text = dataRow[3].ToString();
            ctAddress.Text = dataRow[4].ToString();
            dateOp.SelectedDate = DateTime.Parse(dataRow[5].ToString());
            hvName.Text = dataRow[6].ToString();
            hvArea.Text = dataRow[7].ToString();
            hvPriceArea.Text = dataRow[8].ToString();
            hvAddress.Text = dataRow[9].ToString();
            bhName.Text = dataRow[10].ToString();
            bhHours.Text = dataRow[11].ToString();
            bhPriceHours.Text = dataRow[12].ToString();
            trName.Text = dataRow[13].ToString();
            trNum.Text = dataRow[14].ToString();
            trPriceNum.Text = dataRow[15].ToString();
            ttName.Text = dataRow[16].ToString();
            ttNum.Text = dataRow[17].ToString();
            ttPriceNum.Text = dataRow[18].ToString();
            payPrice.Text = dataRow[19].ToString();
            recorder.Text = dataRow[20].ToString();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            print.Visibility = Visibility.Hidden;
            close.Visibility = Visibility.Hidden;
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Print Report");
            }
            print.Visibility = Visibility.Visible;
            close.Visibility = Visibility.Visible;
        }
    }
}
