
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace Vfs.WebCrawler.Data
{    
    public abstract class sysdiagramDAOBase
    {
        #region Common methods
        public virtual sysdiagram CreatesysdiagramFromReader(IDataReader reader)
        {
            sysdiagram item = new sysdiagram();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("name"))) item.name = (string)reader["name"];
                if (!reader.IsDBNull(reader.GetOrdinal("principal_id"))) item.principal_id = (int)reader["principal_id"];
                if (!reader.IsDBNull(reader.GetOrdinal("diagram_id"))) item.diagram_id = (int)reader["diagram_id"];
                if (!reader.IsDBNull(reader.GetOrdinal("version"))) item.version = (int)reader["version"];
                if (!reader.IsDBNull(reader.GetOrdinal("definition"))) item.definition = (byte[])reader["definition"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatesysdiagramFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region Createsysdiagram methods
            
        public virtual void Createsysdiagram(sysdiagram sysdiagram)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spsysdiagramsInsert");
                
                database.AddInParameter(dbCommand, "@name", DbType.String, sysdiagram.name);
                database.AddInParameter(dbCommand, "@principal_id", DbType.Int32, sysdiagram.principal_id);
                database.AddInParameter(dbCommand, "@version", DbType.Int32, sysdiagram.version);
                database.AddInParameter(dbCommand, "@definition", DbType.Binary, sysdiagram.definition);
                database.AddOutParameter(dbCommand, "@diagram_id", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                sysdiagram.diagram_id = (int)database.GetParameterValue(dbCommand, "@diagram_id");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatesysdiagramException, ex);
            }
        }

        #endregion

        #region Updatesysdiagram methods
        
        public virtual void Updatesysdiagram(sysdiagram sysdiagram)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spsysdiagramsUpdate");            
                
                database.AddInParameter(dbCommand, "@name", DbType.String, sysdiagram.name);
                database.AddInParameter(dbCommand, "@principal_id", DbType.Int32, sysdiagram.principal_id);
                database.AddInParameter(dbCommand, "@diagram_id", DbType.Int32, sysdiagram.diagram_id);
                database.AddInParameter(dbCommand, "@version", DbType.Int32, sysdiagram.version);
                database.AddInParameter(dbCommand, "@definition", DbType.Binary, sysdiagram.definition);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdatesysdiagramException, ex);
            }
        }
        
        #endregion

        #region Deletesysdiagram methods
        public virtual void Deletesysdiagram(int diagram_id)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spsysdiagramsDelete");
                
                database.AddInParameter(dbCommand, "@diagram_id", DbType.Int32, diagram_id);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletesysdiagramException, ex);
            }
        }

        #endregion

        #region Getsysdiagram methods
        
        public virtual sysdiagram Getsysdiagram(int diagram_id)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spsysdiagramsGet");
                
                database.AddInParameter(dbCommand, "@diagram_id", DbType.Int32, diagram_id);
                
                sysdiagram sysdiagram =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        sysdiagram = CreatesysdiagramFromReader(reader);
                        reader.Close();
                    }
                }
                return sysdiagram;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetsysdiagramException, ex);
            }
        }
        
        #endregion

        #region GetsysdiagramList methods
        public virtual sysdiagramCollection GetsysdiagramList(sysdiagramColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spsysdiagramsGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                sysdiagramCollection sysdiagramCollection = new sysdiagramCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        sysdiagram sysdiagram = CreatesysdiagramFromReader(reader);
                        sysdiagramCollection.Add(sysdiagram);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return sysdiagramCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetsysdiagramListException, ex);
            }
        }
        
        public virtual sysdiagramCollection GetsysdiagramList(sysdiagramColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetsysdiagramList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}