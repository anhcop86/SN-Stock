
using System;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Data;
    
namespace Vfs.WebCrawler.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class SourceService
    {
        #region Source

        public static void CreateSource(Source source)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                sourceDAO.CreateSource(source);
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
                throw new ApplicationException(SR.BusinessCreateSourceException, ex);
            }
        }        

        public static void UpdateSource(Source source)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                sourceDAO.UpdateSource(source);
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
                throw new ApplicationException(SR.BusinessUpdateSourceException, ex);
            }
        }        

        public static void DeleteSource(int sourceId)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                sourceDAO.DeleteSource(sourceId);
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
                throw new ApplicationException(SR.BusinessDeleteSourceException, ex);
            }
        }
        
        public static Source GetSource(int sourceId)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                return sourceDAO.GetSource(sourceId);                
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
                throw new ApplicationException(SR.BusinessGetSourceException, ex);
            }
        }                

        public static SourceCollection GetSourceList(SourceColumns orderBy, string orderDirection)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                return sourceDAO.GetSourceList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetSourceListException, ex);
            }
        }        

        public static SourceCollection GetSourceList(SourceColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                SourceDAO sourceDAO = new SourceDAO();
                return sourceDAO.GetSourceList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetSourceListException, ex);
            }
        }        

        #endregion
    }
}