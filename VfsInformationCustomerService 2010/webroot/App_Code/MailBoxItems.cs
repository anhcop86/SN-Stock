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
        mailItems.Add(new { Name = Resources.UIResource.newsFromTradingCenter, ImageUrl = "_assets/img/mailbox.gif", Link= "NewsList.aspx?linkId=1", itemId = "1" });
        mailItems.Add(new { Name = Resources.UIResource.newsIssuedOwner, ImageUrl = "_assets/img/inbox.gif", Link= "NewsList.aspx?linkId=2", itemId = "2" });
        mailItems.Add(new { Name = Resources.UIResource.newsSecuritiesCompany, ImageUrl = "_assets/img/drafts.gif", Link= "NewsList.aspx?linkId=3", itemId = "3" });
        mailItems.Add(new { Name = Resources.UIResource.otherNews, ImageUrl = "_assets/img/outbox.gif", Link= "NewsList.aspx?linkId=4", itemId = "4" });
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
        
        mailItems.Add(new { Name = Resources.UIResource.newsFromHastcTradingCenter, ImageUrl = "_assets/img/mailbox.gif" });
        mailItems.Add(new { Name = Resources.UIResource.newsHastcIssuedOwner, ImageUrl = "_assets/img/inbox.gif" });
        mailItems.Add(new { Name = Resources.UIResource.newsHastcSecuritiesCompany, ImageUrl = "_assets/img/drafts.gif" });
        mailItems.Add(new { Name = Resources.UIResource.hastcBondNews, ImageUrl = "_assets/img/drafts.gif" });
        mailItems.Add(new { Name = Resources.UIResource.hastcBondTransaction, ImageUrl = "_assets/img/drafts.gif" });

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
    
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=5\">" + Resources.UIResource.vsdActivityNews + "</a>", ImageUrl = "_assets/img/mailbox.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=6\">" + Resources.UIResource.vsdRegistrationNews + "</a>", ImageUrl = "_assets/img/inbox.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=7\">" + Resources.UIResource.vsdMemberNews + "</a>", ImageUrl = "_assets/img/drafts.gif" });
        mailItems.Add(new { Name = "<a href=\"NewsList.aspx?linkId=8\">" + Resources.UIResource.vsdEconomicNews + "</a>", ImageUrl = "_assets/img/drafts.gif" });
        
        return mailItems;
    }
    }
}