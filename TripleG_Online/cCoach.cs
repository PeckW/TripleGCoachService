using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//class matches the SQL database Table

namespace TripleG_Online
{
    public class cCoach
    {
        public int    CoachJourneyID     { get; set; }

        public string LeavingFrom        { get; set; }

        public string ArrivalDestination { get; set; }

        public string JourneyTime        { get; set; }

        public string JourneyDate        { get; set; }

        public string Price              { get; set; }

        public string FullInformation
        {
            get
            {
                //returns in this order. change here for formatting 
                return $"{LeavingFrom} {ArrivalDestination} ({JourneyTime}) {JourneyDate} {Price}";
            }
        }
    }
}
