
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Entities;

namespace Vfs.WebCrawler.Data
{    
    public abstract class StockNewDAOBase
    {
        #region Common methods
        public virtual StockNew CreateStockNewFromReader(IDataReader reader)
        {
            StockNew item = new StockNew();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("NewsId"))) item.NewsId = (long)reader["NewsId"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsTitle"))) item.NewsTitle = (string)reader["NewsTitle"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsDescription"))) item.NewsDescription = (string)reader["NewsDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsContent"))) item.NewsContent = (string)reader["NewsContent"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsDate"))) item.NewsDate = (DateTime)reader["NewsDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsSource"))) item.NewsSource = (string)reader["NewsSource"];
                if (!reader.IsDBNull(reader.GetOrdinal("ShareSymbol"))) item.ShareSymbol = (string)reader["ShareSymbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("UseUrl"))) item.UseUrl = (bool)reader["UseUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsUrl"))) item.NewsUrl = (string)reader["NewsUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("LanguageID"))) item.LanguageID = (int)reader["LanguageID"];
                if (!reader.IsDBNull(reader.GetOrdinal("IsApproved"))) item.IsApproved = (int)reader["IsApproved"];
                if (!reader.IsDBNull(reader.GetOrdinal("ImageUrl"))) item.ImageUrl = (string)reader["ImageUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("LinkId"))) item.LinkId = (int)reader["LinkId"];
                if (!reader.IsDBNull(reader.GetOrdinal("OriginalUrl"))) item.OriginalUrl = (string)reader["OriginalUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("FeedDate"))) item.FeedDate = (DateTime)reader["FeedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateStockNewFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateStockNew methods
            
        public virtual void CreateStockNew(StockNew stockNew)
        {
            try
            {

                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsInsert");
                
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, stockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, stockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, stockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, stockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, stockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, stockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, stockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, stockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, stockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, stockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, stockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, stockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, stockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@FeedDate", DbType.DateTime, stockNew.FeedDate);
                database.AddOutParameter(dbCommand, "@NewsId", DbType.Int64, 0);
                
                database.ExecuteNonQuery(dbCommand);
                stockNew.NewsId = (long)database.GetParameterValue(dbCommand, "@NewsId");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateStockNewException, ex);
            }
        }

        #endregion

        #region UpdateStockNew methods
        
        public virtual void UpdateStockNew(StockNew stockNew)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsUpdate");            
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, stockNew.NewsId);
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, stockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, stockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, stockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, stockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, stockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, stockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, stockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, stockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, stockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, stockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, stockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, stockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, stockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@FeedDate", DbType.DateTime, stockNew.FeedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateStockNewException, ex);
            }
        }
        
        #endregion

        #region DeleteStockNew methods
        public virtual void DeleteStockNew(long newsId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsDelete");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteStockNewException, ex);
            }
        }

        #endregion

        #region GetStockNew methods
        
        public virtual StockNew GetStockNew(long newsId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsGet");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                StockNew stockNew =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stockNew = CreateStockNewFromReader(reader);
                        reader.Close();
                    }
                }
                return stockNew;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetStockNewException, ex);
            }
        }
        
        #endregion

        #region GetStockNewList methods
        public virtual StockNewCollection GetStockNewList(StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spStockNewsGetList");
                
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
        
        public virtual StockNewCollection GetStockNewList(StockNewColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetStockNewList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}