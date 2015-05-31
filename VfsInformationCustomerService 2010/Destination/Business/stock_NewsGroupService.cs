
using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
    
namespace Vfs.WebCrawler.Destination.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class stock_NewsGroupService
    {
        #region stock_NewsGroup

        public static void Createstock_NewsGroup(stock_NewsGroup stock_NewsGroup)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                stock_NewsGroupDAO.Createstock_NewsGroup(stock_NewsGroup);
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
                throw new ApplicationException(SR.BusinessCreatestock_NewsGroupException, ex);
            }
        }        

        public static void Updatestock_NewsGroup(stock_NewsGroup stock_NewsGroup)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                stock_NewsGroupDAO.Updatestock_NewsGroup(stock_NewsGroup);
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
                throw new ApplicationException(SR.BusinessUpdatestock_NewsGroupException, ex);
            }
        }        

        public static void Deletestock_NewsGroup(int id)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                stock_NewsGroupDAO.Deletestock_NewsGroup(id);
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
                throw new ApplicationException(SR.BusinessDeletestock_NewsGroupException, ex);
            }
        }
        
        public static stock_NewsGroup Getstock_NewsGroup(int id)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                return stock_NewsGroupDAO.Getstock_NewsGroup(id);                
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
                throw new ApplicationException(SR.BusinessGetstock_NewsGroupException, ex);
            }
        }                

        public static stock_NewsGroupCollection Getstock_NewsGroupList(stock_NewsGroupColumns orderBy, string orderDirection)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                return stock_NewsGroupDAO.Getstock_NewsGroupList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetstock_NewsGroupListException, ex);
            }
        }        

        public static stock_NewsGroupCollection Getstock_NewsGroupList(Int64 NewsId, stock_NewsGroupColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                return stock_NewsGroupDAO.Getstock_NewsGroupList(NewsId, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetstock_NewsGroupListException, ex);
            }
        }

        public static stock_NewsGroupCollection Getstock_NewsGroupList(Int64 NewsId, stock_NewsGroupColumns orderBy, string orderDirection)
        {
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                return stock_NewsGroupDAO.Getstock_NewsGroupList(NewsId, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetstock_NewsGroupListException, ex);
            }
        }

        public static stock_NewsGroupCollection Getstock_NewsGroupList(stock_NewsGroupColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                stock_NewsGroupDAO stock_NewsGroupDAO = new stock_NewsGroupDAO();
                return stock_NewsGroupDAO.Getstock_NewsGroupList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetstock_NewsGroupListException, ex);
            }
        }        

        #endregion
    }
}