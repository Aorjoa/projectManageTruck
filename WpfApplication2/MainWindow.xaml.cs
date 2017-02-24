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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

         private void payPartly_Checked(object sender, RoutedEventArgs e)
        {
            payPartlyPrice.IsEnabled = true;
        }

        private void payPart_Checked(object sender, RoutedEventArgs e)
        {
            //set input pay partly enable only selected partly
            if (payPartlyPrice != null) {
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

            try
            {
                int ihvArea = int.Parse(hvArea.Text);
                int ihvPriceArea = int.Parse(hvPriceArea.Text);
                int ibhHours = int.Parse(bhHours.Text);
                int ibhPriceHours = int.Parse(bhPriceHours.Text);
                int itrNum = int.Parse(trNum.Text);
                int itrPriceNum = int.Parse(trPriceNum.Text);
                payPrice.Text = ((ihvArea * ihvPriceArea) + (ibhHours * ibhPriceHours) + (itrNum * itrPriceNum)).ToString();
            } catch (FormatException ex)
            {
                MessageBox.Show("กรุณากรอกข้อมูลที่ถูกต้อง", "ผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
                payPrice.Clear();
            }
        }
    }
}
