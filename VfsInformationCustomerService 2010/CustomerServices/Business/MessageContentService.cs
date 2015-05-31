
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class MessageContentService
    {
        #region MessageContent

        public static void CreateMessageContent(MessageContent messageContent)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                messageContentDAO.CreateMessageContent(messageContent);
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
                throw new ApplicationException(SR.BusinessCreateMessageContentException, ex);
            }
        }        

        public static void UpdateMessageContent(MessageContent messageContent)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                messageContentDAO.UpdateMessageContent(messageContent);
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
                throw new ApplicationException(SR.BusinessUpdateMessageContentException, ex);
            }
        }        

        public static void DeleteMessageContent(int messageContentID)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                messageContentDAO.DeleteMessageContent(messageContentID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentException, ex);
            }
        }
        
        public static void DeleteMessageContentAndAttachement(int messageContentID)
        {
            Int32 totalRow = 0;
            try
            {
                MessageContentAttachementCollection messageContentAttachementCollection = null;

                messageContentAttachementCollection = MessageContentAttachementService.GetMessageContentAttachementList(messageContentID, MessageContentAttachementColumns.ModifiedDate, "DESC", 1, 10000, out totalRow);
                
                foreach (MessageContentAttachement messAttach in messageContentAttachementCollection)
                {
                    MessageContentAttachementService.DeleteMessageContentAttachement(messAttach.MessageContentAttachementID);
                }
                
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                messageContentDAO.DeleteMessageContent(messageContentID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentException, ex);
            }
        }
    
        public static MessageContent GetMessageContent(int messageContentID)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContent(messageContentID);                
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
                throw new ApplicationException(SR.BusinessGetMessageContentException, ex);
            }
        }                

        public static MessageContentCollection GetMessageContentList(MessageContentColumns orderBy, string orderDirection)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }        

        public static MessageContentCollection GetMessageContentList(MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContentCollection GetMessageContentList(int status, MessageContentColumns orderBy, string orderDirection)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(status, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContentCollection GetMessageContentList(int status, int serviceType, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(status, serviceType, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContentCollection GetMessageContentList(int status, int serviceType, MessageContentColumns orderBy, string orderDirection)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(status, serviceType, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContentCollection GetMessageContentList(int status, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentList(status, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }        

        #endregion
        public static MessageContentCollection ExistServiceTypeIdForMessageContent(int serviceTypeId)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.ExistServiceTypeIdForMessageContent(serviceTypeId);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }
        
        public static MessageContentCollection ExistsContentTemplateForMessageContent(int ContentTemplateID)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.ExistsContentTemplateForMessageContent(ContentTemplateID);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContentCollection MessageContentGetListFilterByServiceTypeID(int serviceTypeId, string Sender, string Receiver, DateTime FromModifiedDate,DateTime ToModifiedDate, MessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.MessageContentGetListFilterByServiceTypeID(serviceTypeId, Sender, Receiver, FromModifiedDate, ToModifiedDate, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentListException, ex);
            }
        }

        public static MessageContent GetMessageContentIDAndDate(int messageContentID, DateTime modifiedDate)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                return messageContentDAO.GetMessageContentIDAndDate(messageContentID, modifiedDate);
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
                throw new ApplicationException(SR.BusinessGetMessageContentException, ex);
            }
        }

        public static void DeleteMessageContentYear(DateTime modifiedDate)
        {
            try
            {
                MessageContentDAO messageContentDAO = new MessageContentDAO();
                messageContentDAO.DeleteMessageContentYear(modifiedDate);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentException, ex);
            }
        }
    }
}