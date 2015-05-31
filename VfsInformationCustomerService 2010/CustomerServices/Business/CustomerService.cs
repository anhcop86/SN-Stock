
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class CustomerService
    {
        #region Customer

        public static void CreateCustomer(Customer customer)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.CreateCustomer(customer);
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
                throw new ApplicationException(SR.BusinessCreateCustomerException, ex);
            }
        }        

        public static void UpdateCustomer(Customer customer)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.UpdateCustomer(customer);
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
                throw new ApplicationException(SR.BusinessUpdateCustomerException, ex);
            }
        }        

        public static void DeleteCustomer(string customerId)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.DeleteCustomer(customerId);
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
                throw new ApplicationException(SR.BusinessDeleteCustomerException, ex);
            }
        }
        
        public static Customer GetCustomer(string customerId)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomer(customerId);                
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
                throw new ApplicationException(SR.BusinessGetCustomerException, ex);
            }
        }                

        public static CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }        

        public static CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }

        public static CustomerCollection GetCustomerListSearch(int TypeId, string CustomerId, string CustomerName, string CustomerNameViet, string Email,string mobile, CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerListSearch(TypeId, CustomerId, CustomerName, CustomerNameViet, Email,mobile, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }        

        #endregion

        public static CustomerCollection GetCustomerListByEmail(string email)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerListByEmail(email);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }

        public static CustomerCollection GetCustomerListByBirthDay()
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerList(CustomerColumns.CustomerId, "ASC", DateTime.Now);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }

        public static CustomerCollection GetCustomerListReceiveMessage(CustomerColumns orderBy, string orderDirection)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerListReceiveMessage(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }

        public static CustomerCollection GetCustomerListReceiveMessage(CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                return customerDAO.GetCustomerListReceiveMessage(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }
    }
}