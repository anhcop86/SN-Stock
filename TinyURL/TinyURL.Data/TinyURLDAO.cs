using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyURL.Entity;
using Dapper;
using System.Data;

namespace TinyURL.Data
{
    public class TinyURLDAO
    {
        public static URLTiny GetURLTiny(long id)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                try
                {
                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        sqlConnection.Open();
                    }
                    var parameter = new DynamicParameters();
                    parameter.Add("@Id", id);
                    return (URLTiny)sqlConnection.Query<URLTiny>("URLTinySelect", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
                catch (Exception)
                {
                    return null;
                    //throw;
                }
            }
            
        }
    }
}
