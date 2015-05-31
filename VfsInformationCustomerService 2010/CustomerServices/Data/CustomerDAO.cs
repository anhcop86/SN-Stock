using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{	
	public class CustomerDAO : CustomerDAOBase
	{
		public CustomerDAO()
		{
		}

        #region GetCustomer methods

        public virtual CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords, DateTime birthDay)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerGetListByBirthDay");
                
                database.AddInParameter(dbCommand, "@BirthDay", DbType.DateTime, birthDay);
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                CustomerCollection customerCollection = new CustomerCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer customer = CreateCustomerFromReader(reader);
                        customerCollection.Add(customer);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return customerCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }

        public virtual CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection, DateTime birthDay)
        {
            int totalRecords = 0;
            return GetCustomerList(orderBy, orderDirection, 0, 0, out totalRecords, birthDay);
        }  
      
        #endregion

        #region GetCustomer receive messages  methods
        public virtual CustomerCollection GetCustomerListReceiveMessage(CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerGetListReceiveMsg");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                CustomerCollection customerCollection = new CustomerCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer customer = CreateCustomerFromReader(reader);
                        customerCollection.Add(customer);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return customerCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }

        public virtual CustomerCollection GetCustomerListReceiveMessage(CustomerColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetCustomerListReceiveMessage(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
	}
}
