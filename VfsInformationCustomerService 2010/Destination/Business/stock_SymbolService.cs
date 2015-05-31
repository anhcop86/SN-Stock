
using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
    
namespace Vfs.WebCrawler.Destination.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class stock_SymbolService
    {
        #region stock_Symbol

        public static void Createstock_Symbol(stock_Symbol stock_Symbol)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                stock_SymbolDAO.Createstock_Symbol(stock_Symbol);
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
                throw new ApplicationException(SR.BusinessCreatestock_SymbolException, ex);
            }
        }        

        public static void Updatestock_Symbol(stock_Symbol stock_Symbol)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                stock_SymbolDAO.Updatestock_Symbol(stock_Symbol);
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
                throw new ApplicationException(SR.BusinessUpdatestock_SymbolException, ex);
            }
        }        

        public static void Deletestock_Symbol(int symbolID)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                stock_SymbolDAO.Deletestock_Symbol(symbolID);
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
                throw new ApplicationException(SR.BusinessDeletestock_SymbolException, ex);
            }
        }
        
        public static stock_Symbol Getstock_Symbol(int symbolID)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                return stock_SymbolDAO.Getstock_Symbol(symbolID);                
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolException, ex);
            }
        }                

        public static stock_SymbolCollection Getstock_SymbolList(stock_SymbolColumns orderBy, string orderDirection)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                return stock_SymbolDAO.Getstock_SymbolList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolListException, ex);
            }
        }        

        public static stock_SymbolCollection Getstock_SymbolList(stock_SymbolColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                stock_SymbolDAO stock_SymbolDAO = new stock_SymbolDAO();
                return stock_SymbolDAO.Getstock_SymbolList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolListException, ex);
            }
        }        

        #endregion
    }
}