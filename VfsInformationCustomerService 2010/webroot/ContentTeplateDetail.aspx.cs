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

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using Vfs.WebCrawler.Utility;

public partial class ContentTeplateDetail : System.Web.UI.Page
{
    string action;
    string clicked;
    int contentTemplateID;
    ContentTemplate contentTemplate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.paramater();
            this.LoadInfo();
            if (this.action == "modify") this.LoadDetail();
        }
    }

    private void LoadDetail()
    {
        contentTemplate = ContentTemplateService.GetContentTemplate(contentTemplateID);
        if (contentTemplate != null)
        {
            this.inputNameTemplate.Value = contentTemplate.Description;
            this.LoadServiceType.SelectedValue = Convert.ToString(contentTemplate.ServiceTypeID);
            this.InputSender.Value = contentTemplate.Sender;
            this.InputReceiver.Value = contentTemplate.Receiver;
            this.inputSuject.Value = contentTemplate.Subject;
            this.InputBodyContentType.Value = contentTemplate.BodyContentType;
            this.InputBodyEncoding.Value = contentTemplate.BodyEncoding;
            this.InputBodyMessager.Value = contentTemplate.BodyMessage;           
        }
    }

    private void LoadInfo()
    {
        this.LoadButton();
        this.LoadInfoServiceType();
    }

    private void LoadButton()
    {
        if (this.action == "new")
        {
            this.ButtonDelete.Visible = false;
        }
        else
        {
            this.ButtonDelete.Visible =true;
        }
    }

    private void LoadInfoServiceType()
    {
        LoadServiceType.DataSource = ServiceTypeService.GetServiceTypeList(ServiceTypeColumns.ServiceTypeDescription, "ASC");
        LoadServiceType.DataTextField = "ServiceTypeDescription";
        LoadServiceType.DataValueField = "ServiceTypeId";
        LoadServiceType.DataBind();
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);
        this.clicked = AppConstants.GetString(AppConstants.QS_CLICK_ED);
        if (this.action == "modify")
        {
            this.contentTemplateID = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_CONTENT_TEMPLATE_ID));
        }
        if (this.clicked == "messagercontent")
        {
            this.ButtonDelete.Disabled = true;
            this.ButtonSave.Disabled = true;
        }
    }
    protected void buttonSave_ServerClick(object ob, EventArgs e)
    {
        this.SaveDetail();
    }

    private void SaveDetail()
    {
        this.paramater();
        if (inputNameTemplate.Value == "")
        {
            this.lableInfor.InnerText =Resources.UIResource.missingInfor;
            this.lableInfor.Visible = true;
            return;
        }
        if (this.action == "modify")
        {
            this.contentTemplate = ContentTemplateService.GetContentTemplate(this.contentTemplateID);
        }
        else
        {
            this.contentTemplate = new ContentTemplate();
        }
        if (this.contentTemplate != null)
        {
            contentTemplate.Description =this.inputNameTemplate.Value ;
            contentTemplate.ServiceTypeID = Convert.ToInt32(this.LoadServiceType.SelectedValue);
            contentTemplate.Sender=this.InputSender.Value;
            contentTemplate.Receiver = this.InputReceiver.Value;
            contentTemplate.Subject = this.inputSuject.Value;
            contentTemplate.BodyContentType = this.InputBodyContentType.Value;
            contentTemplate.BodyEncoding = this.InputBodyEncoding.Value;
            contentTemplate.BodyMessage = this.InputBodyMessager.Value;
        }
        if (this.action == "modify")
        {            
            contentTemplate.ModifiedDate = DateTime.Now;
            ContentTemplateService.UpdateContentTemplate(this.contentTemplate);
        }
        else
        {
            if(ExistContentTemplateByContentTemplate(this.inputNameTemplate.Value)==true)
            {
                this.lableInfor.InnerText = Resources.UIResource.ExistNameForContentTemplate;
                this.lableInfor.Visible = true;
                return;
            }
            contentTemplate.ModifiedDate = contentTemplate.CreatedDate = DateTime.Now;
            ContentTemplateService.CreateContentTemplate(this.contentTemplate);
        }
        Response.Redirect("ContentTemplate.aspx");
    }
    protected void ButtonDelete_onserverclick(object ob, EventArgs e)
    {
        this.paramater();        
        if (this.action == "modify")
        {
            ContentTemplateService.DeleteContentTemplate(this.contentTemplateID);
        }
        Response.Redirect("ContentTemplate.aspx");
    }
    bool ExistContentTemplateByContentTemplate(string description)
    {
        bool resual=false;
        ContentTemplateCollection ContentTemplateCollection = new ContentTemplateCollection();
        ContentTemplateCollection = ContentTemplateService.ExistContentTemplateByContentTemplate(description);
        if (ContentTemplateCollection.Count > 0)
            resual = true;
        return resual;
    }
}
