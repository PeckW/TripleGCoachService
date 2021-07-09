using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Windows.Controls;

namespace TripleG_Online
    //get the data from the DB using methods in this class.
    //talk to SQL server and connect here

{
    public class DataAccessFromDB
    {
        public List<cCoach> GetArrivalDestination(string arrivalDestination)
        {
            //use the 'using' statment here so it opens and closes the connection within this code segment. Does not leave the connection open
            //inside the SqlConnection goes your connection string from the app.congif setup
            //use the template below every time you want to set up a new connection:

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                //use stored procedures for more security

                var output = connection.Query<cCoach>("dbo.spCoach_GetArrivalDestination @ArrivalDestination", new { ArrivalDestination = arrivalDestination }).ToList();

                return output;
            }
        }

        public List<cCoach> GetLeavingFromDestination(string leavingFromDestination)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_LeavingFromDestination @LeavingFrom", new { LeavingFrom = leavingFromDestination }).ToList();

                return output;
            }
        }

        public List<cCoach> GetJourneyDate(string journeyDate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_GetJourneyDate @JourneyDate", new { JourneyDate = journeyDate }).ToList();

                return output;
            }
        }

        public List<cCoach> GetPrice(string price)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_GetPrice @Price", new { Price = price }).ToList();

                return output;
            }
        }

        public List<cCoach> GetAnyValueOfSearchBoxIns(string searchBoxIns)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_SearchAllCoachInfo @LeavingFrom, @ArrivalDestination, @JourneyDate, @Price", new { LeavingFrom = searchBoxIns, ArrivalDestination = searchBoxIns, JourneyDate = searchBoxIns, Price = searchBoxIns }).ToList();

                return output;
            }
        }

        public void CreateAccount(string userName, string password, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                //creates newAccount and then puts it into the list
                //cAccount newAccount = new cAccount { Username = userName, Password = password };

                    List<cAccount> account = new List<cAccount>();

                    //puts it straight into the list
                    account.Add(new cAccount { Username = userName, Password = password, FirstName = firstName, LastName = lastName, EmailAddress = emailAddress, PhoneNumber = phoneNumber });

                    connection.Execute("dbo.spAccount_InsertNewAccount @Username, @Password, @FirstName, @LastName, @EmailAddress, @PhoneNumber", account);

                //NEED TO ADD THE MERGING OF ACCOUNT AND CUSTOMER INTO STORED PROCEDURE. INSERTACCOUNTDETAILS method will be removed
            }
        }

        public List<cAccount> UserNameCheck(string userName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cAccount>("dbo.spAccount_GetAllByUsername @Username", new { Username = userName }).ToList();

                return output;
            }
                
        }

        public List<cAccount> UsernameAccountCheck(string userName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cAccount>("dbo.spAccount_GetUsernameForCheck @Username", new { Username = userName }).ToList();

                return output;
            }
        }

        public List<cCoach> GetAllJourneys()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_ShowAllCoachJourneys").ToList();

                return output;
            }
        }

        public void InsertCreditCardDetails(string creditCardHolderName, string creditCardNumber, string secuityNumber, string expiryDate, int accountID)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                List<cCreditCard> creditCards = new List<cCreditCard>();

                creditCards.Add(new cCreditCard { CreditCardHolderName = creditCardHolderName, CreditCardNumber = creditCardNumber, SecurityNumber = secuityNumber, ExpiryDate = expiryDate, AccountID = accountID }); 

                connection.Execute("dbo.spCreditCard_InsertCreditCard @CreditCardHolderName, @CreditCardNumber, @SecurityNumber, @ExpiryDate, @AccountID", creditCards);
            }
        }

        public List<cAccount> GetAccountID(string usernameCardCheck)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cAccount>("dbo.spAccount_GetAccountIdFromUsername @Username", new { Username = usernameCardCheck }).ToList();

                return output;
            }
        }

        public List<cCreditCard> GetAllCreditCards(int accountID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCreditCard>("dbo.spCreditCard_GetAllCreditCards @AccountID", new { AccountID = accountID }).ToList();

                return output;
            }
        }

        public List<cCreditCard> ShowCardFromListView(int creditCardID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCreditCard>("dbo.spCreditCard_ShowSpecificCardDetails @CreditCardID", new { CreditCardID = creditCardID }).ToList();

                return output;
            }
        }

        public List<cCreditCard> GetCreditCardIdFromAccountId(int accountID, string creditCardNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCreditCard>("dbo.spCreditCard_GetCreditCardIdFromAccountId @AccountID, @CreditCardNumber", new { AccountID = accountID, CreditCardNumber = creditCardNumber }).ToList();

                return output;
            }
        }

        public void InsertNewCoachJourney(string leavingFrom, string arrivalDestination, string journeyTime, string journeyDate, string price)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                List<cCoach> NewJourney = new List<cCoach>();

                NewJourney.Add(new cCoach { LeavingFrom = leavingFrom, ArrivalDestination = arrivalDestination, JourneyTime = journeyTime, JourneyDate = journeyDate, Price = price });

                connection.Execute("dbo.spCoach_InsertNewJourney @LeavingFrom, @ArrivalDestination, @JourneyTime, @JourneyDate, @Price", NewJourney);
            }
        }

        public void DeleteCoachJourney(int coachJourneyID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                List<cCoach> DeleteJourney = new List<cCoach>();

                DeleteJourney.Add(new cCoach { CoachJourneyID = coachJourneyID });

                connection.Execute("dbo.spCoach_DeleteCoachJourney @CoachJourneyID", DeleteJourney);
            }
        }

        public void DeleteCreditCard(string creditCardNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                List<cCreditCard> DeleteCreditCardInList = new List<cCreditCard>();

                DeleteCreditCardInList.Add(new cCreditCard { CreditCardNumber = creditCardNumber });

                connection.Execute("dbo.spCreditCard_DeleteCreditCard @CreditCardNumber", DeleteCreditCardInList);
            }

        }

        public List<cCoach> GetCoachJourneyID(string leavingFrom, string arrivalDestination, string journeyDate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_GetCoachJourneyID @LeavingFrom, @ArrivalDestination, @JourneyDate", new { LeavingFrom = leavingFrom, ArrivalDestination = arrivalDestination, JourneyDate = journeyDate }).ToList();

                return output;
            }
        }

        public List<cCoach> GetSpecificJourneyFromID(int coachJourneyID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cCoach>("dbo.spCoach_GetSpecificJourneyFromID @CoachJourneyID", new { CoachJourneyID = coachJourneyID }).ToList();

                return output;
            }
        }

        public List<cAccount> GetPasswordFromUsername(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionVal("TripleG_SQLDBConnectionString")))
            {
                var output = connection.Query<cAccount>("dbo.spAccount_GetPasswordFromUsername @Username", new { Username = username }).ToList();

                return output;
            }

        }
    }
}

