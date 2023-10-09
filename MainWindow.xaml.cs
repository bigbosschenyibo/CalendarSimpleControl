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

namespace SimpleCalendarControlDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            calendatControl.PrevMonth();
        }

        private void todayBtn_Click(object sender, RoutedEventArgs e)
        {
            calendatControl.CurrMonth();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            calendatControl.NextMonth();
        }
    }
}
