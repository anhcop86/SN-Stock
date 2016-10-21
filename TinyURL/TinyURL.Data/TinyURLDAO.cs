using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyURL.Entity;
using Dapper;
using System.Data;
using Core.Data;

namespace TinyURL.Data
{
    public class TinyURLDAO : BaseDAO
    {
        public long Id { get; set; }

        public URLTiny GetURLTiny()
        {
            //return DbManager.QuerySingle<URLTiny>("URLTinySelect", new DbQueryOption {
            //    ConnectionID = DB_Tiny_URL.GetConnectionId(),
            //    ParameterModel = new DynamicParameters(this)
            //});
            return DbManager.QuerySingle<URLTiny>("URLTinySelect", new DbQueryOption {
                ConnectionID = DB_Tiny_URL.GetConnectionId(),
                ParameterModel = new DynamicParameters(this)
            });
        }

        //public URLTiny GetURLTinyFirst()
        //{
        //    return DbManager.QuerySingleAsync<URLTiny>("URLTinySelect", new DbQueryOption {
        //        ConnectionID = DB_Tiny_URL.GetConnectionId(),
        //        ParameterModel = new DynamicParameters(this)
        //    });
        //}


        public URLTiny GetURLTinyAsync()
        {
            //return DbManager.QuerySingle<URLTiny>("URLTinySelect", new DbQueryOption {
            //    ConnectionID = DB_Tiny_URL.GetConnectionId(),
            //    ParameterModel = new DynamicParameters(this)
            //});
            return DbManager.QuerySingleAsync<URLTiny>("URLTinySelect", new DbQueryOption {
                ConnectionID = DB_Tiny_URL.GetConnectionId(),
                ParameterModel = new DynamicParameters(this)
            }).Result;
        }
    }
}
