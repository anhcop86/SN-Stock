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
        
            mailItems.Add(new { Name = Resources.UIResource.ImportUpdateStock, ImageUrl = "_assets/img/mailbox.gif", Link = "ImportListUpdate.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.ImportUpdateStockForeign, ImageUrl = "_assets/img/mailbox.gif", Link = "ImportListUpdateForeign.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.ExportStockPriceHistory, ImageUrl = "_assets/img/mailbox.gif", Link = "ExportList.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.StatisticTransaction, ImageUrl = "_assets/img/mailbox.gif", Link = "StatisticTransaction.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.Snapshot, ImageUrl = "_assets/img/mailbox.gif", Link = "SnapShot.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.UpdatePrice, ImageUrl = "_assets/img/mailbox.gif", Link = "UpdatePrice.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.SessionCompanyTitle, ImageUrl = "_assets/img/mailbox.gif", Link = "SessionCompany.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.infoPosNochangDown, ImageUrl = "_assets/img/mailbox.gif", Link = "InfoPosNochangeDownOfStock.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.GetAutoPrice, ImageUrl = "_assets/img/mailbox.gif", Link = "TestTool.aspx", itemId = "9" });
            mailItems.Add(new { Name = Resources.UIResource.ExportDataForStox, ImageUrl = "_assets/img/mailbox.gif", Link = "ExportDataForMetaStox.aspx", itemId = "9" });
                
            return mailItems;
        }
    }
}
