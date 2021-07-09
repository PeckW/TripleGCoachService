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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<cAccount> UsernameAccountCheck = new List<cAccount>();
        List<cAccount> PasswordAcountCheck = new List<cAccount>();
        public string Username;

        public LoginPage()
        {
            InitializeComponent();

            MainWindow mainWindow = new MainWindow();
            mainWindow.IsLoggedIn = false;
        }

        public void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            //Converts the specified item in the list into a string format.
            UsernameAccountCheck = dataAccessFromDB.UsernameAccountCheck(loginUsernameIns.Text);
            string mystringUsername = string.Join(",", UsernameAccountCheck.Select(t => t.Username).ToArray());

            PasswordAcountCheck = dataAccessFromDB.GetPasswordFromUsername(loginUsernameIns.Text);
            string myStringPassword = string.Join(",", PasswordAcountCheck.Select(p => p.Password).ToArray());

            Username = mystringUsername;


            if (mystringUsername == loginUsernameIns.Text && myStringPassword == loginPasswordIns.Text)
            {
                //Open new window here
                GoToLoggedInPage();

                loginUsernameIns.Text = "";
                loginPasswordIns.Text = "";
            }
            else if (mystringUsername == loginUsernameIns.Text && myStringPassword != loginPasswordIns.Text)
            {
                userNameCheckListBox.Background  = Brushes.Green;
                userNameCheckListBox.Foreground  = Brushes.Green;
                userNameCheckListBox.BorderBrush = Brushes.Green;
                loginUsernameIns.BorderBrush     = Brushes.Green;

                passwordCheckListBox.Background  = Brushes.Red;
                passwordCheckListBox.Foreground  = Brushes.Red;
                passwordCheckListBox.BorderBrush = Brushes.Red;
                loginPasswordIns.BorderBrush     = Brushes.Red;

                errorMessageLabel.Text = "Error: Incorrect Password";
            }
            else if (mystringUsername != loginUsernameIns.Text)
            {
                userNameCheckListBox.Background  = Brushes.Red;
                userNameCheckListBox.Foreground  = Brushes.Red;
                userNameCheckListBox.BorderBrush = Brushes.Red;
                loginUsernameIns.BorderBrush     = Brushes.Red;

                errorMessageLabel.Text = "Error: Username does not exist";
            }
            else
            {
                userNameCheckListBox.Background  = Brushes.Red;
                userNameCheckListBox.Foreground  = Brushes.Red;
                userNameCheckListBox.BorderBrush = Brushes.Red;
                loginUsernameIns.BorderBrush     = Brushes.Red;

                passwordCheckListBox.Background  = Brushes.Red;
                passwordCheckListBox.Foreground  = Brushes.Red;
                passwordCheckListBox.BorderBrush = Brushes.Red;
                loginPasswordIns.BorderBrush     = Brushes.Red;

                errorMessageLabel.Text = "Error: Incorrect Username / Password";
            }
        }

        public void GoToLoggedInPage()
        {
            LoggedInWindow loggedInWindow = new LoggedInWindow(Username);
            loggedInWindow.Show();
        }
    }
}
