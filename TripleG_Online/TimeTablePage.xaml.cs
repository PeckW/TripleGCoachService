using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;




namespace TripleG_Online
{
    /// <summary>
    /// Interaction logic for TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {
        List<cCoach> destinations = new List<cCoach>();
        List<cCoach> AllDestinations = new List<cCoach>();
        List<cCreditCard> CreditCardList = new List<cCreditCard>();
        List<cAccount> AccountIdList = new List<cAccount>();
        List<cCoach> GetJourneyIdList = new List<cCoach>();
        List<cCoach> SpecificJourney = new List<cCoach>();
        List<cCreditCard> CreditCardIDList = new List<cCreditCard>();

        public string UsernameCheck;
        int accountID;
        int coachJourneyID;


        public TimeTablePage(string username)
        {
            UsernameCheck = username;

            InitializeComponent();

            GetAllDestinations();
        }

        private void CheckUserNameAndAccountID()
        {
            if (UsernameCheck == "null")
            {
                accountID = 0;
            }
            else
            {
                DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

                AccountIdList = dataAccessFromDB.GetAccountID(UsernameCheck);

                string mystringAccountID = string.Join(",", AccountIdList.Select(t => t.AccountID).ToArray());

                accountID = int.Parse(mystringAccountID);
            }
        }

        private void GetAllDestinations()
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();
            AllDestinations = dataAccessFromDB.GetAllJourneys();

            hLeavingFrom.DisplayMemberBinding        = new Binding("LeavingFrom");
            hArrivalDestination.DisplayMemberBinding = new Binding("ArrivalDestination");
            hJourneyTime.DisplayMemberBinding        = new Binding("JourneyTime");
            hJourneyDate.DisplayMemberBinding        = new Binding("JourneyDate");
            hPrice.DisplayMemberBinding              = new Binding("Price");

