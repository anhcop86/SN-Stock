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

public partial class MessengerSentDetail : System.Web.UI.Page
{
    int messagecontentSentID;
    MessageContentSent messageContentSent;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setTextPageLoad();
        this.parameter();
        this.loadInfoDropdownlist();
        if (!IsPostBack)
        {
            this.loadModify();
        }
    }

    private void setTextPageLoad()
    {
        this.ButtonDelete.Value = Resources.UIResource.Delete;
    }

    private void loadInfoDropdownlist()
    {
        this.LoadDropDownListForServiceType();
        this.LoadDropDownListForTemplateType();
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
    private void loadModify()
    {
        messageContentSent = MessageContentSentService.GetMessageContentSent(messagecontentSentID);
        if (messageContentSent != null)
        {
            this.LoadTemplateType.SelectedValue = Convert.ToString(messageContentSent.ContentTemplateID);
            this.LoadServiceType.SelectedValue = Convert.ToString(messageContentSent.ServiceTypeID);
            this.InputSender.Value = messageContentSent.Sender;
            this.InputReceiver.Value = messageContentSent.Receiver;
            this.inputSuject.Value = messageContentSent.Subject;
            this.InputBodyContentType.Text = messageContentSent.BodyContentType;
            this.InputBodyEncoding.Value = messageContentSent.BodyEncoding;
            this.InputBodyMessager.Text = messageContentSent.BodyMessage;
        }
    }

    private void parameter()
    {
        this.Page.Title = Resources.UIResource.MessageSentDetailTitle;
        this.messagecontentSentID = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_MESSAGE_CONTENT_SENT_ID));
    }
    protected void ButtonCallBack_onserverclick(object ob, EventArgs e)
    {
        Response.Redirect("MessengerSent.aspx");

    }
    protected void ButtonDelete_onserverclick(object ob, EventArgs e)
    {    
        MessageContentSentService.DeleteMessageContentSent(this.messagecontentSentID);
        Response.Redirect("MessengerSent.aspx");
    }
}
