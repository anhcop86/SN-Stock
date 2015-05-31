
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class ContentTemplateDAOBase
    {
        #region Common methods
        public virtual ContentTemplate CreateContentTemplateFromReader(IDataReader reader)
        {
            ContentTemplate item = new ContentTemplate();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ContentTemplateID"))) item.ContentTemplateID = (int)reader["ContentTemplateID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceTypeID"))) item.ServiceTypeID = (int)reader["ServiceTypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Description"))) item.Description = (string)reader["Description"];
                if (!reader.IsDBNull(reader.GetOrdinal("Sender"))) item.Sender = (string)reader["Sender"];
                if (!reader.IsDBNull(reader.GetOrdinal("Receiver"))) item.Receiver = (string)reader["Receiver"];
                if (!reader.IsDBNull(reader.GetOrdinal("Subject"))) item.Subject = (string)reader["Subject"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyContentType"))) item.BodyContentType = (string)reader["BodyContentType"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyEncoding"))) item.BodyEncoding = (string)reader["BodyEncoding"];
                if (!reader.IsDBNull(reader.GetOrdinal("BodyMessage"))) item.BodyMessage = (string)reader["BodyMessage"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentTemplateFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateContentTemplate methods
            
        public virtual void CreateContentTemplate(ContentTemplate contentTemplate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateInsert");
                
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, contentTemplate.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Description", DbType.String, contentTemplate.Description);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, contentTemplate.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, contentTemplate.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, contentTemplate.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, contentTemplate.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, contentTemplate.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, contentTemplate.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentTemplate.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentTemplate.ModifiedDate);
                database.AddOutParameter(dbCommand, "@ContentTemplateID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                contentTemplate.ContentTemplateID = (int)database.GetParameterValue(dbCommand, "@ContentTemplateID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentTemplateException, ex);
            }
        }

        #endregion

        #region UpdateContentTemplate methods
        
        public virtual void UpdateContentTemplate(ContentTemplate contentTemplate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateUpdate");            
                
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplate.ContentTemplateID);
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, contentTemplate.ServiceTypeID);
                database.AddInParameter(dbCommand, "@Description", DbType.String, contentTemplate.Description);
                database.AddInParameter(dbCommand, "@Sender", DbType.String, contentTemplate.Sender);
                database.AddInParameter(dbCommand, "@Receiver", DbType.String, contentTemplate.Receiver);
                database.AddInParameter(dbCommand, "@Subject", DbType.String, contentTemplate.Subject);
                database.AddInParameter(dbCommand, "@BodyContentType", DbType.String, contentTemplate.BodyContentType);
                database.AddInParameter(dbCommand, "@BodyEncoding", DbType.String, contentTemplate.BodyEncoding);
                database.AddInParameter(dbCommand, "@BodyMessage", DbType.String, contentTemplate.BodyMessage);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentTemplate.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentTemplate.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateContentTemplateException, ex);
            }
        }
        
        #endregion

        #region DeleteContentTemplate methods
        public virtual void DeleteContentTemplate(int contentTemplateID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateDelete");
                
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteContentTemplateException, ex);
            }
        }

        #endregion

        #region GetContentTemplate methods
        
        public virtual ContentTemplate GetContentTemplate(int contentTemplateID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateGet");
                
                database.AddInParameter(dbCommand, "@ContentTemplateID", DbType.Int32, contentTemplateID);
                
                ContentTemplate contentTemplate =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        contentTemplate = CreateContentTemplateFromReader(reader);
                        reader.Close();
                    }
                }
                return contentTemplate;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateException, ex);
            }
        }
        
        #endregion

        #region GetContentTemplateList methods
        public virtual ContentTemplateCollection GetContentTemplateList(ContentTemplateColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentTemplateGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ContentTemplateCollection contentTemplateCollection = new ContentTemplateCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentTemplate contentTemplate = CreateContentTemplateFromReader(reader);
                        contentTemplateCollection.Add(contentTemplate);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return contentTemplateCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateListException, ex);
            }
        }
        
        public virtual ContentTemplateCollection GetContentTemplateList(ContentTemplateColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetContentTemplateList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
        public virtual ContentTemplateCollection ExistContentTemplateByContentTemplate(string description)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExistContentTemplateByContentTemplate");

                database.AddInParameter(dbCommand, "@Description", DbType.String, description.ToString());

                ContentTemplateCollection contentTemplateCollection = new ContentTemplateCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentTemplate contentTemplate = CreateContentTemplateFromReader(reader);
                        contentTemplateCollection.Add(contentTemplate);
                    }
                    reader.Close();
                }
                return contentTemplateCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateListException, ex);
            }
        }
        public virtual ContentTemplateCollection ExistsServiceTypeForContentTemplate(int serviceTypeID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeForContentTemplate");

                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceTypeID);

                ContentTemplateCollection contentTemplateCollection = new ContentTemplateCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentTemplate contentTemplate = CreateContentTemplateFromReader(reader);
                        contentTemplateCollection.Add(contentTemplate);
                    }
                    reader.Close();
                }
                return contentTemplateCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentTemplateListException, ex);
            }
        }
    }
}