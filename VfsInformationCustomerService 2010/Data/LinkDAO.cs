
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Vfs.WebCrawler.Data
{	
	public class LinkDAO : LinkDAOBase
	{
		public LinkDAO()
		{
		}
        #region GetLinkList methods
        public virtual LinkCollection GetLinkListBySourceId(Int32 sourceId ,LinkColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkGetListBySource");

                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, sourceId);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                LinkCollection linkCollection = new LinkCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Link link = CreateLinkFromReader(reader);
                        linkCollection.Add(link);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return linkCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetLinkListException, ex);
            }
        }

        public virtual LinkCollection GetLinkListBySourceId(Int32 sourceId, LinkColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetLinkListBySourceId(sourceId, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        #endregion

	}
}
