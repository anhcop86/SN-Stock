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
/// Summary description for CafefNewItem
/// </summary>

namespace Vfs.WebCrawler.Utility
{
    public class CafefNewItem
    {
        public CafefNewItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ArrayList FetchCafefItems()
        {
            ArrayList mailItems = new ArrayList();
    
            mailItems.Add(new { Name = Resources.UIResource.CafeFStockNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=9", itemId = "9" });        
            mailItems.Add(new { Name = Resources.UIResource.CafeFBankNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=14", itemId = "14" });        
            mailItems.Add(new { Name = Resources.UIResource.CafeFInternationalFinancialNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=15", itemId = "15" });        
            mailItems.Add(new { Name = Resources.UIResource.CafeFCompanyNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=16", itemId = "16" });
            mailItems.Add(new { Name = Resources.UIResource.CafeFRealEstateNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=17", itemId = "17" });

            return mailItems;
        }
    }
}