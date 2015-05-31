
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Entities;

namespace Vfs.WebCrawler.Data
{    
    public abstract class RejectedStockNewDAOBase
    {
        #region Common methods
        public virtual RejectedStockNew CreateRejectedStockNewFromReader(IDataReader reader)
        {
            RejectedStockNew item = new RejectedStockNew();
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
                if (!reader.IsDBNull(reader.GetOrdinal("RejectedReason"))) item.RejectedReason = (string)reader["RejectedReason"];
                if (!reader.IsDBNull(reader.GetOrdinal("LinkId"))) item.LinkId = (int)reader["LinkId"];
                if (!reader.IsDBNull(reader.GetOrdinal("OriginalUrl"))) item.OriginalUrl = (string)reader["OriginalUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("RejectedDate"))) item.RejectedDate = (DateTime)reader["RejectedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateRejectedStockNewFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateRejectedStockNew methods
            
        public virtual void CreateRejectedStockNew(RejectedStockNew rejectedStockNew)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spRejectedStockNewsInsert");
                
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, rejectedStockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, rejectedStockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, rejectedStockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, rejectedStockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, rejectedStockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, rejectedStockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, rejectedStockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, rejectedStockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, rejectedStockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, rejectedStockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, rejectedStockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@RejectedReason", DbType.String, rejectedStockNew.RejectedReason);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, rejectedStockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, rejectedStockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@RejectedDate", DbType.DateTime, rejectedStockNew.RejectedDate);
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, rejectedStockNew.NewsId);
                
                database.ExecuteNonQuery(dbCommand);
                //rejectedStockNew.NewsId = (long)database.GetParameterValue(dbCommand, "@NewsId");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateRejectedStockNewException, ex);
            }
        }

        #endregion

        #region UpdateRejectedStockNew methods
        
        public virtual void UpdateRejectedStockNew(RejectedStockNew rejectedStockNew)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spRejectedStockNewsUpdate");            
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, rejectedStockNew.NewsId);
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, rejectedStockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, rejectedStockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, rejectedStockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, rejectedStockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, rejectedStockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, rejectedStockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, rejectedStockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, rejectedStockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, rejectedStockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, rejectedStockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, rejectedStockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@RejectedReason", DbType.String, rejectedStockNew.RejectedReason);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, rejectedStockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, rejectedStockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@RejectedDate", DbType.DateTime, rejectedStockNew.RejectedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateRejectedStockNewException, ex);
            }
        }
        
        #endregion

        #region DeleteRejectedStockNew methods
        public virtual void DeleteRejectedStockNew(long newsId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spRejectedStockNewsDelete");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteRejectedStockNewException, ex);
            }
        }

        #endregion

        #region GetRejectedStockNew methods
        
        public virtual RejectedStockNew GetRejectedStockNew(long newsId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spRejectedStockNewsGet");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                RejectedStockNew rejectedStockNew =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        rejectedStockNew = CreateRejectedStockNewFromReader(reader);
                        reader.Close();
                    }
                }
                return rejectedStockNew;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetRejectedStockNewException, ex);
            }
        }
        
        #endregion

        #region GetRejectedStockNewList methods
        public virtual RejectedStockNewCollection GetRejectedStockNewList(RejectedStockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spRejectedStockNewsGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                RejectedStockNewCollection rejectedStockNewCollection = new RejectedStockNewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        RejectedStockNew rejectedStockNew = CreateRejectedStockNewFromReader(reader);
                        rejectedStockNewCollection.Add(rejectedStockNew);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return rejectedStockNewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetRejectedStockNewListException, ex);
            }
        }
        
        public virtual RejectedStockNewCollection GetRejectedStockNewList(RejectedStockNewColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetRejectedStockNewList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}