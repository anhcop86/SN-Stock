
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ContentTemplateAttachementService
    {
        #region ContentTemplateAttachement

        public static void CreateContentTemplateAttachement(ContentTemplateAttachement contentTemplateAttachement)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                contentTemplateAttachementDAO.CreateContentTemplateAttachement(contentTemplateAttachement);
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
                throw new ApplicationException(SR.BusinessCreateContentTemplateAttachementException, ex);
            }
        }        

        public static void UpdateContentTemplateAttachement(ContentTemplateAttachement contentTemplateAttachement)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                contentTemplateAttachementDAO.UpdateContentTemplateAttachement(contentTemplateAttachement);
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
                throw new ApplicationException(SR.BusinessUpdateContentTemplateAttachementException, ex);
            }
        }        

        public static void DeleteContentTemplateAttachement(int contentTemplateAttachementID)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                contentTemplateAttachementDAO.DeleteContentTemplateAttachement(contentTemplateAttachementID);
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
                throw new ApplicationException(SR.BusinessDeleteContentTemplateAttachementException, ex);
            }
        }
        
        public static ContentTemplateAttachement GetContentTemplateAttachement(int contentTemplateAttachementID)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.GetContentTemplateAttachement(contentTemplateAttachementID);                
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementException, ex);
            }
        }                

        public static ContentTemplateAttachementCollection GetContentTemplateAttachementList(ContentTemplateAttachementColumns orderBy, string orderDirection)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.GetContentTemplateAttachementList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementListException, ex);
            }
        }        

        public static ContentTemplateAttachementCollection GetContentTemplateAttachementList(ContentTemplateAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.GetContentTemplateAttachementList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementListException, ex);
            }
        }

        public static ContentTemplateAttachementCollection GetContentTemplateAttachementList(Int32 contentTemplateId, ContentTemplateAttachementColumns orderBy, string orderDirection)
        {
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.GetContentTemplateAttachementList(contentTemplateId, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementListException, ex);
            }
        }

        public static ContentTemplateAttachementCollection GetContentTemplateAttachementList(Int32 contentTemplateId, ContentTemplateAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.GetContentTemplateAttachementList(contentTemplateId, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementListException, ex);
            }
        }
        public static ContentTemplateAttachementCollection ExistsContentTemplateByContentTemplateAttachementGet(int contentTemplateID)
        {
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                return contentTemplateAttachementDAO.ExistsContentTemplateByContentTemplateAttachementGet(contentTemplateID);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateAttachementListException, ex);
            }
        }
        public static void DeleteContentTemplateAttachementForContentTemplate(int contentTemplateID)
        {
            try
            {
                ContentTemplateAttachementDAO contentTemplateAttachementDAO = new ContentTemplateAttachementDAO();
                //contentTemplateAttachementDAO.DeleteContentTemplateAttachementForContentTemplate(contentTemplateID);
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
                throw new ApplicationException(SR.BusinessDeleteContentTemplateAttachementException, ex);
            }
        }
        #endregion
    }
}