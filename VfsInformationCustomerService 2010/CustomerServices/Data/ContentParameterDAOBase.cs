
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class ContentParameterDAOBase
    {
        #region Common methods
        public virtual ContentParameter CreateContentParameterFromReader(IDataReader reader)
        {
            ContentParameter item = new ContentParameter();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ContentParameterID"))) item.ContentParameterID = (int)reader["ContentParameterID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentParameterName"))) item.ContentParameterName = (string)reader["ContentParameterName"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentParameterDescription"))) item.ContentParameterDescription = (string)reader["ContentParameterDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentParameterActive"))) item.ContentParameterActive = (string)reader["ContentParameterActive"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContentParameterValue"))) item.ContentParameterValue = (string)reader["ContentParameterValue"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentParameterFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateContentParameter methods
            
        public virtual void CreateContentParameter(ContentParameter contentParameter)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentParameterInsert");
                
                database.AddInParameter(dbCommand, "@ContentParameterName", DbType.String, contentParameter.ContentParameterName);
                database.AddInParameter(dbCommand, "@ContentParameterDescription", DbType.String, contentParameter.ContentParameterDescription);
                database.AddInParameter(dbCommand, "@ContentParameterActive", DbType.String, contentParameter.ContentParameterActive);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentParameter.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentParameter.ModifiedDate);
                database.AddInParameter(dbCommand, "@ContentParameterValue", DbType.String, contentParameter.ContentParameterValue);
                database.AddOutParameter(dbCommand, "@ContentParameterID", DbType.Int32, 0);
                
                
                database.ExecuteNonQuery(dbCommand);
                contentParameter.ContentParameterID = (int)database.GetParameterValue(dbCommand, "@ContentParameterID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateContentParameterException, ex);
            }
        }

        #endregion

        #region UpdateContentParameter methods
        
        public virtual void UpdateContentParameter(ContentParameter contentParameter)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentParameterUpdate");            
                
                database.AddInParameter(dbCommand, "@ContentParameterID", DbType.Int32, contentParameter.ContentParameterID);
                database.AddInParameter(dbCommand, "@ContentParameterName", DbType.String, contentParameter.ContentParameterName);
                database.AddInParameter(dbCommand, "@ContentParameterDescription", DbType.String, contentParameter.ContentParameterDescription);
                database.AddInParameter(dbCommand, "@ContentParameterActive", DbType.String, contentParameter.ContentParameterActive);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, contentParameter.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, contentParameter.ModifiedDate);
                database.AddInParameter(dbCommand, "@ContentParameterValue", DbType.String, contentParameter.ContentParameterValue);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateContentParameterException, ex);
            }
        }
        
        #endregion

        #region DeleteContentParameter methods
        public virtual void DeleteContentParameter(int contentParameterID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentParameterDelete");
                
                database.AddInParameter(dbCommand, "@ContentParameterID", DbType.Int32, contentParameterID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteContentParameterException, ex);
            }
        }

        #endregion

        #region GetContentParameter methods
        
        public virtual ContentParameter GetContentParameter(int contentParameterID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentParameterGet");
                
                database.AddInParameter(dbCommand, "@ContentParameterID", DbType.Int32, contentParameterID);
                
                ContentParameter contentParameter =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        contentParameter = CreateContentParameterFromReader(reader);
                        reader.Close();
                    }
                }
                return contentParameter;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentParameterException, ex);
            }
        }
        
        #endregion

        #region GetContentParameterList methods
        public virtual ContentParameterCollection GetContentParameterList(ContentParameterColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spContentParameterGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ContentParameterCollection contentParameterCollection = new ContentParameterCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ContentParameter contentParameter = CreateContentParameterFromReader(reader);
                        contentParameterCollection.Add(contentParameter);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return contentParameterCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetContentParameterListException, ex);
            }
        }
        
        public virtual ContentParameterCollection GetContentParameterList(ContentParameterColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetContentParameterList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}