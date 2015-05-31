
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;


namespace VfsCustomerService.Data
{	
	public class ContentTemplateAttachementDAO : ContentTemplateAttachementDAOBase
	{
		public ContentTemplateAttachementDAO()
		{
		}
        #region GetContentTemplateAttachementList methods
        public virtual ContentTemplateAttachementCollection GetContentTemplateAttachementList(Int32 contentTemplateId,ContentTemplateAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementGetListByContentTemplateID");

                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateId);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                ContentTemplateAttachementCollection contentTemplateAttachementCollection = new ContentTemplateAttachementCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentTemplateAttachement contentTemplateAttachement = CreateContentTemplateAttachementFromReader(reader);
                        contentTemplateAttachementCollection.Add(contentTemplateAttachement);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return contentTemplateAttachementCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateAttachementListException, ex);
            }
        }

        public virtual ContentTemplateAttachementCollection GetContentTemplateAttachementList(Int32 contentTemplateId, ContentTemplateAttachementColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetContentTemplateAttachementList(contentTemplateId, orderBy, orderDirection, 0, 0, out totalRecords);
        }

        #endregion

        #region ExistsContentTemplateByContentTemplateAttachementGet

        public virtual ContentTemplateAttachementCollection ExistsContentTemplateByContentTemplateAttachementGet(int contentTemplateID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateByContentTemplateAttachement");

                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateID);

                ContentTemplateAttachementCollection contentTemplateAttachementCollection = new ContentTemplateAttachementCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentTemplateAttachement contentTemplateAttachement = CreateContentTemplateAttachementFromReader(reader);
                        contentTemplateAttachementCollection.Add(contentTemplateAttachement);
                    }
                    reader.Close();
                }

                return contentTemplateAttachementCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateAttachementListException, ex);
            }
        }
        #endregion
	}
}
