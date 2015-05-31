
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;

namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ExtensionMessageLogService
    {
        #region ExtensionMessageLog

        public static void CreateExtensionMessageLog(ExtensionMessageLog extensionMessageLog)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                extensionMessageLogDAO.CreateExtensionMessageLog(extensionMessageLog);
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
                throw new ApplicationException(SR.BusinessCreateExtensionMessageLogException, ex);
            }
        }        

        public static void UpdateExtensionMessageLog(ExtensionMessageLog extensionMessageLog)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                extensionMessageLogDAO.UpdateExtensionMessageLog(extensionMessageLog);
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
                throw new ApplicationException(SR.BusinessUpdateExtensionMessageLogException, ex);
            }
        }        

        public static void DeleteExtensionMessageLog(long extensionMessageLogID)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                extensionMessageLogDAO.DeleteExtensionMessageLog(extensionMessageLogID);
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
                throw new ApplicationException(SR.BusinessDeleteExtensionMessageLogException, ex);
            }
        }
        
        public static ExtensionMessageLog GetExtensionMessageLog(long extensionMessageLogID)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                return extensionMessageLogDAO.GetExtensionMessageLog(extensionMessageLogID);                
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageLogException, ex);
            }
        }                

        public static ExtensionMessageLogCollection GetExtensionMessageLogList(ExtensionMessageLogColumns orderBy, string orderDirection)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                return extensionMessageLogDAO.GetExtensionMessageLogList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageLogListException, ex);
            }
        }        

        public static ExtensionMessageLogCollection GetExtensionMessageLogList(ExtensionMessageLogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ExtensionMessageLogDAO extensionMessageLogDAO = new ExtensionMessageLogDAO();
                return extensionMessageLogDAO.GetExtensionMessageLogList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetExtensionMessageLogListException, ex);
            }
        }        

        #endregion
    }
}