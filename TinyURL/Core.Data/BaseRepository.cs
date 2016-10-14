using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Dapper;
using System.Data.SqlClient;

namespace Core.Data
{
    public class BaseRepository
    {
        //private readonly string ConnectionString;

        //public BaseRepository(string connectionString)
        //{
        //    ConnectionString = connectionString;
        //}

        public static T WithConnection<T>(Func<IDbConnection, T> getData, string connectString)
         
        {
            try {
                using (var c = new SqlConnection(connectString)) {
                    c.Open();

                    return getData(c);
                }
            }
            catch (TimeoutException ex) {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", 12), ex);
            }
            catch (SqlException ex) {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", 12), ex);
            }
        }

    }
}
