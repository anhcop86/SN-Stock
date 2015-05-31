using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{	
	public class stock_NewsGroupDAO : stock_NewsGroupDAOBase
	{
		public stock_NewsGroupDAO()
		{
		}

        #region Getstock_NewsGroupList methods
        public virtual stock_NewsGroupCollection Getstock_NewsGroupList(Int64 NewsId, stock_NewsGroupColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsGetListByNewsId");
                database.AddInParameter(dbCommand, "@NewsId", DbType.Int64, NewsId);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                stock_NewsGroupCollection stock_NewsGroupCollection = new stock_NewsGroupCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_NewsGroup stock_NewsGroup = Createstock_NewsGroupFromReader(reader);
                        stock_NewsGroupCollection.Add(stock_NewsGroup);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stock_NewsGroupCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewsGroupListException, ex);
            }
        }

        public virtual stock_NewsGroupCollection Getstock_NewsGroupList(Int64 NewsId, stock_NewsGroupColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return Getstock_NewsGroupList(NewsId, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        #endregion
	}
}
