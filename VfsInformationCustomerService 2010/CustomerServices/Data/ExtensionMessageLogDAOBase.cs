
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class ExtensionMessageLogDAOBase
    {
        #region Common methods
        public virtual ExtensionMessageLog CreateExtensionMessageLogFromReader(IDataReader reader)
        {
            ExtensionMessageLog item = new ExtensionMessageLog();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ExtensionMessageLogID"))) item.ExtensionMessageLogID = (long)reader["ExtensionMessageLogID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ExtensionMessageID"))) item.ExtensionMessageID = (long)reader["ExtensionMessageID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerId"))) item.CustomerId = (string)reader["CustomerId"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateExtensionMessageLogFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateExtensionMessageLog methods
            
        public virtual void CreateExtensionMessageLog(ExtensionMessageLog extensionMessageLog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageLogInsert");
                
                database.AddInParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, extensionMessageLog.ExtensionMessageID);
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, extensionMessageLog.CustomerId);
                database.AddOutParameter(dbCommand, "@ExtensionMessageLogID", DbType.Int64, 0);
                
                database.ExecuteNonQuery(dbCommand);
                extensionMessageLog.ExtensionMessageLogID = (long)database.GetParameterValue(dbCommand, "@ExtensionMessageLogID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateExtensionMessageLogException, ex);
            }
        }

        #endregion

        #region UpdateExtensionMessageLog methods
        
        public virtual void UpdateExtensionMessageLog(ExtensionMessageLog extensionMessageLog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageLogUpdate");            
                
                database.AddInParameter(dbCommand, "@ExtensionMessageLogID", DbType.Int64, extensionMessageLog.ExtensionMessageLogID);
                database.AddInParameter(dbCommand, "@ExtensionMessageID", DbType.Int64, extensionMessageLog.ExtensionMessageID);
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, extensionMessageLog.CustomerId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateExtensionMessageLogException, ex);
            }
        }
        
        #endregion

        #region DeleteExtensionMessageLog methods
        public virtual void DeleteExtensionMessageLog(long extensionMessageLogID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageLogDelete");
                
                database.AddInParameter(dbCommand, "@ExtensionMessageLogID", DbType.Int64, extensionMessageLogID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteExtensionMessageLogException, ex);
            }
        }

        #endregion

        #region GetExtensionMessageLog methods
        
        public virtual ExtensionMessageLog GetExtensionMessageLog(long extensionMessageLogID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageLogGet");
                
                database.AddInParameter(dbCommand, "@ExtensionMessageLogID", DbType.Int64, extensionMessageLogID);
                
                ExtensionMessageLog extensionMessageLog =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        extensionMessageLog = CreateExtensionMessageLogFromReader(reader);
                        reader.Close();
                    }
                }
                return extensionMessageLog;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetExtensionMessageLogException, ex);
            }
        }
        
        #endregion

        #region GetExtensionMessageLogList methods
        public virtual ExtensionMessageLogCollection GetExtensionMessageLogList(ExtensionMessageLogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExtensionMessageLogGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ExtensionMessageLogCollection extensionMessageLogCollection = new ExtensionMessageLogCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ExtensionMessageLog extensionMessageLog = CreateExtensionMessageLogFromReader(reader);
                        extensionMessageLogCollection.Add(extensionMessageLog);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return extensionMessageLogCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetExtensionMessageLogListException, ex);
            }
        }
        
        public virtual ExtensionMessageLogCollection GetExtensionMessageLogList(ExtensionMessageLogColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetExtensionMessageLogList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}