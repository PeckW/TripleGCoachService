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
    /// Interaction logic for AccountDetailsPage.xaml
    /// </summary>
    public partial class CreditCardDetails : Page
    {
        List<cAccount> AccountIdList = new List<cAccount>();
        List<cCreditCard> CreditCardList = new List<cCreditCard>();
        List<cCreditCard> SpecificCardListView = new List<cCreditCard>();
        List<cCreditCard> GetCreditCardIdList = new List<cCreditCard>();

        public string UsernameCardCheck;
        int accountID;
        int creditCardID;

        public CreditCardDetails(string username)
        {
            UsernameCardCheck = username;
            InitializeComponent();

            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            AccountIdList = dataAccessFromDB.GetAccountID(UsernameCardCheck);

            string mystringAccountID = string.Join(",", AccountIdList.Select(t => t.AccountID).ToArray());

            accountID = int.Parse(mystringAccountID);

            UpdateListBoxBinding();
        }

        private void SaveCreditCardDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            AccountIdList = dataAccessFromDB.GetAccountID(UsernameCardCheck);

            GetSessionAccountID();

            dataAccessFromDB.InsertCreditCardDetails(creditCardHolderNameIns.Text, creditCardNumberIns.Text, securityNumberIns.Text, expiryDateIns.Text, accountID);

            creditCardHolderNameIns.Text = "";
            creditCardNumberIns.Text = "";
            securityNumberIns.Text = "";
            expiryDateIns.Text = "";

            UpdateListBoxBinding();


        }

        public void GetSessionAccountID()
        {
            string mystringAccountID = string.Join(",", AccountIdList.Select(t => t.AccountID).ToArray());

            AccountIDPlaceHolder.Text = mystringAccountID;

            accountID = int.Parse(mystringAccountID);
        }

        public void UpdateListBoxBinding()
        {
            //get the creditcard ID not the account id

            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();
            CreditCardList = dataAccessFromDB.GetAllCreditCards(accountID);

            SavedCreditCardListBox.ItemsSource = CreditCardList;

            CardsAvailable.DisplayMemberBinding = new Binding("CreditCardNumber");
        }

        private void SavedCreditCardListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            if (SavedCreditCardListBox.SelectedIndex >= 0)
            {
                string selectedItem = CreditCardList[SavedCreditCardListBox.SelectedIndex].CreditCardNumber;

                GetCreditCardIdList = dataAccessFromDB.GetCreditCardIdFromAccountId(accountID, selectedItem);

                string mystringCreditCardID = string.Join(",", GetCreditCardIdList.Select(t => t.CreditCardID).ToArray());

                creditCardID = int.Parse(mystringCreditCardID);

                //make a query for GetCreditCardInfo. for one row of the credit card table. Pass in CreditCardID to get the specific CreditCard
                SpecificCardListView = dataAccessFromDB.ShowCardFromListView(creditCardID);

                ShowCreditCardDetailsListView.ItemsSource = SpecificCardListView;

                ChnListView.DisplayMemberBinding = new Binding("CreditCardHolderName");
                CcnListView.DisplayMemberBinding = new Binding("CreditCardNumber");
                SnListView.DisplayMemberBinding = new Binding("SecurityNumber");
                EdListView.DisplayMemberBinding = new Binding("ExpiryDate");

                UpdateListBoxBinding();
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataAccessFromDB dataAccessFromDB = new DataAccessFromDB();

            dataAccessFromDB.DeleteCreditCard(DeleteCardIns.Text);

            DeleteCardIns.Text = "";
        }
    }
}
