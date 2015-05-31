
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CoreSecurityService.Entities;

namespace CoreSecurityService.Data
{    
    public abstract class StockPriceDAOBase
    {
        #region Common methods
        public virtual StockPrice CreateStockPriceFromReader(IDataReader reader)
        {
            StockPrice item = new StockPrice();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("TradingDate"))) item.TradingDate = (string)reader["TradingDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("StockCode"))) item.StockCode = (string)reader["StockCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("StockNo"))) item.StockNo = (int)reader["StockNo"];
                if (!reader.IsDBNull(reader.GetOrdinal("StockType"))) item.StockType = (string)reader["StockType"];
                if (!reader.IsDBNull(reader.GetOrdinal("BoardType"))) item.BoardType = (string)reader["BoardType"];
                if (!reader.IsDBNull(reader.GetOrdinal("OpenPrice"))) item.OpenPrice = (decimal)reader["OpenPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("ClosePrice"))) item.ClosePrice = (decimal)reader["ClosePrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("BasicPrice"))) item.BasicPrice = (decimal)reader["BasicPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("CeilingPrice"))) item.CeilingPrice = (decimal)reader["CeilingPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("FloorPrice"))) item.FloorPrice = (decimal)reader["FloorPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("AveragePrice"))) item.AveragePrice = (decimal)reader["AveragePrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("TransactionDate"))) item.TransactionDate = (DateTime)reader["TransactionDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalRoom"))) item.TotalRoom = (decimal)reader["TotalRoom"];
                if (!reader.IsDBNull(reader.GetOrdinal("CurrentRoom"))) item.CurrentRoom = (decimal)reader["CurrentRoom"];
                if (!reader.IsDBNull(reader.GetOrdinal("Suspension"))) item.Suspension = (string)reader["Suspension"];
                if (!reader.IsDBNull(reader.GetOrdinal("Delisted"))) item.Delisted = (string)reader["Delisted"];
                if (!reader.IsDBNull(reader.GetOrdinal("Halted"))) item.Halted = (string)reader["Halted"];
                if (!reader.IsDBNull(reader.GetOrdinal("Split"))) item.Split = (string)reader["Split"];
                if (!reader.IsDBNull(reader.GetOrdinal("Benefit"))) item.Benefit = (string)reader["Benefit"];
                if (!reader.IsDBNull(reader.GetOrdinal("Meeting"))) item.Meeting = (string)reader["Meeting"];
                if (!reader.IsDBNull(reader.GetOrdinal("Notice"))) item.Notice = (string)reader["Notice"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateStockPriceFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateStockPrice methods
            
        public virtual void CreateStockPrice(StockPrice stockPrice)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CoreSecurityServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStockPriceInsert");
                
                database.AddInParameter(dbCommand, "@TradingDate", DbType.AnsiString, stockPrice.TradingDate);
                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockPrice.StockCode);
                database.AddInParameter(dbCommand, "@StockNo", DbType.Int32, stockPrice.StockNo);
                database.AddInParameter(dbCommand, "@StockType", DbType.AnsiStringFixedLength, stockPrice.StockType);
                database.AddInParameter(dbCommand, "@BoardType", DbType.AnsiStringFixedLength, stockPrice.BoardType);
                database.AddInParameter(dbCommand, "@OpenPrice", DbType.Decimal, stockPrice.OpenPrice);
                database.AddInParameter(dbCommand, "@ClosePrice", DbType.Decimal, stockPrice.ClosePrice);
                database.AddInParameter(dbCommand, "@BasicPrice", DbType.Decimal, stockPrice.BasicPrice);
                database.AddInParameter(dbCommand, "@CeilingPrice", DbType.Decimal, stockPrice.CeilingPrice);
                database.AddInParameter(dbCommand, "@FloorPrice", DbType.Decimal, stockPrice.FloorPrice);
                database.AddInParameter(dbCommand, "@AveragePrice", DbType.Decimal, stockPrice.AveragePrice);
                database.AddInParameter(dbCommand, "@TransactionDate", DbType.DateTime, stockPrice.TransactionDate);
                database.AddInParameter(dbCommand, "@TotalRoom", DbType.Decimal, stockPrice.TotalRoom);
                database.AddInParameter(dbCommand, "@CurrentRoom", DbType.Decimal, stockPrice.CurrentRoom);
                database.AddInParameter(dbCommand, "@Suspension", DbType.AnsiStringFixedLength, stockPrice.Suspension);
                database.AddInParameter(dbCommand, "@Delisted", DbType.AnsiStringFixedLength, stockPrice.Delisted);
                database.AddInParameter(dbCommand, "@Halted", DbType.AnsiStringFixedLength, stockPrice.Halted);
                database.AddInParameter(dbCommand, "@Split", DbType.AnsiStringFixedLength, stockPrice.Split);
                database.AddInParameter(dbCommand, "@Benefit", DbType.AnsiStringFixedLength, stockPrice.Benefit);
                database.AddInParameter(dbCommand, "@Meeting", DbType.AnsiStringFixedLength, stockPrice.Meeting);
                database.AddInParameter(dbCommand, "@Notice", DbType.AnsiStringFixedLength, stockPrice.Notice);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateStockPriceException, ex);
            }
        }

        #endregion

        #region UpdateStockPrice methods
        
        public virtual void UpdateStockPrice(StockPrice stockPrice)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CoreSecurityServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStockPriceUpdate");            
                
                database.AddInParameter(dbCommand, "@TradingDate", DbType.AnsiString, stockPrice.TradingDate);
                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockPrice.StockCode);
                database.AddInParameter(dbCommand, "@StockNo", DbType.Int32, stockPrice.StockNo);
                database.AddInParameter(dbCommand, "@StockType", DbType.AnsiStringFixedLength, stockPrice.StockType);
                database.AddInParameter(dbCommand, "@BoardType", DbType.AnsiStringFixedLength, stockPrice.BoardType);
                database.AddInParameter(dbCommand, "@OpenPrice", DbType.Decimal, stockPrice.OpenPrice);
                database.AddInParameter(dbCommand, "@ClosePrice", DbType.Decimal, stockPrice.ClosePrice);
                database.AddInParameter(dbCommand, "@BasicPrice", DbType.Decimal, stockPrice.BasicPrice);
                database.AddInParameter(dbCommand, "@CeilingPrice", DbType.Decimal, stockPrice.CeilingPrice);
                database.AddInParameter(dbCommand, "@FloorPrice", DbType.Decimal, stockPrice.FloorPrice);
                database.AddInParameter(dbCommand, "@AveragePrice", DbType.Decimal, stockPrice.AveragePrice);
                database.AddInParameter(dbCommand, "@TransactionDate", DbType.DateTime, stockPrice.TransactionDate);
                database.AddInParameter(dbCommand, "@TotalRoom", DbType.Decimal, stockPrice.TotalRoom);
                database.AddInParameter(dbCommand, "@CurrentRoom", DbType.Decimal, stockPrice.CurrentRoom);
                database.AddInParameter(dbCommand, "@Suspension", DbType.AnsiStringFixedLength, stockPrice.Suspension);
                database.AddInParameter(dbCommand, "@Delisted", DbType.AnsiStringFixedLength, stockPrice.Delisted);
                database.AddInParameter(dbCommand, "@Halted", DbType.AnsiStringFixedLength, stockPrice.Halted);
                database.AddInParameter(dbCommand, "@Split", DbType.AnsiStringFixedLength, stockPrice.Split);
                database.AddInParameter(dbCommand, "@Benefit", DbType.AnsiStringFixedLength, stockPrice.Benefit);
                database.AddInParameter(dbCommand, "@Meeting", DbType.AnsiStringFixedLength, stockPrice.Meeting);
                database.AddInParameter(dbCommand, "@Notice", DbType.AnsiStringFixedLength, stockPrice.Notice);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateStockPriceException, ex);
            }
        }
        
        #endregion

        #region DeleteStockPrice methods
        public virtual void DeleteStockPrice(string tradingDate, string stockCode, string boardType)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CoreSecurityServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStockPriceDelete");
                
                database.AddInParameter(dbCommand, "@TradingDate", DbType.AnsiString, tradingDate);
                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockCode);
                database.AddInParameter(dbCommand, "@BoardType", DbType.AnsiStringFixedLength, boardType);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteStockPriceException, ex);
            }
        }

        #endregion

        #region GetStockPrice methods
        
        public virtual StockPrice GetStockPrice(string tradingDate, string stockCode, string boardType)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CoreSecurityServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStockPriceGet");
                
                database.AddInParameter(dbCommand, "@TradingDate", DbType.AnsiString, tradingDate);
                database.AddInParameter(dbCommand, "@StockCode", DbType.AnsiString, stockCode);
                database.AddInParameter(dbCommand, "@BoardType", DbType.AnsiStringFixedLength, boardType);
                
                StockPrice stockPrice =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stockPrice = CreateStockPriceFromReader(reader);
                        reader.Close();
                    }
                }
                return stockPrice;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetStockPriceException, ex);
            }
        }
        
        #endregion

        #region GetStockPriceList methods
        public virtual StockPriceCollection GetStockPriceList(StockPriceColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CoreSecurityServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStockPriceGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                StockPriceCollection stockPriceCollection = new StockPriceCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        StockPrice stockPrice = CreateStockPriceFromReader(reader);
                        stockPriceCollection.Add(stockPrice);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stockPriceCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetStockPriceListException, ex);
            }
        }
        
        public virtual StockPriceCollection GetStockPriceList(StockPriceColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetStockPriceList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}