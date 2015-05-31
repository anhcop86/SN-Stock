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

public partial class MessageContentDetail : System.Web.UI.Page
{
    string action;
    int messageContentID;
    MessageContent messageContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadInfoInDropDownList();
            this.paramater();
            this.LoadButton();
            if (this.action == "modify") this.LoadModify();
        }
    }

    private void LoadInfoInDropDownList()
    {
        
        this.LoadDropDownListForServiceType();
        this.LoadDropDownListForTemplateType();
    }
    private void LoadButton()
    {
        if (this.action == "new")
        {
            this.ButtonDelete.Visible = false;
        }
        else
        {
            this.ButtonDelete.Visible = true;
        }
    }


    private void LoadDropDownListForTemplateType()
    {
        this.LoadTemplateType.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.Description, "ASC");
        this.LoadTemplateType.DataTextField = "Description";
        this.LoadTemplateType.DataValueField = "ContentTemplateID";
        this.LoadTemplateType.DataBind();
    }

    private void LoadDropDownListForServiceType()
    {
        this.LoadServiceType.DataSource = ServiceTypeService.GetServiceTypeList(ServiceTypeColumns.ServiceTypeDescription, "ASC");
        this.LoadServiceType.DataTextField = "ServiceTypeDescription";
        this.LoadServiceType.DataValueField = "ServiceTypeID";
        this.LoadServiceType.DataBind();
    }

    private void LoadModify()
    {
        messageContent = MessageContentService.GetMessageContent(this.messageContentID);
        if (messageContent != null)
        {
            this.LoadTemplateType.SelectedValue = Convert.ToString(messageContent.ContentTemplateID);
            this.LoadServiceType.SelectedValue = Convert.ToString(messageContent.ServiceTypeID);
            this.InputSender.Value = messageContent.Sender;
            this.InputReceiver.Value = messageContent.Receiver;
            this.inputSuject.Value = messageContent.Subject;
            this.InputBodyContentType.Value = messageContent.BodyContentType;
            this.InputBodyEncoding.Value = messageContent.BodyEncoding;
            this.InputBodyMessager.Value = messageContent.BodyMessage;
        }
    }
    protected void buttonSave_ServerClick(object ob, EventArgs e)
    {
        this.SaveDetail();
    }

    private void SaveDetail()
    {
        this.paramater();
        if (this.action == "modify")
        {
            this.messageContent = MessageContentService.GetMessageContent(this.messageContentID);
        }
        else
        {
            this.messageContent = new MessageContent();
        }
        if (this.messageContent != null)
        {
            messageContent.ContentTemplateID = Convert.ToInt32(this.LoadTemplateType.SelectedValue);
            messageContent.ServiceTypeID = Convert.ToInt32(this.LoadServiceType.SelectedValue);
            messageContent.Sender = this.InputSender.Value;
            messageContent.Receiver = this.InputReceiver.Value;
            messageContent.Subject = this.inputSuject.Value;
            messageContent.BodyContentType = this.InputBodyContentType.Value;
            messageContent.BodyEncoding = this.InputBodyEncoding.Value;
            messageContent.BodyMessage = this.InputBodyMessager.Value;
        }
        if (this.action == "modify")
        {
            messageContent.Status = 0;
            messageContent.ModifiedDate = DateTime.Now;            
            MessageContentService.UpdateMessageContent(this.messageContent);
        }
        else
        {
            messageContent.ModifiedDate = messageContent.CreatedDate = DateTime.Now;
            MessageContentService.CreateMessageContent(this.messageContent);
        }
        Response.Redirect("MessengerSending.aspx");

    }
    protected void ButtonDelete_onserverclick(object ob, EventArgs e)
    {
        this.paramater();
        if (this.action == "modify")
        {
            MessageContentService.DeleteMessageContent(this.messageContentID);
        }
        Response.Redirect("MessengerSending.aspx");
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);        
        if (this.action == "modify")
        {
            this.messageContentID = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_MESSAGE_CONTENET_ID));            
        }   
    }
}
