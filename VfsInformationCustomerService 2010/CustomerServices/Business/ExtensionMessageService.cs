
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;

namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ExtensionMessageService
    {
        #region ExtensionMessage

        public static void CreateExtensionMessage(ExtensionMessage extensionMessage)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                extensionMessageDAO.CreateExtensionMessage(extensionMessage);
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
                throw new ApplicationException(SR.BusinessCreateExtensionMessageException, ex);
            }
        }        

        public static void UpdateExtensionMessage(ExtensionMessage extensionMessage)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                extensionMessageDAO.UpdateExtensionMessage(extensionMessage);
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
                throw new ApplicationException(SR.BusinessUpdateExtensionMessageException, ex);
            }
        }        

        public static void DeleteExtensionMessage(long extensionMessageID)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                extensionMessageDAO.DeleteExtensionMessage(extensionMessageID);
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
                throw new ApplicationException(SR.BusinessDeleteExtensionMessageException, ex);
            }
        }
        
        public static ExtensionMessage GetExtensionMessage(long extensionMessageID)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                return extensionMessageDAO.GetExtensionMessage(extensionMessageID);                
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageException, ex);
            }
        }                

        public static ExtensionMessageCollection GetExtensionMessageList(ExtensionMessageColumns orderBy, string orderDirection)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                return extensionMessageDAO.GetExtensionMessageList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageListException, ex);
            }
        }        

        public static ExtensionMessageCollection GetExtensionMessageList(ExtensionMessageColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                return extensionMessageDAO.GetExtensionMessageList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageListException, ex);
            }
        }

        public static ExtensionMessageCollection GetExtensionMessageListByTitle(string title, string customerID, ExtensionMessageColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                ExtensionMessageDAO extensionMessageDAO = new ExtensionMessageDAO();
                return extensionMessageDAO.GetExtensionMessageListByTitle(title, customerID, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageListException, ex);
            }
        }      
        #endregion
    }
}