using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Data
{
    public class StockDao
    {
        #region Common methods
        public virtual Stock CreateShareTypeFromReader(IDataReader reader)
        {
            Stock item = new Stock();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CompanyID"))) item.CompanyID = (string)reader["CompanyID"];               
                if (!reader.IsDBNull(reader.GetOrdinal("FinishPrice"))) item.FinishPrice = (decimal)reader["FinishPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("FinishAmount"))) item.FinishAmount = (int)reader["FinishAmount"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalAmount"))) item.TotalAmount = (int)reader["TotalAmount"];
                if (!reader.IsDBNull(reader.GetOrdinal("Diff"))) item.Diff = (decimal)reader["Diff"];
                if (!reader.IsDBNull(reader.GetOrdinal("RefPrice"))) item.RefPrice = (decimal)reader["RefPrice"];  
                
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
            return item;

        }
        #endregion
        #region GetShareTypeList methods
        public virtual List<Stock> GetStockPriceHSX(string tickerList)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabasePriceHSX");
                DbCommand dbCommand = database.GetStoredProcCommand("HN_AGStock_Session_SelectStock");

                database.AddInParameter(dbCommand, "@TickerList", DbType.String, tickerList.ToString());

                List<Stock> listItem = new List<Stock>();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Stock item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return listItem;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
        }

        public virtual List<Stock> GetStockPriceHNX(string tickerList)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabasePriceHNX");
                DbCommand dbCommand = database.GetStoredProcCommand("HN_AGStock_Session_SelectStockHNX");

                database.AddInParameter(dbCommand, "@TickerList", DbType.String, tickerList.ToString());

                List<Stock> listItem = new List<Stock>();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Stock item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return listItem;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
        }
        #endregion

    }
}
