
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class stock_SymbolPermLongDAOBase
    {
        #region Common methods
        public virtual stock_SymbolPermLong Createstock_SymbolPermLongFromReader(IDataReader reader)
        {
            stock_SymbolPermLong item = new stock_SymbolPermLong();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) item.SymbolID = (int)reader["SymbolID"];
                if (!reader.IsDBNull(reader.GetOrdinal("PermDate"))) item.PermDate = (DateTime)reader["PermDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceOpen"))) item.PriceOpen = (double)reader["PriceOpen"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceClose"))) item.PriceClose = (double)reader["PriceClose"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceHigh"))) item.PriceHigh = (double)reader["PriceHigh"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceLow"))) item.PriceLow = (double)reader["PriceLow"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceAverage"))) item.PriceAverage = (double)reader["PriceAverage"];
                if (!reader.IsDBNull(reader.GetOrdinal("PricePreviousClose"))) item.PricePreviousClose = (double)reader["PricePreviousClose"];
                if (!reader.IsDBNull(reader.GetOrdinal("Volume"))) item.Volume = (double)reader["Volume"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalTrade"))) item.TotalTrade = (double)reader["TotalTrade"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalValue"))) item.TotalValue = (double)reader["TotalValue"];
                if (!reader.IsDBNull(reader.GetOrdinal("AdjRatio"))) item.AdjRatio = (double)reader["AdjRatio"];
                if (!reader.IsDBNull(reader.GetOrdinal("LastUpdated"))) item.LastUpdated = (DateTime)reader["LastUpdated"];
                if (!reader.IsDBNull(reader.GetOrdinal("CurrentForeignRoom"))) item.CurrentForeignRoom = (double)reader["CurrentForeignRoom"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyCount"))) item.BuyCount = (double)reader["BuyCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyQuantity"))) item.BuyQuantity = (double)reader["BuyQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellCount"))) item.SellCount = (double)reader["SellCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellQuantity"))) item.SellQuantity = (double)reader["SellQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyForeignCount"))) item.BuyForeignCount = (double)reader["BuyForeignCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyForeignQuantity"))) item.BuyForeignQuantity = (double)reader["BuyForeignQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyForeignValue"))) item.BuyForeignValue = (double)reader["BuyForeignValue"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellForeignCount"))) item.SellForeignCount = (double)reader["SellForeignCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellForeignQuantity"))) item.SellForeignQuantity = (double)reader["SellForeignQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellForeignValue"))) item.SellForeignValue = (double)reader["SellForeignValue"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolPermLongFromReaderException, ex);
            }
            return item;
        }
        public virtual stock_SymbolPermLongExtension Createstock_SymbolPermLongExtensionFromReader(IDataReader reader)
        {
            stock_SymbolPermLongExtension item = new stock_SymbolPermLongExtension();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) item.Symbol = (string)reader["Symbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceClose"))) item.PriceClose = (double)reader["PriceClose"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceHigh"))) item.PriceHigh = (double)reader["PriceHigh"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceLow"))) item.PriceLow = (double)reader["PriceLow"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChenhLechGia"))) item.ChenhLechGia = (double)reader["ChenhLechGia"];
                if (!reader.IsDBNull(reader.GetOrdinal("KLGDTBTrenPhien"))) item.KLGDTBTrenPhien = (double)reader["KLGDTBTrenPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("GTGDTrungBinhTrenPhien"))) item.GTGDTrungBinhTrenPhien = (double)reader["GTGDTrungBinhTrenPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("KLDM30Phien"))) item.KLDMBaMuoiPhien = (double)reader["KLDM30Phien"];
                if (!reader.IsDBNull(reader.GetOrdinal("KLDB30Phien"))) item.KLDBBaMuoiPhien = (double)reader["KLDB30Phien"];
                if (!reader.IsDBNull(reader.GetOrdinal("KLDMTruKLDBBaMuoiPhien"))) item.KLDMTruKLDBBaMuoiPhien = (double)reader["KLDMTruKLDBBaMuoiPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("DVDMTrungBinhTrenLenh"))) item.DVDMTrungBinhTrenLenh = (double)reader["DVDMTrungBinhTrenLenh"];
                if (!reader.IsDBNull(reader.GetOrdinal("DVDBTrungBinhTrenLenh"))) item.DVDBTrungBinhTrenLenh = (double)reader["DVDBTrungBinhTrenLenh"];
                if (!reader.IsDBNull(reader.GetOrdinal("NDTNNKLMuaBaMuoiPhien"))) item.NDTNNKLMuaBaMuoiPhien = (double)reader["NDTNNKLMuaBaMuoiPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("NDTNNGTMuaBaMuoiPhien"))) item.NDTNNGTMuaBaMuoiPhien = (double)reader["NDTNNGTMuaBaMuoiPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("NDTNNKLBanBaMuoiPhien"))) item.NDTNNKLBanBaMuoiPhien = (double)reader["NDTNNKLBanBaMuoiPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("NDTNNGTBanBaMuoiPhien"))) item.NDTNNGTBanBaMuoiPhien = (double)reader["NDTNNGTBanBaMuoiPhien"];
                if (!reader.IsDBNull(reader.GetOrdinal("NDTNNKLMuaTruBanBaMuoiPhien"))) item.NDTNNKLMuaTruBanBaMuoiPhien = (double)reader["NDTNNKLMuaTruBanBaMuoiPhien"];
                
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolPermLongFromReaderException, ex);
            }
            return item;
        }

        #endregion
        
        #region Createstock_SymbolPermLong methods
            
        public virtual void Createstock_SymbolPermLong(stock_SymbolPermLong stock_SymbolPermLong)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");                
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongInsert");
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, stock_SymbolPermLong.SymbolID);
                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, stock_SymbolPermLong.PermDate);
                database.AddInParameter(dbCommand, "@PriceOpen", DbType.Double, stock_SymbolPermLong.PriceOpen);
                database.AddInParameter(dbCommand, "@PriceClose", DbType.Double, stock_SymbolPermLong.PriceClose);
                database.AddInParameter(dbCommand, "@PriceHigh", DbType.Double, stock_SymbolPermLong.PriceHigh);
                database.AddInParameter(dbCommand, "@PriceLow", DbType.Double, stock_SymbolPermLong.PriceLow);
                database.AddInParameter(dbCommand, "@PriceAverage", DbType.Double, stock_SymbolPermLong.PriceAverage);
                database.AddInParameter(dbCommand, "@PricePreviousClose", DbType.Double, stock_SymbolPermLong.PricePreviousClose);
                database.AddInParameter(dbCommand, "@Volume", DbType.Double, stock_SymbolPermLong.Volume);
                database.AddInParameter(dbCommand, "@TotalTrade", DbType.Double, stock_SymbolPermLong.TotalTrade);
                database.AddInParameter(dbCommand, "@TotalValue", DbType.Double, stock_SymbolPermLong.TotalValue);
                database.AddInParameter(dbCommand, "@AdjRatio", DbType.Double, stock_SymbolPermLong.AdjRatio);
                database.AddInParameter(dbCommand, "@LastUpdated", DbType.DateTime, stock_SymbolPermLong.LastUpdated);
                database.AddInParameter(dbCommand, "@CurrentForeignRoom", DbType.Double, stock_SymbolPermLong.CurrentForeignRoom);
                database.AddInParameter(dbCommand, "@BuyCount", DbType.Double, stock_SymbolPermLong.BuyCount);
                database.AddInParameter(dbCommand, "@BuyQuantity", DbType.Double, stock_SymbolPermLong.BuyQuantity);
                database.AddInParameter(dbCommand, "@SellCount", DbType.Double, stock_SymbolPermLong.SellCount);
                database.AddInParameter(dbCommand, "@SellQuantity", DbType.Double, stock_SymbolPermLong.SellQuantity);
                database.AddInParameter(dbCommand, "@BuyForeignCount", DbType.Double, stock_SymbolPermLong.BuyForeignCount);
                database.AddInParameter(dbCommand, "@BuyForeignQuantity", DbType.Double, stock_SymbolPermLong.BuyForeignQuantity);
                database.AddInParameter(dbCommand, "@BuyForeignValue", DbType.Double, stock_SymbolPermLong.BuyForeignValue);
                database.AddInParameter(dbCommand, "@SellForeignCount", DbType.Double, stock_SymbolPermLong.SellForeignCount);
                database.AddInParameter(dbCommand, "@SellForeignQuantity", DbType.Double, stock_SymbolPermLong.SellForeignQuantity);
                database.AddInParameter(dbCommand, "@SellForeignValue", DbType.Double, stock_SymbolPermLong.SellForeignValue);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolPermLongException, ex);
            }
        }

        #endregion

        #region Updatestock_SymbolPermLong methods
        
        public virtual void Updatestock_SymbolPermLong(stock_SymbolPermLong stock_SymbolPermLong)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");                
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongUpdate");            
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, stock_SymbolPermLong.SymbolID);
                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, stock_SymbolPermLong.PermDate);
                database.AddInParameter(dbCommand, "@PriceOpen", DbType.Double, stock_SymbolPermLong.PriceOpen);
                database.AddInParameter(dbCommand, "@PriceClose", DbType.Double, stock_SymbolPermLong.PriceClose);
                database.AddInParameter(dbCommand, "@PriceHigh", DbType.Double, stock_SymbolPermLong.PriceHigh);
                database.AddInParameter(dbCommand, "@PriceLow", DbType.Double, stock_SymbolPermLong.PriceLow);
                database.AddInParameter(dbCommand, "@PriceAverage", DbType.Double, stock_SymbolPermLong.PriceAverage);
                database.AddInParameter(dbCommand, "@PricePreviousClose", DbType.Double, stock_SymbolPermLong.PricePreviousClose);
                database.AddInParameter(dbCommand, "@Volume", DbType.Double, stock_SymbolPermLong.Volume);
                database.AddInParameter(dbCommand, "@TotalTrade", DbType.Double, stock_SymbolPermLong.TotalTrade);
                database.AddInParameter(dbCommand, "@TotalValue", DbType.Double, stock_SymbolPermLong.TotalValue);
                database.AddInParameter(dbCommand, "@AdjRatio", DbType.Double, stock_SymbolPermLong.AdjRatio);
                database.AddInParameter(dbCommand, "@LastUpdated", DbType.DateTime, stock_SymbolPermLong.LastUpdated);
                database.AddInParameter(dbCommand, "@CurrentForeignRoom", DbType.Double, stock_SymbolPermLong.CurrentForeignRoom);
                database.AddInParameter(dbCommand, "@BuyCount", DbType.Double, stock_SymbolPermLong.BuyCount);
                database.AddInParameter(dbCommand, "@BuyQuantity", DbType.Double, stock_SymbolPermLong.BuyQuantity);
                database.AddInParameter(dbCommand, "@SellCount", DbType.Double, stock_SymbolPermLong.SellCount);
                database.AddInParameter(dbCommand, "@SellQuantity", DbType.Double, stock_SymbolPermLong.SellQuantity);
                database.AddInParameter(dbCommand, "@BuyForeignCount", DbType.Double, stock_SymbolPermLong.BuyForeignCount);
                database.AddInParameter(dbCommand, "@BuyForeignQuantity", DbType.Double, stock_SymbolPermLong.BuyForeignQuantity);
                database.AddInParameter(dbCommand, "@BuyForeignValue", DbType.Double, stock_SymbolPermLong.BuyForeignValue);
                database.AddInParameter(dbCommand, "@SellForeignCount", DbType.Double, stock_SymbolPermLong.SellForeignCount);
                database.AddInParameter(dbCommand, "@SellForeignQuantity", DbType.Double, stock_SymbolPermLong.SellForeignQuantity);
                database.AddInParameter(dbCommand, "@SellForeignValue", DbType.Double, stock_SymbolPermLong.SellForeignValue);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdatestock_SymbolPermLongException, ex);
            }
        }
        
        #endregion

        #region Deletestock_SymbolPermLong methods
        public virtual void Deletestock_SymbolPermLong(int symbolID, DateTime permDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");                
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongDelete");
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, symbolID);
                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, permDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_SymbolPermLongException, ex);
            }
        }

        #endregion

        #region Getstock_SymbolPermLong methods
        
        public virtual stock_SymbolPermLong Getstock_SymbolPermLong(int symbolID, DateTime permDate)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongGet");
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, symbolID);
                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, permDate);
                
                stock_SymbolPermLong stock_SymbolPermLong =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stock_SymbolPermLong = Createstock_SymbolPermLongFromReader(reader);
                        reader.Close();
                    }
                }
                return stock_SymbolPermLong;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongException, ex);
            }
        }
        
        #endregion

        #region Getstock_SymbolPermLongList methods
        public virtual stock_SymbolPermLongCollection Getstock_SymbolPermLongList(stock_SymbolPermLongColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                stock_SymbolPermLongCollection stock_SymbolPermLongCollection = new stock_SymbolPermLongCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_SymbolPermLong stock_SymbolPermLong = Createstock_SymbolPermLongFromReader(reader);
                        stock_SymbolPermLongCollection.Add(stock_SymbolPermLong);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stock_SymbolPermLongCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
        
        public virtual stock_SymbolPermLongCollection Getstock_SymbolPermLongList(stock_SymbolPermLongColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return Getstock_SymbolPermLongList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        public virtual stock_SymbolPermLongCollection GetUpdated_SymbolPermLongGetList(DateTime PermDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("GetUpdated_SymbolPermLongGetList");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, PermDate);
                
                stock_SymbolPermLongCollection stock_SymbolPermLongCollection = new stock_SymbolPermLongCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_SymbolPermLong stock_SymbolPermLong = Createstock_SymbolPermLongFromReader(reader);
                        stock_SymbolPermLongCollection.Add(stock_SymbolPermLong);
                    }
                    reader.Close();
                }                
                return stock_SymbolPermLongCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
        public virtual stock_SymbolPermLongCollection SymbolPermLongGetListTestTool(DateTime PermDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spSymbolPermLongGetListTestTool");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, PermDate);

                stock_SymbolPermLongCollection stock_SymbolPermLongCollection = new stock_SymbolPermLongCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_SymbolPermLong stock_SymbolPermLong = Createstock_SymbolPermLongFromReader(reader);
                        stock_SymbolPermLongCollection.Add(stock_SymbolPermLong);
                    }
                    reader.Close();
                }
                return stock_SymbolPermLongCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
        #endregion

        public virtual void UpdateStock_SymbolPermLongDataForeign(DateTime permDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolPermLongUpdateDataForeign");
                                
                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, permDate);

                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_SymbolPermLongException, ex);
            }
        }


    }
}