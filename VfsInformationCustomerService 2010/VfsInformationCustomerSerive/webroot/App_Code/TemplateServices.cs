using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VfsCustomerService.Business;
using Vfs.WebCrawler.Utility;
using System.Web.Script.Services;

/// <summary>
/// Summary description for TemplateServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[ScriptService] // important becase stuture is same mvc /webservices/action
public class TemplateServices : System.Web.Services.WebService {

    public TemplateServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [Obsolete]
    [WebMethod]
    public JsonResponse DeleteTemplate(string id)
    {
        if (Utils.StringIsNullOrWhitespace(id))
        {
            return new JsonResponse() { Message = Resources.UIResource.invalidPostId };
        }
        var contentTemplate = ContentTemplateService.GetContentTemplate(int.Parse(id));

        if (contentTemplate == null)
        {
            return new JsonResponse() { Message = Resources.UIResource.invalidPostId };
        }

        if (Common.ExistsContentTemplateForMessageContent(int.Parse(id)) == true || Common.ExistsContentTemplateForMessageContentSent(int.Parse(id)) == true)
        {
            return new JsonResponse() { Message = Resources.UIResource.CouldNotDeleteTemplate };
        }
        else
        {
            try
            {
                ContentTemplateService.DeleteContentTemplateAndAttachement(int.Parse(id));
                return new JsonResponse() { Success = true, Message = Resources.UIResource.Delete };
            }
            catch (Exception ex)
            {
                return new JsonResponse() { Message = string.Format(Resources.UIResource.CouldNotDeleteTemplate, ex.Message) };
                
            }
        }
    }
    
}
