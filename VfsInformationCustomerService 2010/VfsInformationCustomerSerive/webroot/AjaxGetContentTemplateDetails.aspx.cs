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
using System.Text;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Utility;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

public partial class AjaxGetContentTemplateDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/html";
        Response.Charset = "UTF-8";
        Response.Write(this.GetContentTemplateDetail());
        Response.End();
    }

    protected string GetContentTemplateDetail()
    {
        Int32 contentTemplateId;
        StringBuilder returnValue = new StringBuilder();
        ContentTemplate contentTemplate = null;
        contentTemplateId = AppConstants.GetInt32(AppConstants.QS_CONTENT_TEMPLATE_ID);
        contentTemplate = ContentTemplateService.GetContentTemplate(contentTemplateId);
        try
        {
            if (contentTemplate == null)
                return string.Empty;
            returnValue.Append(contentTemplate.BodyMessage);            
        }
        catch (Exception ex)
        {
            return string.Empty;;
        }
        return returnValue.ToString();
    }
}
