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
    /// Interaction logic for StaffInsertCoachWindow.xaml
    /// </summary>
    public partial class StaffInsertCoachWindow : Window
    {
        public StaffInsertCoachWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTheNewJourneyFromInput();

            if (MessageBox.Show("Journey Saved, Input another Journey?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
               
            }
            else
            {
                this.Close();
            }
        }

        private void AddTheNewJourneyFromInput()
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            dataAccessFromDB.InsertNewCoachJourney(sLeavingFromIns.Text, sArrivalDestinationIns.Text, sJourneyTimeIns.Text, sJourneyDateIns.Text, sPriceIns.Text);

            sLeavingFromIns.Text = "";
            sArrivalDestinationIns.Text = "";
            sJourneyTimeIns.Text = "";
            sJourneyDateIns.Text = "";
            sPriceIns.Text = "";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteJourneyFromInput();

            if (MessageBox.Show("Journey Deleted, Make another Change?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
            }
            else
            {
                this.Close();
            }
        }

        private void DeleteJourneyFromInput()
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            int coachJourneyIdAsInt = int.Parse(sCoachJournyIdIns.Text);

            dataAccessFromDB.DeleteCoachJourney(coachJourneyIdAsInt);

            sCoachJournyIdIns.Text = "";
        }
    }
}
