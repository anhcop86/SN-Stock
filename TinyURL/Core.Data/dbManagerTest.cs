using Core.Data;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class dbManagerTest: BaseRepository
    {
        public dbManagerTest(string connectionString) : base(connectionString) { }

        public async Task<TResult> GetPersonById<TResult>(Guid Id)
        {
            return await WithConnection(async c => {

                // Here's all the same data access code,
                // albeit now it's async, and nicely wrapped
                // in this handy WithConnection() call.
                var p = new DynamicParameters();
                p.Add("Id", 1, DbType.Guid);
                var people = await c.QueryAsync<TResult>(
                    sql: "sp_Person_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return people.FirstOrDefault();

            });
        }
    }
}
