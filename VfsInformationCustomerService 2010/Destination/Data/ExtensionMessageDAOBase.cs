
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class ExtensionMessageDAOBase
    {
        #region Common methods
        public virtual ExtensionMessage CreateExtensionMessageFromReader(IDataReader reader)
        {
            ExtensionMessage item = new ExtensionMessage();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ExtensionMessageID"))) item.ExtensionMessageID = (long)reader["ExtensionMessageID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Title"))) item.Title = (string)reader["Title"];
                if (!reader.IsDBNull(reader.GetOrdinal("Content"))) item.Content = (string)reader["Content"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateExtensionMessageFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateExtensionMessage methods
            
        public virtual void CreateExtensionMessage(ExtensionMessage extensionMessage)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageInsert");
                
                database.AddInParameter(dbCommand, "@Title", DbType.String, extensionMessage.Title);
                database.AddInParameter(dbCommand, "@Content", DbType.String, extensionMessage.Content);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, extensionMessage.CreatedDate);
                database.AddOutParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, 0);
                
                database.ExecuteNonQuery(dbCommand);
                extensionMessage.ExtensionMessageID = (long)database.GetParameterValue(dbCommand, "@ExtensionMessageID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateExtensionMessageException, ex);
            }
        }

        #endregion

        #region UpdateExtensionMessage methods
        
        public virtual void UpdateExtensionMessage(ExtensionMessage extensionMessage)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageUpdate");            
                
                database.AddInParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, extensionMessage.ExtensionMessageID);
                database.AddInParameter(dbCommand, "@Title", DbType.String, extensionMessage.Title);
                database.AddInParameter(dbCommand, "@Content", DbType.String, extensionMessage.Content);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, extensionMessage.CreatedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateExtensionMessageException, ex);
            }
        }
        
        #endregion

        #region DeleteExtensionMessage methods
        public virtual void DeleteExtensionMessage(long extensionMessageID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageDelete");
                
                database.AddInParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, extensionMessageID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteExtensionMessageException, ex);
            }
        }

        #endregion

        #region GetExtensionMessage methods
        
        public virtual ExtensionMessage GetExtensionMessage(long extensionMessageID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageGet");
                
                database.AddInParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, extensionMessageID);
                
                ExtensionMessage extensionMessage =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        extensionMessage = CreateExtensionMessageFromReader(reader);
                        reader.Close();
                    }
                }
                return extensionMessage;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetExtensionMessageException, ex);
            }
        }
        
        #endregion

        #region GetExtensionMessageList methods
        public virtual ExtensionMessageCollection GetExtensionMessageList(ExtensionMessageColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ExtensionMessageCollection extensionMessageCollection = new ExtensionMessageCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ExtensionMessage extensionMessage = CreateExtensionMessageFromReader(reader);
                        extensionMessageCollection.Add(extensionMessage);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return extensionMessageCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetExtensionMessageListException, ex);
            }
        }
        
        public virtual ExtensionMessageCollection GetExtensionMessageList(ExtensionMessageColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetExtensionMessageList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}