
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class MessageCommandDAOBase
    {
        #region Common methods
        public virtual MessageCommand CreateMessageCommandFromReader(IDataReader reader)
        {
            MessageCommand item = new MessageCommand();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MessageCommandID"))) item.MessageCommandID = (int)reader["MessageCommandID"];
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentID"))) item.MessageContentID = (int)reader["MessageContentID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Status"))) item.Status = (int)reader["Status"];
                if (!reader.IsDBNull(reader.GetOrdinal("ProcessedDateTime"))) item.ProcessedDateTime = (DateTime)reader["ProcessedDateTime"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageCommandFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateMessageCommand methods
            
        public virtual void CreateMessageCommand(MessageCommand messageCommand)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageCommandInsert");
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageCommand.MessageContentID);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, messageCommand.Status);
                database.AddInParameter(dbCommand, "@ProcessedDateTime", DbType.DateTime, messageCommand.ProcessedDateTime);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageCommand.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageCommand.ModifiedDate);
                database.AddOutParameter(dbCommand, "@MessageCommandID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                messageCommand.MessageCommandID = (int)database.GetParameterValue(dbCommand, "@MessageCommandID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageCommandException, ex);
            }
        }

        #endregion

        #region UpdateMessageCommand methods
        
        public virtual void UpdateMessageCommand(MessageCommand messageCommand)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageCommandUpdate");            
                
                database.AddInParameter(dbCommand, "@MessageCommandID", DbType.Int32, messageCommand.MessageCommandID);
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageCommand.MessageContentID);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, messageCommand.Status);
                database.AddInParameter(dbCommand, "@ProcessedDateTime", DbType.DateTime, messageCommand.ProcessedDateTime);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageCommand.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageCommand.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateMessageCommandException, ex);
            }
        }
        
        #endregion

        #region DeleteMessageCommand methods
        public virtual void DeleteMessageCommand(int messageCommandID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageCommandDelete");
                
                database.AddInParameter(dbCommand, "@MessageCommandID", DbType.Int32, messageCommandID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageCommandException, ex);
            }
        }

        #endregion

        #region GetMessageCommand methods
        
        public virtual MessageCommand GetMessageCommand(int messageCommandID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageCommandGet");
                
                database.AddInParameter(dbCommand, "@MessageCommandID", DbType.Int32, messageCommandID);
                
                MessageCommand messageCommand =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageCommand = CreateMessageCommandFromReader(reader);
                        reader.Close();
                    }
                }
                return messageCommand;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageCommandException, ex);
            }
        }
        
        #endregion

        #region GetMessageCommandList methods
        public virtual MessageCommandCollection GetMessageCommandList(MessageCommandColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageCommandGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                MessageCommandCollection messageCommandCollection = new MessageCommandCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageCommand messageCommand = CreateMessageCommandFromReader(reader);
                        messageCommandCollection.Add(messageCommand);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return messageCommandCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageCommandListException, ex);
            }
        }
        
        public virtual MessageCommandCollection GetMessageCommandList(MessageCommandColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetMessageCommandList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}