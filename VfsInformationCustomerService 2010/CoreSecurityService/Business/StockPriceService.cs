
using System;
using System.Collections;
using CoreSecurityService.Entities;
using CoreSecurityService.Data;
    
namespace CoreSecurityService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class StockPriceService
    {
        #region StockPrice

        public static void CreateStockPrice(StockPrice stockPrice)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                stockPriceDAO.CreateStockPrice(stockPrice);
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
                throw new ApplicationException(SR.BusinessCreateStockPriceException, ex);
            }
        }        

        public static void UpdateStockPrice(StockPrice stockPrice)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                stockPriceDAO.UpdateStockPrice(stockPrice);
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
                throw new ApplicationException(SR.BusinessUpdateStockPriceException, ex);
            }
        }        

        public static void DeleteStockPrice(string tradingDate, string stockCode, string boardType)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                stockPriceDAO.DeleteStockPrice(tradingDate, stockCode, boardType);
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
                throw new ApplicationException(SR.BusinessDeleteStockPriceException, ex);
            }
        }
        
        public static StockPrice GetStockPrice(string tradingDate, string stockCode, string boardType)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                return stockPriceDAO.GetStockPrice(tradingDate, stockCode, boardType);                
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
                throw new ApplicationException(SR.BusinessGetStockPriceException, ex);
            }
        }                

        public static StockPriceCollection GetStockPriceList(StockPriceColumns orderBy, string orderDirection)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                return stockPriceDAO.GetStockPriceList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetStockPriceListException, ex);
            }
        }        

        public static StockPriceCollection GetStockPriceList(StockPriceColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                StockPriceDAO stockPriceDAO = new StockPriceDAO();
                return stockPriceDAO.GetStockPriceList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetStockPriceListException, ex);
            }
        }        

        #endregion
    }
}