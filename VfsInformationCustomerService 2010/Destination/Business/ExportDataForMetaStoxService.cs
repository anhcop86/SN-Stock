using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public static class ExportDataForMetaStoxService
    {
        public static ExportDataForMetaStoxCollection GetExportDataForMetaStoxList(DateTime PermDate, int marketId)
        {
            try
            {
                ExportDataForMetaStoxDao exportDataForMetaStoxDao = new ExportDataForMetaStoxDao();
                return exportDataForMetaStoxDao.GetExportDataForMetaStoxList(PermDate, marketId);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }
        public static ExportDataForMetaStoxCollection GetExportIndexForMetaStoxList(DateTime PermDate, int marketId)
        {
            try
            {
                ExportDataForMetaStoxDao exportDataForMetaStoxDao = new ExportDataForMetaStoxDao();
                return exportDataForMetaStoxDao.GetExportIndexForMetaStoxList(PermDate, marketId);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }        
    }
}
