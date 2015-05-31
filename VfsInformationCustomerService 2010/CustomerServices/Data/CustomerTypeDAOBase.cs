
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class CustomerTypeDAOBase
    {
        #region Common methods
        public virtual CustomerType CreateCustomerTypeFromReader(IDataReader reader)
        {
            CustomerType item = new CustomerType();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("TypeID"))) item.TypeID = (int)reader["TypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Description"))) item.Description = (string)reader["Description"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateCustomerTypeFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateCustomerType methods
            
        public virtual void CreateCustomerType(CustomerType customerType)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerTypeInsert");
                
                database.AddInParameter(dbCommand, "@Description", DbType.String, customerType.Description);
                database.AddOutParameter(dbCommand, "@TypeID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                customerType.TypeID = (int)database.GetParameterValue(dbCommand, "@TypeID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateCustomerTypeException, ex);
            }
        }

        #endregion

        #region UpdateCustomerType methods
        
        public virtual void UpdateCustomerType(CustomerType customerType)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerTypeUpdate");            
                
                database.AddInParameter(dbCommand, "@TypeID", DbType.Int32, customerType.TypeID);
                database.AddInParameter(dbCommand, "@Description", DbType.String, customerType.Description);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateCustomerTypeException, ex);
            }
        }
        
        #endregion

        #region DeleteCustomerType methods
        public virtual void DeleteCustomerType(int typeID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerTypeDelete");
                
                database.AddInParameter(dbCommand, "@TypeID", DbType.Int32, typeID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteCustomerTypeException, ex);
            }
        }

        #endregion

        #region GetCustomerType methods
        
        public virtual CustomerType GetCustomerType(int typeID)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerTypeGet");
                
                database.AddInParameter(dbCommand, "@TypeID", DbType.Int32, typeID);
                
                CustomerType customerType =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        customerType = CreateCustomerTypeFromReader(reader);
                        reader.Close();
                    }
                }
                return customerType;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerTypeException, ex);
            }
        }
        
        #endregion

        #region GetCustomerTypeList methods
        public virtual CustomerTypeCollection GetCustomerTypeList(CustomerTypeColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerTypeGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                CustomerTypeCollection customerTypeCollection = new CustomerTypeCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        CustomerType customerType = CreateCustomerTypeFromReader(reader);
                        customerTypeCollection.Add(customerType);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return customerTypeCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerTypeListException, ex);
            }
        }
        
        public virtual CustomerTypeCollection GetCustomerTypeList(CustomerTypeColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetCustomerTypeList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}