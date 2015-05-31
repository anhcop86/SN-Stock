using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public class IndexTestToolService
    {
        public static IndexTestToolCollection GetIndexTestTool(DateTime permDate)
        {
            try
            {
                IndexTestToolDao indexTestToolDao = new IndexTestToolDao();
                return indexTestToolDao.GetIndexTestTool(permDate);
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
