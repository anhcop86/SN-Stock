
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace Vfs.WebCrawler.Data
{    
    public abstract class SourceDAOBase
    {
        #region Common methods
        public virtual Source CreateSourceFromReader(IDataReader reader)
        {
            Source item = new Source();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("SourceId"))) item.SourceId = (int)reader["SourceId"];
                if (!reader.IsDBNull(reader.GetOrdinal("SiteName"))) item.SiteName = (string)reader["SiteName"];
                if (!reader.IsDBNull(reader.GetOrdinal("URL"))) item.URL = (string)reader["URL"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateSourceFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateSource methods
            
        public virtual void CreateSource(Source source)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spSourceInsert");
                
                database.AddInParameter(dbCommand, "@SiteName", DbType.String, source.SiteName);
                database.AddInParameter(dbCommand, "@URL", DbType.String, source.URL);
                database.AddOutParameter(dbCommand, "@SourceId", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                source.SourceId = (int)database.GetParameterValue(dbCommand, "@SourceId");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateSourceException, ex);
            }
        }

        #endregion

        #region UpdateSource methods
        
        public virtual void UpdateSource(Source source)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spSourceUpdate");            
                
                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, source.SourceId);
                database.AddInParameter(dbCommand, "@SiteName", DbType.String, source.SiteName);
                database.AddInParameter(dbCommand, "@URL", DbType.String, source.URL);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateSourceException, ex);
            }
        }
        
        #endregion

        #region DeleteSource methods
        public virtual void DeleteSource(int sourceId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spSourceDelete");
                
                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, sourceId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteSourceException, ex);
            }
        }

        #endregion

        #region GetSource methods
        
        public virtual Source GetSource(int sourceId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spSourceGet");
                
                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, sourceId);
                
                Source source =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        source = CreateSourceFromReader(reader);
                        reader.Close();
                    }
                }
                return source;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetSourceException, ex);
            }
        }
        
        #endregion

        #region GetSourceList methods
        public virtual SourceCollection GetSourceList(SourceColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spSourceGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                SourceCollection sourceCollection = new SourceCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Source source = CreateSourceFromReader(reader);
                        sourceCollection.Add(source);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return sourceCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetSourceListException, ex);
            }
        }
        
        public virtual SourceCollection GetSourceList(SourceColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetSourceList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}