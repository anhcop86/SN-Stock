
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using System.IO;

namespace VfsCustomerService.Business
{
    /// <summary>
    /// 
    /// </summary> 
    public class ContentTemplateService
    {
        #region ContentTemplate

        public static void CreateContentTemplate(ContentTemplate contentTemplate)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.CreateContentTemplate(contentTemplate);
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
                throw new ApplicationException(SR.BusinessCreateContentTemplateException, ex);
            }
        }
        
        public static void CreateContentTemplate(ContentTemplate contentTemplate, Stream attchement, string filePath, string fileName, bool keepOrignialFileName)
        {
            try
            {
                string uploadedfileName;
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.CreateContentTemplate(contentTemplate);

                if (attchement.Length > 0)
                {
                    uploadedfileName = Utility.UploadService.UploadDocument(attchement, filePath, fileName, keepOrignialFileName);
                    ContentTemplateAttachement contentTemplateAttachement = new ContentTemplateAttachement();
                    contentTemplateAttachement.AttachementDocument = uploadedfileName;
                    contentTemplateAttachement.ContentTemplateID = contentTemplate.ContentTemplateID;
                    ContentTemplateAttachementService.CreateContentTemplateAttachement(contentTemplateAttachement);
                }
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
                throw new ApplicationException(SR.BusinessCreateContentTemplateException, ex);
            }
        }

        public static void UpdateContentTemplate(ContentTemplate contentTemplate)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.UpdateContentTemplate(contentTemplate);
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
                throw new ApplicationException(SR.BusinessUpdateContentTemplateException, ex);
            }
        }

        public static void UpdateContentTemplate(ContentTemplate contentTemplate, Stream attchement, string filePath, string fileName, bool keepOrignialFileName)
        {
            try
            {
                string uploadedfileName;
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.UpdateContentTemplate(contentTemplate);
                
                if (attchement.Length > 0)
                {
                    uploadedfileName = Utility.UploadService.UploadDocument(attchement, filePath, fileName, keepOrignialFileName);
                    ContentTemplateAttachement contentTemplateAttachement = new ContentTemplateAttachement();
                    contentTemplateAttachement.AttachementDocument = uploadedfileName;
                    contentTemplateAttachement.ContentTemplateID = contentTemplate.ContentTemplateID;
                    ContentTemplateAttachementService.CreateContentTemplateAttachement(contentTemplateAttachement);
                }
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
                throw new ApplicationException(SR.BusinessUpdateContentTemplateException, ex);
            }
        }

        public static void DeleteContentTemplate(int contentTemplateID)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.DeleteContentTemplate(contentTemplateID);                
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
                throw new ApplicationException(SR.BusinessDeleteContentTemplateException, ex);
            }
        }

        public static void DeleteContentTemplateAndAttachement(int contentTemplateID)
        {
            Int32 totalRow;
            try
            {
                ContentTemplateAttachementCollection contentTemplateAttachementCollection = null;

                contentTemplateAttachementCollection = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC", 1, 10000, out totalRow);

                foreach (ContentTemplateAttachement messAttach in contentTemplateAttachementCollection)
                {
                    ContentTemplateAttachementService.DeleteContentTemplateAttachement(messAttach.ContentTemplateAttachementID);
                }

                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                contentTemplateDAO.DeleteContentTemplate(contentTemplateID);
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
                throw new ApplicationException(SR.BusinessDeleteContentTemplateException, ex);
            }
        }    

        public static ContentTemplate GetContentTemplate(int contentTemplateID)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                return contentTemplateDAO.GetContentTemplate(contentTemplateID);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateException, ex);
            }
        }

        public static ContentTemplateCollection GetContentTemplateList(ContentTemplateColumns orderBy, string orderDirection)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                return contentTemplateDAO.GetContentTemplateList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateListException, ex);
            }
        }

        public static ContentTemplateCollection GetContentTemplateList(ContentTemplateColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                return contentTemplateDAO.GetContentTemplateList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateListException, ex);
            }
        }

        #endregion
        
        public static ContentTemplateCollection ExistContentTemplateByContentTemplate(string description)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                return contentTemplateDAO.ExistContentTemplateByContentTemplate(description);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateListException, ex);
            }
        }
        
        public static ContentTemplateCollection ExistsServiceTypeForContentTemplate(int serviceTypeID)
        {
            try
            {
                ContentTemplateDAO contentTemplateDAO = new ContentTemplateDAO();
                return contentTemplateDAO.ExistsServiceTypeForContentTemplate(serviceTypeID);
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
                throw new ApplicationException(SR.BusinessGetContentTemplateListException, ex);
            }
        }

    }
}