using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleG_Online
{
    public class cAccount
    {
        public string Username          { get; set; }

        public string Password          { get; set; }

        public int AccountID            { get; set; }

        public string FirstName         { get; set; }

        public string LastName          { get; set; }

        public string EmailAddress      { get; set; }

        public string PhoneNumber       { get; set; }

        public string AllAccountInformation
        {
            get
            {
                return $"{Username} {Password}";
            }
        }
    }
}
