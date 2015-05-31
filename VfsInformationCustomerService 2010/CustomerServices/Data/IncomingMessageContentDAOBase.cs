
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class IncomingMessageContentDAOBase
    {
        #region Common methods
        public virtual IncomingMessageContent CreateIncomingMessageContentFromReader(IDataReader reader)
        {
            IncomingMessageContent item = new IncomingMessageContent();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("IncomingMessageContentID"))) item.IncomingMessageContentID = (int)reader["IncomingMessageContentID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceTypeID"))) item.ServiceTypeID = (int)reader["ServiceTypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Sender"))) item.Sender = (string)reader["Sender"];
                if (!reader.IsDBNull(reader.GetOrdinal("Receiver"))) item.Receiver = (string)reader["Receiver"];
                if (!reader.IsDBNull(reader.GetOrdinal("Subject"))) item.Subject = (string)reader["Subject"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyContentType"))) item.BodyContentType = (string)reader["BodyContentType"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyEncoding"))) item.BodyEncoding = (string)reader["BodyEncoding"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyMessage"))) item.BodyMessage = (string)reader["BodyMessage"];
                if (!reader.IsDBNull(reader.GetOrdinal("Status"))) item.Status = (int)reader["Status"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceID"))) item.ServiceID = (string)reader["ServiceID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CommandCode"))) item.CommandCode = (string)reader["CommandCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("Request"))) item.Request = (string)reader["Request"];
                if (!reader.IsDBNull(reader.GetOrdinal("MoID"))) item.MoID = (string)reader["MoID"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateIncomingMessageContentFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateIncomingMessageContent methods
            
        public virtual void CreateIncomingMessageContent(IncomingMessageContent incomingMessageContent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentInsert");
                
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, incomingMessageContent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, incomingMessageContent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, incomingMessageContent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, incomingMessageContent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, incomingMessageContent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, incomingMessageContent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, incomingMessageContent.BodyMessage);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, incomingMessageContent.Status);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, incomingMessageContent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, incomingMessageContent.ModifiedDate);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, incomingMessageContent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, incomingMessageContent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, incomingMessageContent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, incomingMessageContent.MoID);
                database.AddOutParameter(dbCommand, "@IncomingMessageContentID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                incomingMessageContent.IncomingMessageContentID = (int)database.GetParameterValue(dbCommand, "@IncomingMessageContentID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateIncomingMessageContentException, ex);
            }
        }

        #endregion

        #region UpdateIncomingMessageContent methods
        
        public virtual void UpdateIncomingMessageContent(IncomingMessageContent incomingMessageContent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentUpdate");            
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentID", DbType.Int32, incomingMessageContent.IncomingMessageContentID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, incomingMessageContent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, incomingMessageContent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, incomingMessageContent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, incomingMessageContent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, incomingMessageContent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, incomingMessageContent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, incomingMessageContent.BodyMessage);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, incomingMessageContent.Status);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, incomingMessageContent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, incomingMessageContent.ModifiedDate);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, incomingMessageContent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, incomingMessageContent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, incomingMessageContent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, incomingMessageContent.MoID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateIncomingMessageContentException, ex);
            }
        }
        
        #endregion

        #region DeleteIncomingMessageContent methods
        public virtual void DeleteIncomingMessageContent(int incomingMessageContentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentDelete");
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentID", DbType.Int32, incomingMessageContentID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteIncomingMessageContentException, ex);
            }
        }

        #endregion

        #region GetIncomingMessageContent methods
        
        public virtual IncomingMessageContent GetIncomingMessageContent(int incomingMessageContentID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentGet");
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentID", DbType.Int32, incomingMessageContentID);
                
                IncomingMessageContent incomingMessageContent =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        incomingMessageContent = CreateIncomingMessageContentFromReader(reader);
                        reader.Close();
                    }
                }
                return incomingMessageContent;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetIncomingMessageContentException, ex);
            }
        }
        
        #endregion

        #region GetIncomingMessageContentList methods
        public virtual IncomingMessageContentCollection GetIncomingMessageContentList(IncomingMessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                IncomingMessageContentCollection incomingMessageContentCollection = new IncomingMessageContentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        IncomingMessageContent incomingMessageContent = CreateIncomingMessageContentFromReader(reader);
                        incomingMessageContentCollection.Add(incomingMessageContent);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return incomingMessageContentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetIncomingMessageContentListException, ex);
            }
        }
        
        public virtual IncomingMessageContentCollection GetIncomingMessageContentList(IncomingMessageContentColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetIncomingMessageContentList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}