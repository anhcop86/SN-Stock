
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ContentParameterService
    {
        #region ContentParameter

        public static void CreateContentParameter(ContentParameter contentParameter)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                contentParameterDAO.CreateContentParameter(contentParameter);
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
                throw new ApplicationException(SR.BusinessCreateContentParameterException, ex);
            }
        }        

        public static void UpdateContentParameter(ContentParameter contentParameter)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                contentParameterDAO.UpdateContentParameter(contentParameter);
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
                throw new ApplicationException(SR.BusinessUpdateContentParameterException, ex);
            }
        }        

        public static void DeleteContentParameter(int contentParameterID)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                contentParameterDAO.DeleteContentParameter(contentParameterID);
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
                throw new ApplicationException(SR.BusinessDeleteContentParameterException, ex);
            }
        }
        
        public static ContentParameter GetContentParameter(int contentParameterID)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                return contentParameterDAO.GetContentParameter(contentParameterID);                
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
                throw new ApplicationException(SR.BusinessGetContentParameterException, ex);
            }
        }                

        public static ContentParameterCollection GetContentParameterList(ContentParameterColumns orderBy, string orderDirection)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                return contentParameterDAO.GetContentParameterList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetContentParameterListException, ex);
            }
        }        

        public static ContentParameterCollection GetContentParameterList(ContentParameterColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ContentParameterDAO contentParameterDAO = new ContentParameterDAO();
                return contentParameterDAO.GetContentParameterList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetContentParameterListException, ex);
            }
        }        

        #endregion
    }
}