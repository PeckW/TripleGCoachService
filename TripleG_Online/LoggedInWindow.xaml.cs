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

namespace TripleG_Online
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        private string Username;

        public LoggedInWindow(string username)
        {
            InitializeComponent();
            Username = username;
            xIsLoggedInWindowFrame.Content = new TimeTablePage(Username);

        }

        private void AccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            xIsLoggedInWindowFrame.Content = new CreditCardDetails(Username);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            xIsLoggedInWindowFrame.Content = new TimeTablePage(Username);
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
