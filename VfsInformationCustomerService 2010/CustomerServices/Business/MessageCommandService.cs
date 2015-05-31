
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class MessageCommandService
    {
        #region MessageCommand

        public static void CreateMessageCommand(MessageCommand messageCommand)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                messageCommandDAO.CreateMessageCommand(messageCommand);
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
                throw new ApplicationException(SR.BusinessCreateMessageCommandException, ex);
            }
        }        

        public static void UpdateMessageCommand(MessageCommand messageCommand)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                messageCommandDAO.UpdateMessageCommand(messageCommand);
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
                throw new ApplicationException(SR.BusinessUpdateMessageCommandException, ex);
            }
        }        

        public static void DeleteMessageCommand(int messageCommandID)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                messageCommandDAO.DeleteMessageCommand(messageCommandID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageCommandException, ex);
            }
        }
        
        public static MessageCommand GetMessageCommand(int messageCommandID)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                return messageCommandDAO.GetMessageCommand(messageCommandID);                
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
                throw new ApplicationException(SR.BusinessGetMessageCommandException, ex);
            }
        }                

        public static MessageCommandCollection GetMessageCommandList(MessageCommandColumns orderBy, string orderDirection)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                return messageCommandDAO.GetMessageCommandList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageCommandListException, ex);
            }
        }        

        public static MessageCommandCollection GetMessageCommandList(MessageCommandColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                MessageCommandDAO messageCommandDAO = new MessageCommandDAO();
                return messageCommandDAO.GetMessageCommandList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageCommandListException, ex);
            }
        }        

        #endregion
    }
}