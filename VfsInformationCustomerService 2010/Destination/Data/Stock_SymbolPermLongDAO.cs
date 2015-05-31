using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{
    public class stock_SymbolPermLongDAO : stock_SymbolPermLongDAOBase
    {
        public stock_SymbolPermLongDAO()
        {
        }
        public virtual stock_SymbolPermLongExtensionCollection Export_SymbolPermLongList(DateTime permDate, string market)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMarketReview");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, permDate);
                database.AddInParameter(dbCommand, "@Market", DbType.String, market.ToString());


                stock_SymbolPermLongExtensionCollection stock_SymbolPermLongExtensionCollection = new stock_SymbolPermLongExtensionCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_SymbolPermLongExtension stock_SymbolPermLongExtension = Createstock_SymbolPermLongExtensionFromReader(reader);
                        stock_SymbolPermLongExtensionCollection.Add(stock_SymbolPermLongExtension);
                    }
                    reader.Close();
                }
                return stock_SymbolPermLongExtensionCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
    }
}

