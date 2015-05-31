
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class MessageContentSentAttachementDAOBase
    {
        #region Common methods
        public virtual MessageContentSentAttachement CreateMessageContentSentAttachementFromReader(IDataReader reader)
        {
            MessageContentSentAttachement item = new MessageContentSentAttachement();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentSentAttachementID"))) item.MessageContentSentAttachementID = (int)reader["MessageContentSentAttachementID"];
                if (!reader.IsDBNull(reader.GetOrdinal("AttachementDocument"))) item.AttachementDocument = (string)reader["AttachementDocument"];
                if (!reader.IsDBNull(reader.GetOrdinal("AttachementDescription"))) item.AttachementDescription = (string)reader["AttachementDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentID"))) item.MessageContentID = (int)reader["MessageContentID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentSentAttachementFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateMessageContentSentAttachement methods
            
        public virtual void CreateMessageContentSentAttachement(MessageContentSentAttachement messageContentSentAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentAttachementInsert");
                
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, messageContentSentAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, messageContentSentAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentSentAttachement.MessageContentID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentSentAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentSentAttachement.ModifiedDate);
                database.AddOutParameter(dbCommand, "@MessageContentSentAttachementID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                messageContentSentAttachement.MessageContentSentAttachementID = (int)database.GetParameterValue(dbCommand, "@MessageContentSentAttachementID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentSentAttachementException, ex);
            }
        }

        #endregion

        #region UpdateMessageContentSentAttachement methods
        
        public virtual void UpdateMessageContentSentAttachement(MessageContentSentAttachement messageContentSentAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentAttachementUpdate");            
                
                database.AddInParameter(dbCommand, "@MessageContentSentAttachementID", DbType.Int32, messageContentSentAttachement.MessageContentSentAttachementID);
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, messageContentSentAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, messageContentSentAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentSentAttachement.MessageContentID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentSentAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentSentAttachement.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateMessageContentSentAttachementException, ex);
            }
        }
        
        #endregion

        #region DeleteMessageContentSentAttachement methods
        public virtual void DeleteMessageContentSentAttachement(int messageContentSentAttachementID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentAttachementDelete");
                
                database.AddInParameter(dbCommand, "@MessageContentSentAttachementID", DbType.Int32, messageContentSentAttachementID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageContentSentAttachementException, ex);
            }
        }

        #endregion

        #region GetMessageContentSentAttachement methods
        
        public virtual MessageContentSentAttachement GetMessageContentSentAttachement(int messageContentSentAttachementID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentAttachementGet");
                
                database.AddInParameter(dbCommand, "@MessageContentSentAttachementID", DbType.Int32, messageContentSentAttachementID);
                
                MessageContentSentAttachement messageContentSentAttachement =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageContentSentAttachement = CreateMessageContentSentAttachementFromReader(reader);
                        reader.Close();
                    }
                }
                return messageContentSentAttachement;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentAttachementException, ex);
            }
        }
        
        #endregion

        #region GetMessageContentSentAttachementList methods
        public virtual MessageContentSentAttachementCollection GetMessageContentSentAttachementList(MessageContentSentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentAttachementGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                MessageContentSentAttachementCollection messageContentSentAttachementCollection = new MessageContentSentAttachementCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentSentAttachement messageContentSentAttachement = CreateMessageContentSentAttachementFromReader(reader);
                        messageContentSentAttachementCollection.Add(messageContentSentAttachement);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return messageContentSentAttachementCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentAttachementListException, ex);
            }
        }
        
        public virtual MessageContentSentAttachementCollection GetMessageContentSentAttachementList(MessageContentSentAttachementColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetMessageContentSentAttachementList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}