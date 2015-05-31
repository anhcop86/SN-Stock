using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Vfs.WebCrawler.Destination.Data
{
    public abstract class IndexTestToolDaoBase
    {
        public virtual IndexTestTool CreateIndexTestToolFromReader(IDataReader reader)
        {
            IndexTestTool item = new IndexTestTool();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) item.Symbol = (string)reader["Symbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("IndexSymbol"))) item.IndexSymbol = (double)reader["IndexSymbol"];                
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_NewFromReaderException, ex);
            }
            return item;
        }

        public virtual IndexTestToolCollection GetIndexTestTool(DateTime permDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIndexTestTool");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, permDate);

                IndexTestToolCollection indexTestToolCollection = new IndexTestToolCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        IndexTestTool indexTestTool = CreateIndexTestToolFromReader(reader);
                        indexTestToolCollection.Add(indexTestTool);
                    }
                    reader.Close();
                }
                return indexTestToolCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_NewException, ex);
            }
        }
    }
}
