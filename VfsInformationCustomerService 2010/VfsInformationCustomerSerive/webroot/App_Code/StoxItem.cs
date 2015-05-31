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
/// Summary description for StoxItem
/// </summary>

namespace Vfs.WebCrawler.Utility
{
    public class StoxItem
    {
        public StoxItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ArrayList FetchStoxItems()
        {
            ArrayList stoxItems = new ArrayList();    
            stoxItems.Add(new { Name = Resources.UIResource.StoxNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=19", itemId = "19" });                               

            return stoxItems;
        }
    }
}