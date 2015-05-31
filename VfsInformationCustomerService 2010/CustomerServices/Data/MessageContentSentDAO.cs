
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{	
	public class MessageContentSentDAO : MessageContentSentDAOBase
	{
		public MessageContentSentDAO()
		{
		}
        public virtual MessageContentSentCollection MessageContentSentGetListFilterByServiceTypeID(int ServiceTypeId, string Sender, string Receiver, DateTime FromModifiedDate,DateTime ToModifiedDate, MessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentSentGetListFilterByServiceTypeID");

                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, ServiceTypeId);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, Sender.ToString());
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, Receiver.ToString());
                database.AddInParameter(dbCommand, "@FromModifiedDate", DbType.DateTime, FromModifiedDate);
                database.AddInParameter(dbCommand, "@ToModifiedDate", DbType.DateTime, ToModifiedDate);
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
	}
}
