
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class ContentTemplateAttachementDAOBase
    {
        #region Common methods
        public virtual ContentTemplateAttachement CreateContentTemplateAttachementFromReader(IDataReader reader)
        {
            ContentTemplateAttachement item = new ContentTemplateAttachement();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ContentTemplateAttachementID"))) item.ContentTemplateAttachementID = (int)reader["ContentTemplateAttachementID"];
                if (!reader.IsDBNull(reader.GetOrdinal("AttachementDocument"))) item.AttachementDocument = (string)reader["AttachementDocument"];
                if (!reader.IsDBNull(reader.GetOrdinal("AttachementDescription"))) item.AttachementDescription = (string)reader["AttachementDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentTemplateID"))) item.ContentTemplateID = (int)reader["ContentTemplateID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentTemplateAttachementFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateContentTemplateAttachement methods
            
        public virtual void CreateContentTemplateAttachement(ContentTemplateAttachement contentTemplateAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementInsert");
                
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, contentTemplateAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, contentTemplateAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateAttachement.ContentTemplateID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentTemplateAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentTemplateAttachement.ModifiedDate);
                database.AddOutParameter(dbCommand, "@ContentTemplateAttachementID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                contentTemplateAttachement.ContentTemplateAttachementID = (int)database.GetParameterValue(dbCommand, "@ContentTemplateAttachementID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentTemplateAttachementException, ex);
            }
        }

        #endregion

        #region UpdateContentTemplateAttachement methods
        
        public virtual void UpdateContentTemplateAttachement(ContentTemplateAttachement contentTemplateAttachement)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementUpdate");            
                
                database.AddInParameter(dbCommand, "@ContentTemplateAttachementID", DbType.Int32, contentTemplateAttachement.ContentTemplateAttachementID);
                database.AddInParameter(dbCommand, "@AttachementDocument", DbType.String, contentTemplateAttachement.AttachementDocument);
                database.AddInParameter(dbCommand, "@AttachementDescription", DbType.String, contentTemplateAttachement.AttachementDescription);
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateAttachement.ContentTemplateID);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentTemplateAttachement.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentTemplateAttachement.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateContentTemplateAttachementException, ex);
            }
        }
        
        #endregion

        #region DeleteContentTemplateAttachement methods
        public virtual void DeleteContentTemplateAttachement(int contentTemplateAttachementID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementDelete");
                
                database.AddInParameter(dbCommand, "@ContentTemplateAttachementID", DbType.Int32, contentTemplateAttachementID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteContentTemplateAttachementException, ex);
            }
        }

        #endregion

        #region GetContentTemplateAttachement methods
        
        public virtual ContentTemplateAttachement GetContentTemplateAttachement(int contentTemplateAttachementID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementGet");
                
                database.AddInParameter(dbCommand, "@ContentTemplateAttachementID", DbType.Int32, contentTemplateAttachementID);
                
                ContentTemplateAttachement contentTemplateAttachement =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        contentTemplateAttachement = CreateContentTemplateAttachementFromReader(reader);
                        reader.Close();
                    }
                }
                return contentTemplateAttachement;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateAttachementException, ex);
            }
        }
        
        #endregion

        #region GetContentTemplateAttachementList methods
        public virtual ContentTemplateAttachementCollection GetContentTemplateAttachementList(ContentTemplateAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateAttachementGetList");
                
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
        
        public virtual ContentTemplateAttachementCollection GetContentTemplateAttachementList(ContentTemplateAttachementColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetContentTemplateAttachementList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion

       
    }
}