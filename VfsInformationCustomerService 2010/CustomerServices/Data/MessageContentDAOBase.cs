
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class MessageContentDAOBase
    {
        #region Common methods
        public virtual MessageContent CreateMessageContentFromReader(IDataReader reader)
        {
            MessageContent item = new MessageContent();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentID"))) item.MessageContentID = (int)reader["MessageContentID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentTemplateID"))) item.ContentTemplateID = (int)reader["ContentTemplateID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceTypeID"))) item.ServiceTypeID = (int)reader["ServiceTypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Sender"))) item.Sender = (string)reader["Sender"];
                if (!reader.IsDBNull(reader.GetOrdinal("Receiver"))) item.Receiver = (string)reader["Receiver"];
                if (!reader.IsDBNull(reader.GetOrdinal("Subject"))) item.Subject = (string)reader["Subject"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyContentType"))) item.BodyContentType = (string)reader["BodyContentType"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyEncoding"))) item.BodyEncoding = (string)reader["BodyEncoding"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyMessage"))) item.BodyMessage = (string)reader["BodyMessage"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("Status"))) item.Status = (Int32)reader["Status"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceID"))) item.ServiceID = (string)reader["ServiceID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CommandCode"))) item.CommandCode = (string)reader["CommandCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("Request"))) item.Request = (string)reader["Request"];
                if (!reader.IsDBNull(reader.GetOrdinal("MoID"))) item.MoID = (string)reader["MoID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChargeYN"))) item.ChargeYN = (string)reader["ChargeYN"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalMessages"))) item.TotalMessages = (short)reader["TotalMessages"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateMessageContent methods
            
        public virtual void CreateMessageContent(MessageContent messageContent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentInsert");
                
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, messageContent.ContentTemplateID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, messageContent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, messageContent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, messageContent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, messageContent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, messageContent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, messageContent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, messageContent.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContent.ModifiedDate);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, messageContent.Status);
                database.AddOutParameter(dbCommand, "@MessageContentID", DbType.Int32, 0);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, messageContent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, messageContent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, messageContent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, messageContent.MoID);
                database.AddInParameter(dbCommand, "@ChargeYN", DbType.String, messageContent.ChargeYN);
                database.AddInParameter(dbCommand, "@TotalMessages", DbType.Int16, messageContent.TotalMessages);
                
                database.ExecuteNonQuery(dbCommand);
                messageContent.MessageContentID = (int)database.GetParameterValue(dbCommand, "@MessageContentID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentException, ex);
            }
        }

        #endregion

        #region UpdateMessageContent methods
        
        public virtual void UpdateMessageContent(MessageContent messageContent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentUpdate");            
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContent.MessageContentID);
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, messageContent.ContentTemplateID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, messageContent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, messageContent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, messageContent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, messageContent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, messageContent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, messageContent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, messageContent.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContent.ModifiedDate);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, messageContent.Status);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, messageContent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, messageContent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, messageContent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, messageContent.MoID);
                database.AddInParameter(dbCommand, "@ChargeYN", DbType.String, messageContent.ChargeYN);
                database.AddInParameter(dbCommand, "@TotalMessages", DbType.Int16, messageContent.TotalMessages);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateMessageContentException, ex);
            }
        }
        
        #endregion

        #region DeleteMessageContent methods
        public virtual void DeleteMessageContent(int messageContentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentDelete");
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageContentException, ex);
            }
        }

        #endregion

        #region GetMessageContent methods
        
        public virtual MessageContent GetMessageContent(int messageContentID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGet");
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentID);
                
                MessageContent messageContent =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageContent = CreateMessageContentFromReader(reader);
                        reader.Close();
                    }
                }
                return messageContent;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentException, ex);
            }
        }
        
        #endregion

        #region GetMessageContentList methods
        public virtual MessageContentCollection GetMessageContentList(MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                MessageContentCollection messageContentCollection = new MessageContentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContent messageContent = CreateMessageContentFromReader(reader);
                        messageContentCollection.Add(messageContent);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return messageContentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentListException, ex);
            }
        }
        
        public virtual MessageContentCollection GetMessageContentList(MessageContentColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetMessageContentList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
        #region Extention
        public virtual MessageContentCollection ExistServiceTypeIdForMessageContent(int serviceTypeID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExistServiceTypeIdForMessageContent");

                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceTypeID);

                MessageContentCollection messageContentCollection = new MessageContentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContent messageContent = CreateMessageContentFromReader(reader);
                        messageContentCollection.Add(messageContent);
                    }
                    reader.Close();
                }
                return messageContentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentListException, ex);
            }
        }

        public virtual MessageContentCollection ExistsContentTemplateForMessageContent(int ContentTemplateID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateForMessageContent");

                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, ContentTemplateID);

                MessageContentCollection messageContentCollection = new MessageContentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContent messageContent = CreateMessageContentFromReader(reader);
                        messageContentCollection.Add(messageContent);
                    }
                    reader.Close();
                }
                return messageContentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentListException, ex);
            }
        }
          #endregion
        public virtual MessageContent GetMessageContentIDAndDate(int messageContentID, DateTime ModifiedDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGetIDAndDate");

                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentID);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, ModifiedDate);

                MessageContent messageContent = null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageContent = CreateMessageContentFromReader(reader);
                        reader.Close();
                    }
                }
                return messageContent;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentException, ex);
            }
        }
        public virtual void DeleteMessageContentYear(DateTime modifiedDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentDeleteYear");

                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, modifiedDate);

                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageContentException, ex);
            }
        }
    }
}