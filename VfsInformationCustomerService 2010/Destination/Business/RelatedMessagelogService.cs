
using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class RelatedMessagelogService
    {
        #region RelatedMessagelog

        public static void CreateRelatedMessagelog(RelatedMessagelog relatedMessagelog)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                relatedMessagelogDAO.CreateRelatedMessagelog(relatedMessagelog);
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
                throw new ApplicationException(SR.BusinessCreateRelatedMessagelogException, ex);
            }
        }
        public static void CreateRelatedMessagelog2(RelatedMessagelog relatedMessagelog)
        {
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                relatedMessagelogDAO.CreateRelatedMessagelog2(relatedMessagelog);
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
                throw new ApplicationException(SR.BusinessCreateRelatedMessagelogException, ex);
            }
        }        

        public static void UpdateRelatedMessagelog(RelatedMessagelog relatedMessagelog)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                relatedMessagelogDAO.UpdateRelatedMessagelog(relatedMessagelog);
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
                throw new ApplicationException(SR.BusinessUpdateRelatedMessagelogException, ex);
            }
        }        

        public static void DeleteRelatedMessagelog(long relatedMessagelogID)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                relatedMessagelogDAO.DeleteRelatedMessagelog(relatedMessagelogID);
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
                throw new ApplicationException(SR.BusinessDeleteRelatedMessagelogException, ex);
            }
        }
        
        public static RelatedMessagelog GetRelatedMessagelog(long relatedMessagelogID)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                return relatedMessagelogDAO.GetRelatedMessagelog(relatedMessagelogID);                
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
                throw new ApplicationException(SR.BusinessGetRelatedMessagelogException, ex);
            }
        }                

        public static RelatedMessagelogCollection GetRelatedMessagelogList(RelatedMessagelogColumns orderBy, string orderDirection)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                return relatedMessagelogDAO.GetRelatedMessagelogList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetRelatedMessagelogListException, ex);
            }
        }        

        public static RelatedMessagelogCollection GetRelatedMessagelogList(RelatedMessagelogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                RelatedMessagelogDAO relatedMessagelogDAO = new RelatedMessagelogDAO();
                return relatedMessagelogDAO.GetRelatedMessagelogList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetRelatedMessagelogListException, ex);
            }
        }        

        #endregion
    }
}