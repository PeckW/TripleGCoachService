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
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        List<cAccount> UsernameList = new List<cAccount>();

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if(passwordInsTextbox.Text == re_EnterPasswordInsTextbox.Text && userNameAvailabliltyStatus.Background == Brushes.LightGreen)
            {
                DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

                dataAccessFromDB.CreateAccount(userNameInsTextbox.Text, passwordInsTextbox.Text, firstNameIns.Text, lastNameIns.Text, emailAddressIns.Text, phoneNumberIns.Text );

                firstNameIns.Text = "";
                lastNameIns.Text = "";
                emailAddressIns.Text = "";
                phoneNumberIns.Text = "";

                userNameInsTextbox.Text = "";
                passwordInsTextbox.Text = "";
                re_EnterPasswordInsTextbox.Text = "";

                errorMessageDisplay.Text = "Account Creation SucessFul, Please Log In";
            }
            else if (passwordInsTextbox.Text != re_EnterPasswordInsTextbox.Text)
            {
                errorMessageDisplay.Text = "Entered passwords do not match, please try again";

                passwordInsTextbox.Text = "";
                re_EnterPasswordInsTextbox.Text = "";
            }
            else if(userNameAvailabliltyStatus.Background == Brushes.Red || userNameInsTextbox.Text == "")
            {
                errorMessageDisplay.Text = "Entered Username is unavailable, please enter a different Username";

                userNameInsTextbox.Text = "";

                passwordInsTextbox.Text = "";
                re_EnterPasswordInsTextbox.Text = "";
            }
            else if (passwordInsTextbox.Text == null || passwordInsTextbox.Text == " ")
            {
                errorMessageDisplay.Text = "Spaces are not allowed in the password";

                passwordInsTextbox.Text = "";
                re_EnterPasswordInsTextbox.Text = "";
            }
        }

        private void UserNameInsTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            UsernameList = dataAccessFromDB.UserNameCheck(userNameInsTextbox.Text);

            UpdateListBoxBindingOfUsername();
        }

        private void UpdateListBoxBindingOfUsername()
        {
            userNameAvailabliltyStatus.ItemsSource = UsernameList;

            if(UsernameList.Count == 0)
            {
                userNameAvailabliltyStatus.Background = Brushes.LightGreen;
                userNameAvailabliltyStatus.Foreground = Brushes.LightGreen;
            }
            else
            {
                userNameAvailabliltyStatus.Background = Brushes.Red;
                userNameAvailabliltyStatus.Foreground = Brushes.Red;
            }
        }
    }
}

