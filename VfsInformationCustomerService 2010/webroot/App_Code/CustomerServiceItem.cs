using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CustomerServiceItem
/// </summary>
namespace Vfs.WebCrawler.Utility
{
    public class CustomerServiceItem
    {
        public CustomerServiceItem()
        {
        }
        public static ArrayList FetchCustomerServiceItem()
        {
            ArrayList item = new ArrayList();
            item.Add(new {Name = Resources.UIResource.CatalogService,ImageUrl = "_assets/img/mailbox.gif", Link = "CatalogService.aspx", itemId = "20" });
            item.Add(new {Name = Resources.UIResource.ModelContent,ImageUrl = "_assets/img/mailbox.gif", Link = "ContentTemplate.aspx", itemId = "21" });
            item.Add(new {Name = Resources.UIResource.CreateMessage,ImageUrl = "_assets/img/mailbox.gif", Link = "CreateMessage.aspx", itemId = "22" });
            item.Add(new {Name = Resources.UIResource.MessengerSending,ImageUrl = "_assets/img/mailbox.gif", Link = "MessengerSending.aspx", itemId = "23" });
            item.Add(new {Name = Resources.UIResource.MessengerSent,ImageUrl = "_assets/img/mailbox.gif", Link = "MessengerSent.aspx", itemId = "24" });
            item.Add(new {Name = Resources.UIResource.Imitate,ImageUrl = "_assets/img/mailbox.gif", Link = "Imitate.aspx", itemId = "25" });
            return item;
        }
    }
}