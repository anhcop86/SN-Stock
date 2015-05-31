
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class MessageContentAttachementDAOBase
    {
        #region Common methods
        public virtual MessageContentAttachement CreateMessageContentAttachementFromReader(IDataReader reader)
        {
            MessageContentAttachement item = new MessageContentAttachement();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentAttachementID"))) item.MessageContentAttachementID = (int)reader["MessageContentAttachementID"];
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
                throw new ApplicationException(SR.DataAccessCreateMessageContentAttachementFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateMessageContentAttachement methods
            
        public virtual void CreateMessageContentAttachement(MessageContentAttachement messageContentAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementInsert");
                
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, messageContentAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, messageContentAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentAttachement.MessageContentID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentAttachement.ModifiedDate);
                database.AddOutParameter(dbCommand, "@MessageContentAttachementID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                messageContentAttachement.MessageContentAttachementID = (int)database.GetParameterValue(dbCommand, "@MessageContentAttachementID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentAttachementException, ex);
            }
        }

        #endregion

        #region UpdateMessageContentAttachement methods
        
        public virtual void UpdateMessageContentAttachement(MessageContentAttachement messageContentAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementUpdate");            
                
                database.AddInParameter(dbCommand, "@MessageContentAttachementID", DbType.Int32, messageContentAttachement.MessageContentAttachementID);
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, messageContentAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, messageContentAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentAttachement.MessageContentID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentAttachement.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateMessageContentAttachementException, ex);
            }
        }
        
        #endregion

        #region DeleteMessageContentAttachement methods
        public virtual void DeleteMessageContentAttachement(int messageContentAttachementID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementDelete");
                
                database.AddInParameter(dbCommand, "@MessageContentAttachementID", DbType.Int32, messageContentAttachementID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageContentAttachementException, ex);
            }
        }

        #endregion

        #region GetMessageContentAttachement methods
        
        public virtual MessageContentAttachement GetMessageContentAttachement(int messageContentAttachementID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementGet");
                
                database.AddInParameter(dbCommand, "@MessageContentAttachementID", DbType.Int32, messageContentAttachementID);
                
                MessageContentAttachement messageContentAttachement =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageContentAttachement = CreateMessageContentAttachementFromReader(reader);
                        reader.Close();
                    }
                }
                return messageContentAttachement;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentAttachementException, ex);
            }
        }
        
        #endregion

        #region GetMessageContentAttachementList methods
        public virtual MessageContentAttachementCollection GetMessageContentAttachementList(MessageContentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                MessageContentAttachementCollection messageContentAttachementCollection = new MessageContentAttachementCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentAttachement messageContentAttachement = CreateMessageContentAttachementFromReader(reader);
                        messageContentAttachementCollection.Add(messageContentAttachement);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return messageContentAttachementCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentAttachementListException, ex);
            }
        }
        
        public virtual MessageContentAttachementCollection GetMessageContentAttachementList(MessageContentAttachementColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetMessageContentAttachementList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion

        #region Extention
        public virtual MessageContentAttachementCollection ExistsMessageContentForMessageContentAttachement(int messageContentId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentForMessageContentAttachement");

                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentId);               
                

                MessageContentAttachementCollection messageContentAttachementCollection = new MessageContentAttachementCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentAttachement messageContentAttachement = CreateMessageContentAttachementFromReader(reader);
                        messageContentAttachementCollection.Add(messageContentAttachement);
                    }
                    reader.Close();
                }                
                return messageContentAttachementCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentAttachementListException, ex);
            }
        }
        #endregion
    }
}