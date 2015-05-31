
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace Vfs.WebCrawler.Data
{    
    public abstract class ApprovedStockNewDAOBase
    {
        #region Common methods
        public virtual ApprovedStockNew CreateApprovedStockNewFromReader(IDataReader reader)
        {
            ApprovedStockNew item = new ApprovedStockNew();
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
                if (!reader.IsDBNull(reader.GetOrdinal("Comment"))) item.Comment = (string)reader["Comment"];
                if (!reader.IsDBNull(reader.GetOrdinal("LinkId"))) item.LinkId = (int)reader["LinkId"];
                if (!reader.IsDBNull(reader.GetOrdinal("OriginalUrl"))) item.OriginalUrl = (string)reader["OriginalUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("ApprovedDate"))) item.ApprovedDate = (DateTime)reader["ApprovedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateApprovedStockNewFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateApprovedStockNew methods
            
        public virtual void CreateApprovedStockNew(ApprovedStockNew approvedStockNew)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spApprovedStockNewsInsert");
                
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, approvedStockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, approvedStockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, approvedStockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, approvedStockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, approvedStockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, approvedStockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, approvedStockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, approvedStockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, approvedStockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, approvedStockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, approvedStockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@Comment", DbType.String, approvedStockNew.Comment);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, approvedStockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, approvedStockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@ApprovedDate", DbType.DateTime, approvedStockNew.ApprovedDate);
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, approvedStockNew.NewsId);
                
                database.ExecuteNonQuery(dbCommand);
                //approvedStockNew.NewsId = (long)database.GetParameterValue(dbCommand, "@NewsId");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateApprovedStockNewException, ex);
            }
        }

        #endregion

        #region UpdateApprovedStockNew methods
        
        public virtual void UpdateApprovedStockNew(ApprovedStockNew approvedStockNew)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spApprovedStockNewsUpdate");            
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, approvedStockNew.NewsId);
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, approvedStockNew.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, approvedStockNew.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, approvedStockNew.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, approvedStockNew.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, approvedStockNew.NewsSource);
                database.AddInParameter(dbCommand, "@ShareSymbol", DbType.String, approvedStockNew.ShareSymbol);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, approvedStockNew.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, approvedStockNew.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, approvedStockNew.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Int32, approvedStockNew.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, approvedStockNew.ImageUrl);
                database.AddInParameter(dbCommand, "@Comment", DbType.String, approvedStockNew.Comment);
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, approvedStockNew.LinkId);
                database.AddInParameter(dbCommand, "@OriginalUrl", DbType.String, approvedStockNew.OriginalUrl);
                database.AddInParameter(dbCommand, "@ApprovedDate", DbType.DateTime, approvedStockNew.ApprovedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateApprovedStockNewException, ex);
            }
        }
        
        #endregion

        #region DeleteApprovedStockNew methods
        public virtual void DeleteApprovedStockNew(long newsId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spApprovedStockNewsDelete");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteApprovedStockNewException, ex);
            }
        }

        #endregion

        #region GetApprovedStockNew methods
        
        public virtual ApprovedStockNew GetApprovedStockNew(long newsId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spApprovedStockNewsGet");
                
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, newsId);
                
                ApprovedStockNew approvedStockNew =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        approvedStockNew = CreateApprovedStockNewFromReader(reader);
                        reader.Close();
                    }
                }
                return approvedStockNew;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetApprovedStockNewException, ex);
            }
        }
        
        #endregion

        #region GetApprovedStockNewList methods
        public virtual ApprovedStockNewCollection GetApprovedStockNewList(ApprovedStockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spApprovedStockNewsGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ApprovedStockNewCollection approvedStockNewCollection = new ApprovedStockNewCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ApprovedStockNew approvedStockNew = CreateApprovedStockNewFromReader(reader);
                        approvedStockNewCollection.Add(approvedStockNew);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return approvedStockNewCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetApprovedStockNewListException, ex);
            }
        }
        
        public virtual ApprovedStockNewCollection GetApprovedStockNewList(ApprovedStockNewColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetApprovedStockNewList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}