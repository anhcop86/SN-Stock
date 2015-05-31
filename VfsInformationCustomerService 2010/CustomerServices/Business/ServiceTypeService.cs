
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ServiceTypeService
    {
        #region ServiceType

        public static void CreateServiceType(ServiceType serviceType)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                serviceTypeDAO.CreateServiceType(serviceType);
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
                throw new ApplicationException(SR.BusinessCreateServiceTypeException, ex);
            }
        }        

        public static void UpdateServiceType(ServiceType serviceType)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                serviceTypeDAO.UpdateServiceType(serviceType);
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
                throw new ApplicationException(SR.BusinessUpdateServiceTypeException, ex);
            }
        }        

        public static void DeleteServiceType(int serviceTypeID)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                serviceTypeDAO.DeleteServiceType(serviceTypeID);
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
                throw new ApplicationException(SR.BusinessDeleteServiceTypeException, ex);
            }
        }
        
        public static ServiceType GetServiceType(int serviceTypeID)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                return serviceTypeDAO.GetServiceType(serviceTypeID);                
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
                throw new ApplicationException(SR.BusinessGetServiceTypeException, ex);
            }
        }                

        public static ServiceTypeCollection GetServiceTypeList(ServiceTypeColumns orderBy, string orderDirection)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                return serviceTypeDAO.GetServiceTypeList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetServiceTypeListException, ex);
            }
        }        

        public static ServiceTypeCollection GetServiceTypeList(ServiceTypeColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                return serviceTypeDAO.GetServiceTypeList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetServiceTypeListException, ex);
            }
        }        

        #endregion
        public static ServiceTypeCollection ExistServiceTypeIdForServiceType(string ServiceTypeDescription)
        {
            try
            {
                ServiceTypeDAO serviceTypeDAO = new ServiceTypeDAO();
                return serviceTypeDAO.ExistServiceTypeIdForServiceType(ServiceTypeDescription);
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
                throw new ApplicationException(SR.BusinessGetServiceTypeListException, ex);
            }
        }   
    }
}