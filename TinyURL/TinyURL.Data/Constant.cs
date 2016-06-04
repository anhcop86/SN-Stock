using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyURL.Data
{
    public class Constant
    {
        public static string DatabaseConnection =
            ConfigurationManager.ConnectionStrings["Database-URLTiny"].ConnectionString; 
    }
}
