using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for VsdNewItem
/// </summary>
/// 
namespace Vfs.WebCrawler.Utility
{
    public class VsdNewItem
    {
        public VsdNewItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ArrayList FetchVsdNewItemItems()
    {
        ArrayList mailItems = new ArrayList();
    
        mailItems.Add(new { Name = Resources.UIResource.vsdActivityNews, ImageUrl = "_assets/img/mailbox.gif", Link = "NewsList.aspx?linkId=5", itemId = "5" });
        mailItems.Add(new { Name = Resources.UIResource.vsdRegistrationNews, ImageUrl = "_assets/img/inbox.gif", Link = "NewsList.aspx?linkId=6", itemId = "6" });
        mailItems.Add(new { Name = Resources.UIResource.vsdMemberNews, ImageUrl = "_assets/img/drafts.gif", Link = "NewsList.aspx?linkId=7", itemId = "7" });
        mailItems.Add(new { Name = Resources.UIResource.vsdEconomicNews, ImageUrl = "_assets/img/drafts.gif", Link = "NewsList.aspx?linkId=8", itemId = "8" });
        
        return mailItems;
    }
    }
}