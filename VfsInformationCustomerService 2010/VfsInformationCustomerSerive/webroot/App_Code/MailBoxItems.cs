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

namespace Vfs.WebCrawler.Utility
{
    public class MailBoxItems
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ArrayList FetchMailItems()
    {
        ArrayList mailItems = new ArrayList();
        mailItems.Add(new { Name = Resources.UIResource.NewsFromTradingCenter, ImageUrl = "_assets/img/mailbox.gif", Link= "NewsList.aspx?linkId=1", itemId = "1" });
        mailItems.Add(new { Name = Resources.UIResource.NewsIssuedOwner, ImageUrl = "_assets/img/inbox.gif", Link= "NewsList.aspx?linkId=2", itemId = "2" });
        mailItems.Add(new { Name = Resources.UIResource.NewsSecuritiesCompany, ImageUrl = "_assets/img/drafts.gif", Link= "NewsList.aspx?linkId=3", itemId = "3" });
        mailItems.Add(new { Name = Resources.UIResource.OtherNews, ImageUrl = "_assets/img/outbox.gif", Link= "NewsList.aspx?linkId=4", itemId = "4" });
        /*mailItems.Add(new { Name = "Junk Mail", ImageUrl = "_assets/img/junk.gif" });
        mailItems.Add(new { Name = "Deleted Items", ImageUrl = "_assets/img/deleted.gif" });
        mailItems.Add(new { Name = "Search Folders", ImageUrl = "_assets/img/search.gif" });
        mailItems.Add(new { Name = "Sent Items", ImageUrl = "_assets/img/sentitems.gif" });*/ 

        return mailItems;
    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ArrayList FetchNoteItems()
    {
        ArrayList mailItems = new ArrayList();
        
        mailItems.Add(new { Name = Resources.UIResource.NewsFromHastcTradingCenter, ImageUrl = "_assets/img/mailbox.gif", Link= "NewsList.aspx?linkId=10", itemId = "10" });
        mailItems.Add(new { Name = Resources.UIResource.NewsHastcIssuedOwner, ImageUrl = "_assets/img/inbox.gif",  Link= "NewsList.aspx?linkId=11", itemId = "11" });
        mailItems.Add(new { Name = Resources.UIResource.NewsHastcSecuritiesCompany, ImageUrl = "_assets/img/drafts.gif", Link= "NewsList.aspx?linkId=12", itemId = "12" });
        //mailItems.Add(new { Name = Resources.UIResource.HastcBondNews, ImageUrl = "_assets/img/drafts.gif" });
        //mailItems.Add(new { Name = Resources.UIResource.HastcBondTransaction, ImageUrl = "_assets/img/drafts.gif" });

       /* mailItems.Add(new { Name = "Note List", ImageUrl = "_assets/img/note_small.gif" });
        mailItems.Add(new { Name = "Last Seven Days", ImageUrl = "_assets/img/note_small.gif" });
        mailItems.Add(new { Name = "By Category", ImageUrl = "_assets/img/note_small.gif" });
        mailItems.Add(new { Name = "By Color", ImageUrl = "_assets/img/note_small.gif" });
        */
        return mailItems;
    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ArrayList FetchContactItems()
    {
        ArrayList mailItems = new ArrayList();
    
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=5\">" + Resources.UIResource.VsdActivityNews + "</a>", ImageUrl = "_assets/img/mailbox.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=6\">" + Resources.UIResource.VsdRegistrationNews + "</a>", ImageUrl = "_assets/img/inbox.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=7\">" + Resources.UIResource.VsdMemberNews + "</a>", ImageUrl = "_assets/img/drafts.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=8\">" + Resources.UIResource.VsdEconomicNews + "</a>", ImageUrl = "_assets/img/drafts.gif" });
        
        return mailItems;
    }
    }
}