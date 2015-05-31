
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class IncomingMessageContentSentDAOBase
    {
        #region Common methods
        public virtual IncomingMessageContentSent CreateIncomingMessageContentSentFromReader(IDataReader reader)
        {
            IncomingMessageContentSent item = new IncomingMessageContentSent();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("IncomingMessageContentSentID"))) item.IncomingMessageContentSentID = (long)reader["IncomingMessageContentSentID"];
                if (!reader.IsDBNull(reader.GetOrdinal("IncomingMessageContentID"))) item.IncomingMessageContentID = (long)reader["IncomingMessageContentID"];
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
                throw new ApplicationException(SR.DataAccessCreateIncomingMessageContentSentFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateIncomingMessageContentSent methods
            
        public virtual void CreateIncomingMessageContentSent(IncomingMessageContentSent incomingMessageContentSent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentSentInsert");
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentID", DbType.Int64, incomingMessageContentSent.IncomingMessageContentID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, incomingMessageContentSent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, incomingMessageContentSent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, incomingMessageContentSent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, incomingMessageContentSent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, incomingMessageContentSent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, incomingMessageContentSent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, incomingMessageContentSent.BodyMessage);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, incomingMessageContentSent.Status);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, incomingMessageContentSent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, incomingMessageContentSent.ModifiedDate);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, incomingMessageContentSent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, incomingMessageContentSent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, incomingMessageContentSent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, incomingMessageContentSent.MoID);
                database.AddOutParameter(dbCommand, "@IncomingMessageContentSentID", DbType.Int64, 0);
                
                database.ExecuteNonQuery(dbCommand);
                incomingMessageContentSent.IncomingMessageContentSentID = (long)database.GetParameterValue(dbCommand, "@IncomingMessageContentSentID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateIncomingMessageContentSentException, ex);
            }
        }

        #endregion

        #region UpdateIncomingMessageContentSent methods
        
        public virtual void UpdateIncomingMessageContentSent(IncomingMessageContentSent incomingMessageContentSent)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentSentUpdate");            
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentSentID", DbType.Int64, incomingMessageContentSent.IncomingMessageContentSentID);
                database.AddInParameter(dbCommand, "@IncomingMessageContentID", DbType.Int64, incomingMessageContentSent.IncomingMessageContentID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, incomingMessageContentSent.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, incomingMessageContentSent.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, incomingMessageContentSent.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, incomingMessageContentSent.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, incomingMessageContentSent.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, incomingMessageContentSent.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, incomingMessageContentSent.BodyMessage);
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, incomingMessageContentSent.Status);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, incomingMessageContentSent.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, incomingMessageContentSent.ModifiedDate);
                database.AddInParameter(dbCommand, "@ServiceID", DbType.String, incomingMessageContentSent.ServiceID);
                database.AddInParameter(dbCommand, "@CommandCode", DbType.String, incomingMessageContentSent.CommandCode);
                database.AddInParameter(dbCommand, "@Request", DbType.String, incomingMessageContentSent.Request);
                database.AddInParameter(dbCommand, "@MoID", DbType.String, incomingMessageContentSent.MoID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateIncomingMessageContentSentException, ex);
            }
        }
        
        #endregion

        #region DeleteIncomingMessageContentSent methods
        public virtual void DeleteIncomingMessageContentSent(long incomingMessageContentSentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentSentDelete");
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentSentID", DbType.Int64, incomingMessageContentSentID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteIncomingMessageContentSentException, ex);
            }
        }

        #endregion

        #region GetIncomingMessageContentSent methods
        
        public virtual IncomingMessageContentSent GetIncomingMessageContentSent(long incomingMessageContentSentID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentSentGet");
                
                database.AddInParameter(dbCommand, "@IncomingMessageContentSentID", DbType.Int64, incomingMessageContentSentID);
                
                IncomingMessageContentSent incomingMessageContentSent =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        incomingMessageContentSent = CreateIncomingMessageContentSentFromReader(reader);
                        reader.Close();
                    }
                }
                return incomingMessageContentSent;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetIncomingMessageContentSentException, ex);
            }
        }
        
        #endregion

        #region GetIncomingMessageContentSentList methods
        public virtual IncomingMessageContentSentCollection GetIncomingMessageContentSentList(IncomingMessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spIncomingMessageContentSentGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                IncomingMessageContentSentCollection incomingMessageContentSentCollection = new IncomingMessageContentSentCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        IncomingMessageContentSent incomingMessageContentSent = CreateIncomingMessageContentSentFromReader(reader);
                        incomingMessageContentSentCollection.Add(incomingMessageContentSent);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return incomingMessageContentSentCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetIncomingMessageContentSentListException, ex);
            }
        }
        
        public virtual IncomingMessageContentSentCollection GetIncomingMessageContentSentList(IncomingMessageContentSentColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetIncomingMessageContentSentList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}