            dataFoundListBox.ItemsSource = AllDestinations;
        }

        private void UpdateListboxBinding()
        {
            //dataSource doesnt work = ItemSource may be the replacement
            dataFoundListBox.ItemsSource = destinations;

            hLeavingFrom.DisplayMemberBinding        = new Binding("LeavingFrom");
            hArrivalDestination.DisplayMemberBinding = new Binding("ArrivalDestination");
            hJourneyTime.DisplayMemberBinding        = new Binding("JourneyTime");
            hJourneyDate.DisplayMemberBinding        = new Binding("JourneyDate");
            hPrice.DisplayMemberBinding              = new Binding("Price");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            if (SelectionSearchFieldBox.SelectedItem == leavingFromSelection)
            {
                if(string.IsNullOrEmpty(SearchFieldText.Text))
                {
                    GetAllDestinations();
                }
                else
                {
                    destinations = dataAccessFromDB.GetLeavingFromDestination(SearchFieldText.Text);

                    UpdateListboxBinding();
                }
            }
            else if(SelectionSearchFieldBox.SelectedItem == arrivalDestinationSelection)
            {
                if (string.IsNullOrEmpty(SearchFieldText.Text))
                {
                    GetAllDestinations();
                }
                else
                {
                    destinations = dataAccessFromDB.GetArrivalDestination(SearchFieldText.Text);

                    UpdateListboxBinding();
                }

            }
            else if(SelectionSearchFieldBox.SelectedItem == journeyDateSelection)
            {
                if (string.IsNullOrEmpty(SearchFieldText.Text))
                {
                    GetAllDestinations();
                }
                else
                {
                    destinations = dataAccessFromDB.GetJourneyDate(SearchFieldText.Text);

                    UpdateListboxBinding();
                }

            }
            else if(SelectionSearchFieldBox.SelectedItem == priceSelection)
            {
                if (string.IsNullOrEmpty(SearchFieldText.Text))
                {
                    GetAllDestinations();
                }
                else
                {
                    destinations = dataAccessFromDB.GetPrice(SearchFieldText.Text);

                    UpdateListboxBinding();
                }

            }
            else
            {
                if (string.IsNullOrEmpty(SearchFieldText.Text))
                {
                    GetAllDestinations();
                }
                else
                {
                    destinations = dataAccessFromDB.GetAnyValueOfSearchBoxIns(SearchFieldText.Text);

                    UpdateListboxBinding();
                }

            }
        }

        public void DataFoundListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckUserNameAndAccountID();

            if (AccountIdList.Count != 0)
            {
                BuyTicketPopup.IsOpen = true;

                UpdatePopUpTicketWindow();

                UpdatePopUpTicketWindowJourneyList();
            }
            else
            {
                BuyTicketPopup.IsOpen = false;
            }
        }

        public void UpdatePopUpTicketWindow()
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();
            CreditCardList = dataAccessFromDB.GetAllCreditCards(accountID);

            popUpCreditCardListBox.ItemsSource = CreditCardList;

            popUpSelectCard.DisplayMemberBinding = new Binding("CreditCardNumber");

            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            BuyTicketPopup.IsOpen = false;
        }

        public void UpdatePopUpTicketWindowJourneyList()
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            if(dataFoundListBox.SelectedIndex >= 0)
            {
                string selectedItemLeavingFrom = AllDestinations[dataFoundListBox.SelectedIndex].LeavingFrom;
                string selectedItemArrivalDestination = AllDestinations[dataFoundListBox.SelectedIndex].ArrivalDestination;
                string selectedItemJourneyTime = AllDestinations[dataFoundListBox.SelectedIndex].JourneyTime;

                GetJourneyIdList = dataAccessFromDB.GetCoachJourneyID(selectedItemLeavingFrom, selectedItemArrivalDestination, selectedItemJourneyTime);

                string mystringJourneyID = string.Join(",", GetJourneyIdList.Select(t => t.CoachJourneyID).ToArray());
                coachJourneyID = int.Parse(mystringJourneyID);

                SpecificJourney = dataAccessFromDB.GetSpecificJourneyFromID(coachJourneyID);

                popUpJourneyDetails.ItemsSource = SpecificJourney;

                popUpJdJourneyID.DisplayMemberBinding          = new Binding("CoachJourneyID");
                popUpJdLeavingFrom.DisplayMemberBinding        = new Binding("LeavingFrom");
                popUpJdArrivalDestination.DisplayMemberBinding = new Binding("ArrivalDestination");
                poUpJdJourneyTime.DisplayMemberBinding         = new Binding("JourneyTime");
                popUpJdJourneyDate.DisplayMemberBinding        = new Binding("JourneyDate");
                popUpJdPrice.DisplayMemberBinding              = new Binding("Price");
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            if(popUpCreditCardListBox.SelectedIndex >= 0)
            {
                popUpReciptOfPurchase.IsOpen = true;

                string selectedCreditCard = CreditCardList[popUpCreditCardListBox.SelectedIndex].CreditCardNumber;

                CreditCardIDList = dataAccessFromDB.GetCreditCardIdFromAccountId(accountID, selectedCreditCard);

                string mystringCreditCardID = string.Join(",", CreditCardIDList.Select(t => t.CreditCardID).ToArray());

                int creditCardID = int.Parse(mystringCreditCardID);

                CreditCardIDList = dataAccessFromDB.ShowCardFromListView(creditCardID);

                ReciptListView.ItemsSource = SpecificJourney;

                reciptCoachID.DisplayMemberBinding = new Binding("CoachJourneyID");
                reciptLeavingFrom.DisplayMemberBinding = new Binding("LeavingFrom");
                reciptToDestination.DisplayMemberBinding = new Binding("ArrivalDestination");

                ReciptCardListView.ItemsSource = CreditCardIDList;
                reciptCard.DisplayMemberBinding = new Binding("CreditCardNumber");
            }
            else
            {
                MessageBox.Show("No selected credit card\nPlease select a credit card to continue with purchase.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                popUpReciptOfPurchase.IsOpen = false;
            }



        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            popUpReciptOfPurchase.IsOpen = false;
            BuyTicketPopup.IsOpen = false;
        }
    }
}
