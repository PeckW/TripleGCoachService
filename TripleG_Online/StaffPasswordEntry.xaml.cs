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
    /// Interaction logic for StaffPasswordEntry.xaml
    /// </summary>
    public partial class StaffPasswordEntry : Window
    {
        string CurrentStaffPassword = "394";

        public StaffPasswordEntry()
        {
            InitializeComponent();
        }

        private void EnterPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordIns.Text;

            if(password == CurrentStaffPassword)
            {
                StaffInsertCoachWindow staffInsertCoachWindow = new StaffInsertCoachWindow();
                staffInsertCoachWindow.Show();

                this.Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}
