using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Entities;

namespace Vfs.WebCrawler.Data
{	
	public class StockNewDAO : StockNewDAOBase
	{
		public StockNewDAO()
		{
		}

        public virtual StockNewCollection GetStockNewListByLink(int linkId, StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsGetListByLink");

                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, linkId);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                StockNewCollection stockNewCollection = new StockNewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        StockNew stockNew = CreateStockNewFromReader(reader);
                        stockNewCollection.Add(stockNew);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stockNewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetStockNewListException, ex);
            }
        }

        public virtual StockNewCollection GetStockNewListByLink(int linkId, StockNewColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetStockNewListByLink(linkId, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        public virtual StockNewCollection GetStockNewListBySource(int sourceId, StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsGetListBySource");

                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, sourceId);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                StockNewCollection stockNewCollection = new StockNewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        StockNew stockNew = CreateStockNewFromReader(reader);
                        stockNewCollection.Add(stockNew);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stockNewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetStockNewListException, ex);
            }
        }

        public virtual StockNewCollection GetStockNewListBySource(int sourceId, StockNewColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetStockNewListBySource(sourceId, orderBy, orderDirection, 0, 0, out totalRecords);
        }
	}
}
