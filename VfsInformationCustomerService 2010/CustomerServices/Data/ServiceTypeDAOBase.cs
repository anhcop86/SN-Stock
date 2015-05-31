
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class ServiceTypeDAOBase
    {
        #region Common methods
        public virtual ServiceType CreateServiceTypeFromReader(IDataReader reader)
        {
            ServiceType item = new ServiceType();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceTypeID"))) item.ServiceTypeID = (int)reader["ServiceTypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("ServiceTypeDescription"))) item.ServiceTypeDescription = (string)reader["ServiceTypeDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateServiceTypeFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateServiceType methods
            
        public virtual void CreateServiceType(ServiceType serviceType)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeInsert");
                
                database.AddInParameter(dbCommand, "@ServiceTypeDescription", DbType.String, serviceType.ServiceTypeDescription);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, serviceType.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, serviceType.ModifiedDate);
                database.AddOutParameter(dbCommand, "@ServiceTypeID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                serviceType.ServiceTypeID = (int)database.GetParameterValue(dbCommand, "@ServiceTypeID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateServiceTypeException, ex);
            }
        }

        #endregion

        #region UpdateServiceType methods
        
        public virtual void UpdateServiceType(ServiceType serviceType)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeUpdate");            
                
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceType.ServiceTypeID);
                database.AddInParameter(dbCommand, "@ServiceTypeDescription", DbType.String, serviceType.ServiceTypeDescription);
                database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, serviceType.CreatedDate);
                database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, serviceType.ModifiedDate);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateServiceTypeException, ex);
            }
        }
        
        #endregion

        #region DeleteServiceType methods
        public virtual void DeleteServiceType(int serviceTypeID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeDelete");
                
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceTypeID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteServiceTypeException, ex);
            }
        }

        #endregion

        #region GetServiceType methods
        
        public virtual ServiceType GetServiceType(int serviceTypeID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeGet");
                
                database.AddInParameter(dbCommand, "@ServiceTypeID", DbType.Int32, serviceTypeID);
                
                ServiceType serviceType =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        serviceType = CreateServiceTypeFromReader(reader);
                        reader.Close();
                    }
                }
                return serviceType;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetServiceTypeException, ex);
            }
        }
        
        #endregion

        #region GetServiceTypeList methods
        public virtual ServiceTypeCollection GetServiceTypeList(ServiceTypeColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spServiceTypeGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                ServiceTypeCollection serviceTypeCollection = new ServiceTypeCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ServiceType serviceType = CreateServiceTypeFromReader(reader);
                        serviceTypeCollection.Add(serviceType);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return serviceTypeCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetServiceTypeListException, ex);
            }
        }
        
        public virtual ServiceTypeCollection GetServiceTypeList(ServiceTypeColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetServiceTypeList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
        #region Extention
        public virtual ServiceTypeCollection ExistServiceTypeIdForServiceType(string ServiceTypeDescription)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExistServiceTypeIdForServiceType");

                database.AddInParameter(dbCommand, "@ServiceTypeDescription", DbType.AnsiString, ServiceTypeDescription.ToString());

                ServiceTypeCollection serviceTypeCollection = new ServiceTypeCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ServiceType serviceType = CreateServiceTypeFromReader(reader);
                        serviceTypeCollection.Add(serviceType);
                    }
                    reader.Close();
                }                
                return serviceTypeCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetServiceTypeListException, ex);
            }
        }
        #endregion
    }
}