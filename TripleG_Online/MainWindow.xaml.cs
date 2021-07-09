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

namespace TripleG_Online
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsLoggedIn;
        public string nullUsername = "null";

        public MainWindow()
        {
            InitializeComponent();

            xMainFrame.Content = new TimeTablePage(nullUsername);

            IsLoggedIn = false;
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            xMainFrame.Content = new LoginPage();
        }

        private void CreateAccountBtnClick(object sender, RoutedEventArgs e)
        {
            xMainFrame.Content = new CreateAccountPage();
        }

        private void HomeBtnClick(object sender, RoutedEventArgs e)
        {
            xMainFrame.Content = new TimeTablePage(nullUsername);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void OpenStaffCoachPage_Click(object sender, RoutedEventArgs e)
        {
            StaffPasswordEntry staffPasswordEntry = new StaffPasswordEntry();
            staffPasswordEntry.Show();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
