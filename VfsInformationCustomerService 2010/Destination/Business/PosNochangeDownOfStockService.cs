using System;
using System.Collections.Generic;
using System.Text;

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public class PosNochangeDownOfStockService
    {
        public static PosNochangeDownOfStockCollection GetPosNochangeDownOfStockCollection(DateTime fromDate, DateTime toDate)
        {
            try
            {
                PosNochangeDownOfStockDAO posNochangeDownOfStockDAO = new PosNochangeDownOfStockDAO();
                return posNochangeDownOfStockDAO.GetPosNochangeDownOfStockCollection(fromDate, toDate);
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

    }
}
