using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleG_Online
{
    public static class ConnectionHelper
    {
        public static string ConnectionVal(string TripleG_SQLDBConnectionString)
        {
            TripleG_SQLDBConnectionString = ConfigurationManager.ConnectionStrings["TripleG_SQLDBConnectionString"].ConnectionString;

            return TripleG_SQLDBConnectionString;
        }
    }
}
