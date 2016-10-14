using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class DB_Tiny_URL
    {
        //public static string DatabaseConnection =
        //    ConfigurationManager.ConnectionStrings["Database-URLTiny"].ConnectionString;

        public static string GetConnectionId()
        {
            return ConfigurationManager.ConnectionStrings["Database-URLTiny"].ConnectionString;
        }
    }
}
