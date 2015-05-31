using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public class SymbolPermLongTestToolService
    {
        public static SymbolPermLongTestToolCollection SymbolPermLongGetListTestTool(DateTime fromDate, DateTime toDate, int countAVG)
        {
            try
            {
                SymbolPermLongTestToolDao symbolPermLongTestToolDaoBase = new SymbolPermLongTestToolDao();
                return symbolPermLongTestToolDaoBase.SymbolPermLongGetListTestTool(fromDate, toDate, countAVG);
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
