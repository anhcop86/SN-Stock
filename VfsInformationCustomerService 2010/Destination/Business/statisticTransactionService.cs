using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public class statisticTransactionService
    {
        public static statisticTransactionCollection ExportStatisticTransaction(DateTime PermDate, string Market)
        {
            try
            {
                statisticTransactionDAO statisticTransactionDAO = new statisticTransactionDAO();
                return statisticTransactionDAO.ExportStatisticTransaction(PermDate, Market);
            }
            catch(Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }
    }
}
