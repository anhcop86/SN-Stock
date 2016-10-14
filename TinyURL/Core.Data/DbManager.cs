using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Core.Data
{
    public class DbManager : BaseRepository
    {
        //public DbManager()
        //    : base(DB_Tiny_URL.GetConnectionId())
        //{

        //}
        public static IEnumerable<TResult> Query<TResult>(string queryName, DbQueryOption queryOption)
        {
            using (var sqlConnection = new SqlConnection(queryOption.ConnectionID)) {
                try {

                    if (sqlConnection.State != ConnectionState.Open) {
                        sqlConnection.Open();
                    }
                    return (IEnumerable<TResult>)sqlConnection.Query<TResult>(queryName, queryOption.ParameterModel, commandType: CommandType.StoredProcedure);

                }
                catch (Exception) {
                    throw;
                }
                //finally {
                //    sqlConnection.Close();
                //}
            }

        }
        //public static TResult QuerySingle<TResult>(string queryName, DbQueryOption queryOption)
        //{
        //    using (var sqlConnection = new SqlConnection(queryOption.ConnectionID)) {
        //        try {

        //            if (sqlConnection.State != ConnectionState.Open) {
        //                sqlConnection.Open();
        //            }
        //            return (TResult)sqlConnection.QuerySingle<TResult>(queryName, queryOption.ParameterModel, commandType: CommandType.StoredProcedure);

        //        }
        //        catch (Exception) {
        //            throw;
        //        }
        //        //finally {
        //        //    sqlConnection.Close();
        //        //}
        //    }

        //}

        public static TResult QuerySingle<TResult>(string queryName, DbQueryOption queryOption)
        {
            return WithConnection(c => 
                c.QuerySingle<TResult>(queryName, queryOption.ParameterModel, commandType: CommandType.StoredProcedure), 
                queryOption.ConnectionID
                 );
        }

    }
}
