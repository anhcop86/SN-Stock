using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{	
	public class ExtensionMessageDAO : ExtensionMessageDAOBase
	{
		public ExtensionMessageDAO()
		{
		}

        public virtual ExtensionMessageCollection GetExtensionMessageListByTitle(string title, string customerID ,ExtensionMessageColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageGetListByTitle");
                database.AddInParameter(dbCommand, "@Title", DbType.AnsiString, title.ToString());
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, customerID.ToString());
                 database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                ExtensionMessageCollection extensionMessageCollection = new ExtensionMessageCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ExtensionMessage extensionMessage = CreateExtensionMessageFromReader(reader);
                        extensionMessageCollection.Add(extensionMessage);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return extensionMessageCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetExtensionMessageListException, ex);
            }
        }
	}
}
