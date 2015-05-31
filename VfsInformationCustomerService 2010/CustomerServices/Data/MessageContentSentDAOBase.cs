
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class MessageContentSentDAOBase
    {
        #region Common methods
        public virtual MessageContentSent CreateMessageContentSentFromReader(IDataReader reader)
        {
            MessageContentSent item = new MessageContentSent();
            
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
                if (!reader.IsDBNull(reader.GetOrdinal("MessageContentSentID"))) item.MessageContentSentID = (long)reader["MessageContentSentID"];
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
                throw new ApplicationException(SR.DataAccessCreateMessageContentSentFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateMessageContentSent methods
            
        public virtual void CreateMessageContentSent(MessageContentSent messageContentSent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentInsert");
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentSent.MessageContentID);
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, messageContentSent.ContentTemplateID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, messageContentSent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, messageContentSent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, messageContentSent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, messageContentSent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, messageContentSent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, messageContentSent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, messageContentSent.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentSent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentSent.ModifiedDate);
                database.AddOutParameter(dbCommand, "@MessageContentSentID", DbType.Int64, 0);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, messageContentSent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, messageContentSent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, messageContentSent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, messageContentSent.MoID);
                database.AddInParameter(dbCommand, "@ChargeYN", DbType.String, messageContentSent.ChargeYN);
                database.AddInParameter(dbCommand, "@TotalMessages", DbType.Int16, messageContentSent.TotalMessages);

                
                database.ExecuteNonQuery(dbCommand);
                messageContentSent.MessageContentSentID = (long)database.GetParameterValue(dbCommand, "@MessageContentSentID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateMessageContentSentException, ex);
            }
        }

        #endregion

        #region UpdateMessageContentSent methods
        
        public virtual void UpdateMessageContentSent(MessageContentSent messageContentSent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentUpdate");            
                
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentSent.MessageContentID);
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, messageContentSent.ContentTemplateID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, messageContentSent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, messageContentSent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, messageContentSent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, messageContentSent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, messageContentSent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, messageContentSent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, messageContentSent.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, messageContentSent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, messageContentSent.ModifiedDate);
                database.AddInParameter(dbCommand, "@MessageContentSentID", DbType.Int64, messageContentSent.MessageContentSentID);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, messageContentSent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, messageContentSent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, messageContentSent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, messageContentSent.MoID);
                database.AddInParameter(dbCommand, "@ChargeYN", DbType.String, messageContentSent.ChargeYN);
                database.AddInParameter(dbCommand, "@TotalMessages", DbType.Int16, messageContentSent.TotalMessages);
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateMessageContentSentException, ex);
            }
        }
        
        #endregion

        #region DeleteMessageContentSent methods
        public virtual void DeleteMessageContentSent(long messageContentSentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentDelete");
                
                database.AddInParameter(dbCommand, "@MessageContentSentID", DbType.Int64, messageContentSentID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteMessageContentSentException, ex);
            }
        }

        #endregion

        #region GetMessageContentSent methods
        
        public virtual MessageContentSent GetMessageContentSent(long messageContentSentID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentGet");
                
                database.AddInParameter(dbCommand, "@MessageContentSentID", DbType.Int64, messageContentSentID);
                
                MessageContentSent messageContentSent =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        messageContentSent = CreateMessageContentSentFromReader(reader);
                        reader.Close();
                    }
                }
                return messageContentSent;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentException, ex);
            }
        }
        
        #endregion

        #region GetMessageContentSentList methods
        public virtual MessageContentSentCollection GetMessageContentSentList(MessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                MessageContentSentCollection messageContentSentCollection = new MessageContentSentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentSent messageContentSent = CreateMessageContentSentFromReader(reader);
                        messageContentSentCollection.Add(messageContentSent);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return messageContentSentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentListException, ex);
            }
        }
        
        public virtual MessageContentSentCollection GetMessageContentSentList(MessageContentSentColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetMessageContentSentList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion

        public virtual MessageContentSentCollection ExistsServiceTypeForMessageContentSent(int serviceTypeID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeForMessageContentSent");

                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceTypeID);                

                MessageContentSentCollection messageContentSentCollection = new MessageContentSentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentSent messageContentSent = CreateMessageContentSentFromReader(reader);
                        messageContentSentCollection.Add(messageContentSent);
                    }
                    reader.Close();
                }                
                return messageContentSentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentListException, ex);
            }
        }
        public virtual MessageContentSentCollection ExistsMessageContentForMessageContentSent(int MessageContentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentForMessageContentSent");

                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, MessageContentID);

                MessageContentSentCollection messageContentSentCollection = new MessageContentSentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentSent messageContentSent = CreateMessageContentSentFromReader(reader);
                        messageContentSentCollection.Add(messageContentSent);
                    }
                    reader.Close();
                }
                return messageContentSentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentListException, ex);
            }
        }
        public virtual MessageContentSentCollection ExistsContentTemplateForMessageContentSent(int ContentTemplateID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateForMessageContentSent");

                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, ContentTemplateID);

                MessageContentSentCollection messageContentSentCollection = new MessageContentSentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        MessageContentSent messageContentSent = CreateMessageContentSentFromReader(reader);
                        messageContentSentCollection.Add(messageContentSent);
                    }
                    reader.Close();
                }
                return messageContentSentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetMessageContentSentListException, ex);
            }
        }
    }
}