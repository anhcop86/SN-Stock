
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class CustomerTypeService
    {
        #region CustomerType

        public static void CreateCustomerType(CustomerType customerType)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                customerTypeDAO.CreateCustomerType(customerType);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessCreateCustomerTypeException, ex);
            }
        }        

        public static void UpdateCustomerType(CustomerType customerType)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                customerTypeDAO.UpdateCustomerType(customerType);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessUpdateCustomerTypeException, ex);
            }
        }        

        public static void DeleteCustomerType(int typeID)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                customerTypeDAO.DeleteCustomerType(typeID);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessDeleteCustomerTypeException, ex);
            }
        }
        
        public static CustomerType GetCustomerType(int typeID)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                return customerTypeDAO.GetCustomerType(typeID);                
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetCustomerTypeException, ex);
            }
        }                

        public static CustomerTypeCollection GetCustomerTypeList(CustomerTypeColumns orderBy, string orderDirection)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                return customerTypeDAO.GetCustomerTypeList(orderBy, orderDirection);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetCustomerTypeListException, ex);
            }
        }        

        public static CustomerTypeCollection GetCustomerTypeList(CustomerTypeColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                CustomerTypeDAO customerTypeDAO = new CustomerTypeDAO();
                return customerTypeDAO.GetCustomerTypeList(orderBy, orderDirection, page, pageSize, out totalRecords);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetCustomerTypeListException, ex);
            }
        }        

        #endregion
    }
}