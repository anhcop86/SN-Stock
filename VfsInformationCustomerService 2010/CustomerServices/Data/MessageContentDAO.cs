using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using VfsCustomerService.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace VfsCustomerService.Data
{	
	public class MessageContentDAO : MessageContentDAOBase
	{
		public MessageContentDAO()
		{
		}

        #region GetMessageContentList methods
        public virtual MessageContentCollection GetMessageContentList(int status, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGetListByStatus");

                database.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
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

        public virtual MessageContentCollection GetMessageContentList(int status, MessageContentColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetMessageContentList(status, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        public virtual MessageContentCollection GetMessageContentList(int status,int serviceType, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGetListByServiceType");
                
                database.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
                database.AddInParameter(dbCommand, "@ServiceType", DbType.Int32, serviceType);
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

        public virtual MessageContentCollection GetMessageContentList(int status,int serviceType, MessageContentColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetMessageContentList(status, serviceType, orderBy, orderDirection, 0, 0, out totalRecords);
        }
#endregion
        #region GetMessageContentListFilterByServiceTypeId
        public virtual MessageContentCollection MessageContentGetListFilterByServiceTypeID(int ServiceTypeID, string Sender, string Receiver, DateTime FromModifiedDate, DateTime ToModifiedDate, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentGetListFilterByServiceTypeID");

                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, ServiceTypeID);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, Sender.ToString());
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, Receiver.ToString());
                database.AddInParameter(dbCommand, "@FromModifiedDate", DbType.DateTime, FromModifiedDate);
                database.AddInParameter(dbCommand, "@ToModifiedDate", DbType.DateTime, ToModifiedDate);
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
        #endregion
    }
}
