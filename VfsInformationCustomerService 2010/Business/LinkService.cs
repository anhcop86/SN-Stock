
using System;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Data;
    
namespace Vfs.WebCrawler.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class LinkService
    {
        #region Link

        public static void CreateLink(Link link)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                linkDAO.CreateLink(link);
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
                throw new ApplicationException(SR.BusinessCreateLinkException, ex);
            }
        }        

        public static void UpdateLink(Link link)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                linkDAO.UpdateLink(link);
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
                throw new ApplicationException(SR.BusinessUpdateLinkException, ex);
            }
        }        

        public static void DeleteLink(int linkId)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                linkDAO.DeleteLink(linkId);
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
                throw new ApplicationException(SR.BusinessDeleteLinkException, ex);
            }
        }
        
        public static Link GetLink(int linkId)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                return linkDAO.GetLink(linkId);                
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
                throw new ApplicationException(SR.BusinessGetLinkException, ex);
            }
        }                

        public static LinkCollection GetLinkList(LinkColumns orderBy, string orderDirection)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                return linkDAO.GetLinkList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetLinkListException, ex);
            }
        }        

        public static LinkCollection GetLinkList(LinkColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                return linkDAO.GetLinkList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetLinkListException, ex);
            }
        }

        public static LinkCollection GetLinkListBySourceId(Int32 sourceId,LinkColumns orderBy, string orderDirection)
        {
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                return linkDAO.GetLinkListBySourceId(sourceId, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetLinkListException, ex);
            }
        }

        public static LinkCollection GetLinkListBySourceId(Int32 sourceId, LinkColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                LinkDAO linkDAO = new LinkDAO();
                return linkDAO.GetLinkListBySourceId(sourceId, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetLinkListException, ex);
            }
        }     
        #endregion
    }
}