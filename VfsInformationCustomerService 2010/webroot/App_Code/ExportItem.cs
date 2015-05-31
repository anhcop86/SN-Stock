using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for ExportItem
/// </summary>
/// 
namespace Vfs.WebCrawler.Utility
{
    public class ExportItem
    {
        public ExportItem()
        {
            //
            // TODO: Add constructor logic here
            //


        }

        public static ArrayList FetchExportItems()
    {
        ArrayList mailItems = new ArrayList();
        
        mailItems.Add(new { Name = Resources.UIResource.importUpdateStock, ImageUrl = "_assets/img/mailbox.gif", Link = "ImportUpdateList.aspx", itemId = "9" });        
        mailItems.Add(new { Name = Resources.UIResource.ExportStockPriceHistory, ImageUrl = "_assets/img/mailbox.gif", Link = "ExportList.aspx", itemId = "9" });        
        mailItems.Add(new { Name = Resources.UIResource.StatisticTransaction, ImageUrl = "_assets/img/mailbox.gif", Link = "StatisticTransaction.aspx", itemId = "9" });                
        return mailItems;
    }
    }
}
