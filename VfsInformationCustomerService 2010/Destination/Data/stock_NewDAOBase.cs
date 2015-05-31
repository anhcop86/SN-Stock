
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;
using System.Configuration;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class stock_NewDAOBase
    {
        #region Common methods
        public virtual stock_New Createstock_NewFromReader(IDataReader reader)
        {
            stock_New item = new stock_New();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("NewsID"))) item.NewsID = (int)reader["NewsID"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsTitle"))) item.NewsTitle = (string)reader["NewsTitle"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsDescription"))) item.NewsDescription = (string)reader["NewsDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsContent"))) item.NewsContent = (string)reader["NewsContent"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsDate"))) item.NewsDate = (DateTime)reader["NewsDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsSource"))) item.NewsSource = (string)reader["NewsSource"];
                if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) item.SymbolID = (int)reader["SymbolID"];
                if (!reader.IsDBNull(reader.GetOrdinal("UseUrl"))) item.UseUrl = (bool)reader["UseUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsUrl"))) item.NewsUrl = (string)reader["NewsUrl"];
                if (!reader.IsDBNull(reader.GetOrdinal("LanguageID"))) item.LanguageID = (int)reader["LanguageID"];
                if (!reader.IsDBNull(reader.GetOrdinal("IsApproved"))) item.IsApproved = (bool)reader["IsApproved"];
                if (!reader.IsDBNull(reader.GetOrdinal("ImageUrl"))) item.ImageUrl = (string)reader["ImageUrl"];
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
        #endregion
        
        #region Createstock_New methods
            
        public virtual void Createstock_New(stock_New stock_New)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
//              Database database = DatabaseFactory.CreateDatabase();
                
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsInsert");
                
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, stock_New.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, stock_New.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, stock_New.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, stock_New.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, stock_New.NewsSource);
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, stock_New.SymbolID);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, stock_New.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, stock_New.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, stock_New.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Boolean, stock_New.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, stock_New.ImageUrl);
                database.AddOutParameter(dbCommand, "@NewsID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                stock_New.NewsID = (int)database.GetParameterValue(dbCommand, "@NewsID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_NewException, ex);
            }
        }

        #endregion

        #region Updatestock_New methods
        
        public virtual void Updatestock_New(stock_New stock_New)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsUpdate");            
                
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int32, stock_New.NewsID);
                database.AddInParameter(dbCommand, "@NewsTitle", DbType.String, stock_New.NewsTitle);
                database.AddInParameter(dbCommand, "@NewsDescription", DbType.String, stock_New.NewsDescription);
                database.AddInParameter(dbCommand, "@NewsContent", DbType.String, stock_New.NewsContent);
                database.AddInParameter(dbCommand, "@NewsDate", DbType.DateTime, stock_New.NewsDate);
                database.AddInParameter(dbCommand, "@NewsSource", DbType.String, stock_New.NewsSource);
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, stock_New.SymbolID);
                database.AddInParameter(dbCommand, "@UseUrl", DbType.Boolean, stock_New.UseUrl);
                database.AddInParameter(dbCommand, "@NewsUrl", DbType.String, stock_New.NewsUrl);
                database.AddInParameter(dbCommand, "@LanguageID", DbType.Int32, stock_New.LanguageID);
                database.AddInParameter(dbCommand, "@IsApproved", DbType.Boolean, stock_New.IsApproved);
                database.AddInParameter(dbCommand, "@ImageUrl", DbType.String, stock_New.ImageUrl);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdatestock_NewException, ex);
            }
        }
        
        #endregion

        #region Deletestock_New methods
        public virtual void Deletestock_New(int newsID)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsDelete");
                
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int32, newsID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_NewException, ex);
            }
        }

        #endregion

        #region Getstock_New methods
        
        public virtual stock_New Getstock_New(int newsID)
        {            
            try
            {
               // Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGet");
                
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int32, newsID);
                
                stock_New stock_New =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stock_New = Createstock_NewFromReader(reader);
                        reader.Close();
                    }
                }
                return stock_New;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewException, ex);
            }
        }
        
        #endregion

        #region Getstock_NewList methods
        public virtual stock_NewCollection Getstock_NewList(stock_NewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGetList");
                
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
        
        public virtual stock_NewCollection Getstock_NewList(stock_NewColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return Getstock_NewList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}