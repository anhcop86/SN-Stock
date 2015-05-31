
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class BirthdayMessageLogDAOBase
    {
        #region Common methods
        public virtual BirthdayMessageLog CreateBirthdayMessageLogFromReader(IDataReader reader)
        {
            BirthdayMessageLog item = new BirthdayMessageLog();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("BirthdayMessageDay"))) item.BirthdayMessageDay = (string)reader["BirthdayMessageDay"];
                if (!reader.IsDBNull(reader.GetOrdinal("ProccessYN"))) item.ProccessYN = (string)reader["ProccessYN"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateBirthdayMessageLogFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateBirthdayMessageLog methods
            
        public virtual void CreateBirthdayMessageLog(BirthdayMessageLog birthdayMessageLog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spBirthdayMessageLogInsert");
                
                database.AddInParameter(dbCommand, "@BirthdayMessageDay", DbType.String, birthdayMessageLog.BirthdayMessageDay);
                database.AddInParameter(dbCommand, "@ProccessYN", DbType.String, birthdayMessageLog.ProccessYN);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateBirthdayMessageLogException, ex);
            }
        }

        #endregion

        #region UpdateBirthdayMessageLog methods
        
        public virtual void UpdateBirthdayMessageLog(BirthdayMessageLog birthdayMessageLog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spBirthdayMessageLogUpdate");            
                
                database.AddInParameter(dbCommand, "@BirthdayMessageDay", DbType.String, birthdayMessageLog.BirthdayMessageDay);
                database.AddInParameter(dbCommand, "@ProccessYN", DbType.String, birthdayMessageLog.ProccessYN);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateBirthdayMessageLogException, ex);
            }
        }
        
        #endregion

        #region DeleteBirthdayMessageLog methods
        public virtual void DeleteBirthdayMessageLog(string birthdayMessageDay)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spBirthdayMessageLogDelete");
                
                database.AddInParameter(dbCommand, "@BirthdayMessageDay", DbType.String, birthdayMessageDay);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteBirthdayMessageLogException, ex);
            }
        }

        #endregion

        #region GetBirthdayMessageLog methods
        
        public virtual BirthdayMessageLog GetBirthdayMessageLog(string birthdayMessageDay)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spBirthdayMessageLogGet");
                
                database.AddInParameter(dbCommand, "@BirthdayMessageDay", DbType.String, birthdayMessageDay);
                
                BirthdayMessageLog birthdayMessageLog =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        birthdayMessageLog = CreateBirthdayMessageLogFromReader(reader);
                        reader.Close();
                    }
                }
                return birthdayMessageLog;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetBirthdayMessageLogException, ex);
            }
        }
        
        #endregion

        #region GetBirthdayMessageLogList methods
        public virtual BirthdayMessageLogCollection GetBirthdayMessageLogList(BirthdayMessageLogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spBirthdayMessageLogGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                BirthdayMessageLogCollection birthdayMessageLogCollection = new BirthdayMessageLogCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        BirthdayMessageLog birthdayMessageLog = CreateBirthdayMessageLogFromReader(reader);
                        birthdayMessageLogCollection.Add(birthdayMessageLog);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return birthdayMessageLogCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetBirthdayMessageLogListException, ex);
            }
        }
        
        public virtual BirthdayMessageLogCollection GetBirthdayMessageLogList(BirthdayMessageLogColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetBirthdayMessageLogList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}