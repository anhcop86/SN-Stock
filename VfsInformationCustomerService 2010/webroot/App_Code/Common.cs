using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

namespace Vfs.WebCrawler.Utility
{
    public class Common
    {
        public Common()
        {
            
        }
        public static bool ExistServiceTypeIdForMessageContent(int serviceTypeId)
        {
            bool resual = false;
            MessageContentCollection messageContentC = new MessageContentCollection();
            messageContentC = MessageContentService.ExistServiceTypeIdForMessageContent(serviceTypeId);
            if (messageContentC.Count > 0) resual = true;
            return resual;
        }
        public static bool ExistsServiceTypeForContentTemplate(int serviceTypeId)
        {
            bool resual = false;
            ContentTemplateCollection contentTemplateCollection = new ContentTemplateCollection();
            contentTemplateCollection = ContentTemplateService.ExistsServiceTypeForContentTemplate(serviceTypeId);
            if (contentTemplateCollection.Count > 0) resual = true;
            return resual;
        }
        public static bool ExistsServiceTypeForMessageContentSent(int serviceTypeId)
        {
            bool resual = false;
            MessageContentSentCollection messageContentSentCollection = new MessageContentSentCollection();
            messageContentSentCollection = MessageContentSentService.ExistsServiceTypeForMessageContentSent(serviceTypeId);
            if (messageContentSentCollection.Count > 0) resual = true;
            return resual;
        }
    }
}
