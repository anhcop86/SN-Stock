
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;
using System.Configuration;

namespace Vfs.WebCrawler.Destination.Data
{	
	public class stock_NewDAO : stock_NewDAOBase
	{
		public stock_NewDAO()
		{
		}

        #region Getstock_NewList by Stock Code methods
        public virtual stock_NewCollection Getstock_NewList(string stockCode, string customerID, stock_NewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGetListByStockCode");
                
                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockCode);
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, customerID);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                stock_NewCollection stock_NewCollection = new stock_NewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_New stock_New = Createstock_NewFromReader(reader);
                        stock_NewCollection.Add(stock_New);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stock_NewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewListException, ex);
            }
        }

        public virtual stock_NewCollection Getstock_NewList(string stockCode, string customerID, stock_NewColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return Getstock_NewList(stockCode, customerID, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        public virtual stock_NewCollection Getstock_NewList(string stockCode, string customerID)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnectionNewWebsite");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGetListByStockCode"); // foler NewWebsite

                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockCode);
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, customerID);                

                stock_NewCollection stock_NewCollection = new stock_NewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_New stock_New = Createstock_NewFromReader(reader);
                        stock_NewCollection.Add(stock_New);
                    }
                    reader.Close();
                }                
                return stock_NewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewListException, ex);
            }
        }
        #endregion
	}
}
