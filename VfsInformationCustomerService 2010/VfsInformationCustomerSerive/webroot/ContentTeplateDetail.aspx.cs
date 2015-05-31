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
using System.Resources;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using Vfs.WebCrawler.Utility;
using VfsCustomerService.Utility;

public partial class ContentTeplateDetail : System.Web.UI.Page
{
    string action;
    string clicked;
    int contentTemplateID;
    ContentTemplate contentTemplate;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SetTextPageLoad();
        if (!IsPostBack)
        {
            this.Paramater();
            this.LoadInfo();
            this.SetButtonDescription();
            if (this.action == "modify") this.LoadDetail();
        }
    }

    protected void LoadServiceType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.LoadServiceType.SelectedValue.ToString() == "2")
        {
            this.InputBodyContentType.SelectedIndex = 1;
        }
    }

    private void SetTextPageLoad()
    {
        this.Page.Title = Resources.UIResource.ContentTemplateDetailTitle;
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
            this.InputBodyContentType.SelectedValue = contentTemplate.BodyContentType;
            this.InputBodyEncoding.Value = contentTemplate.BodyEncoding;
            this.InputBodyMessager.Text = HttpUtility.HtmlDecode(contentTemplate.BodyMessage);
            this.LoadAttachement(contentTemplate.ContentTemplateID);
        }
    }

    private void LoadInfo()
    {
        this.LoadButton();
        this.LoadInfoServiceType();
    }

    private void SetButtonDescription()
    {
        this.deleteAttchementButton.Text = Resources.UIResource.DeleteAttachement;
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

    private void Paramater()
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
        this.Paramater();
        if (inputNameTemplate.Value == "")
        {
            this.lableInfor.InnerText =Resources.UIResource.MissingInfor;
            this.lableInfor.Visible = true;
            return;
        }
        if (VfsCustomerService.Utility.HtmlRemoval.StripTagsRegexCompiled(this.InputBodyMessager.Text).Length > 320 && this.LoadServiceType.SelectedValue == "2")
        {
            this.errorCheckMaxleng.InnerHtml = Resources.UIResource.LabelCheckmaxleng;
            this.lableInfor.Visible = false;
            return;
        }
        if (ImportService.CheckParamaterDuplication(InputBodyMessager.Text.Trim()) == true)
        {
            this.errorCheckMaxleng.InnerHtml = Resources.UIResource.DuplicatedParameter;
            this.lableInfor.Visible = false;
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
            contentTemplate.Description =this.inputNameTemplate.Value;
            contentTemplate.ServiceTypeID = Convert.ToInt32(this.LoadServiceType.SelectedValue);
            contentTemplate.Sender=this.InputSender.Value;
            contentTemplate.Receiver = this.InputReceiver.Value;
            contentTemplate.Subject = this.inputSuject.Value;
            contentTemplate.BodyContentType = this.InputBodyContentType.SelectedValue.ToString();
            contentTemplate.BodyEncoding = this.InputBodyEncoding.Value;

            if (InputBodyContentType.SelectedValue == "HTML" || contentTemplate.ServiceTypeID == 1)
            {
                contentTemplate.BodyMessage = HttpUtility.HtmlDecode(InputBodyMessager.Text);
            }
            else
            {
                contentTemplate.BodyMessage = VfsCustomerService.Utility.HtmlRemoval.StripTagsRegexCompiled(InputBodyMessager.Text);
            }
        }
        if (this.action == "modify")
        {            
            contentTemplate.ModifiedDate = DateTime.Now;
            //ContentTemplateService.UpdateContentTemplate(this.contentTemplate);
            ContentTemplateService.UpdateContentTemplate(this.contentTemplate, this.attchementFile.FileContent, ApplicationHelper.AttachementUploadFolderPath, this.attchementFile.FileName, true);
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
            ContentTemplateService.CreateContentTemplate(this.contentTemplate, this.attchementFile.FileContent, ApplicationHelper.AttachementUploadFolderPath, this.attchementFile.FileName, true);
        }
        Response.Redirect("ContentTemplate.aspx");
    }

    protected void ButtonDelete_onserverclick(object ob, EventArgs e)
    {
        this.Paramater();
        if (this.action == "modify")
        {
            if (this.contentTemplateID == Convert.ToInt32(ApplicationHelper.ContentemplateId))
                return;
            else
                ContentTemplateService.DeleteContentTemplateAndAttachement(this.contentTemplateID);
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

    public void LoadAttachement(Int32 contentTemplateId)
    {
        Int32 totalRow = 0;
        this.contentTemplateAttchementDropDownList.DataSource = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplateId, ContentTemplateAttachementColumns.ModifiedDate, "DESC", 1, 20, out totalRow);
        this.contentTemplateAttchementDropDownList.DataTextField = "AttachementDocument";
        this.contentTemplateAttchementDropDownList.DataValueField = "ContentTemplateAttachementID";
        this.contentTemplateAttchementDropDownList.DataBind();
    }

    public void DeleteAttchement(object ob, EventArgs e)
    {
        ListItem item = this.contentTemplateAttchementDropDownList.SelectedItem;
        ContentTemplateAttachementService.DeleteContentTemplateAttachement(Convert.ToInt32(item.Value.ToString()));
        VfsCustomerService.Utility.UploadService.Delete(ApplicationHelper.AttachementUploadFolderPath + item.Text);
        this.Paramater();
        this.LoadAttachement(contentTemplateID);
    }
    protected void ButtonCallBack_onserverclick(object ob, EventArgs e)
    {
        Response.Redirect("ContentTemplate.aspx");
    }
}
