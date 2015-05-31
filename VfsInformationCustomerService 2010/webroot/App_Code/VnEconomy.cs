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
/// Summary description for VnEconomy
/// </summary>
/// 
namespace Vfs.WebCrawler.Utility
{
    public class VnEconomyItem
    {
        public VnEconomyItem()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public static ArrayList FetchVnEconomyItems()
    {
        ArrayList mailItems = new ArrayList();
        
        mailItems.Add(new { Name = Resources.UIResource.cafeFStockNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=13", itemId = "13" });        
        mailItems.Add(new { Name = Resources.UIResource.vnEconomyFinancialNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=18", itemId = "18" });
        
        return mailItems;
    }
    }
}