
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;


namespace VfsCustomerService.Data
{	
	public class MessageContentAttachementDAO : MessageContentAttachementDAOBase
	{
		public MessageContentAttachementDAO()
		{
		}

        #region GetMessageContentAttachementList methods
        public virtual MessageContentAttachementCollection GetMessageContentAttachementList(Int32 messageContentID, MessageContentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementGetListByMessageContentID");
                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentID);
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

        public virtual MessageContentAttachementCollection GetMessageContentAttachementList(Int32 messageContentID, MessageContentAttachementColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetMessageContentAttachementList(messageContentID, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        #endregion
        #region DeleteMessageContentAttachement methods
        public virtual void DeleteMessageContentAttachementByMessageContent(int messageContentID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spMessageContentAttachementDeleteByMessageContent");

                database.AddInParameter(dbCommand, "@MessageContentID", DbType.Int32, messageContentID);

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
	}
}
