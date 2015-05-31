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

using Vfs.WebCrawler.Utility;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

public partial class CustomerService_Simulation : System.Web.UI.Page
{
    protected ContentTemplate contentTemplate = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.SetInterface();
            this.LoadTemplates();
        }
        this.ResetInterface();
    }

    private void ResetInterface()
    {
        messageLiteral.Visible = false;
    }

    private void SetInterface()
    {
        this.sendButton.Value = Resources.UIResource.Send;
        this.Page.Title = Resources.UIResource.Simulation;
        this.receiverTextBoxRequiredFieldValidator.ErrorMessage = Resources.UIResource.ReceiverEmptyErrorMessage;
    }

    private void LoadTemplates()
    {
        this.templateDropDownList.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.ModifiedDate, "DESC");
        this.templateDropDownList.DataTextField = "Description";
        this.templateDropDownList.DataValueField = "ContentTemplateID";        
        this.templateDropDownList.DataBind();
        this.templateDropDownList.Items.Insert(0, new ListItem(Resources.UIResource.SelectOneItem, "0"));
    }

    protected void templateDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       this.GetContentTemplate();
       if (contentTemplate != null)
       {
           this.InputBodyMessager.Text = contentTemplate.BodyMessage;
           this.serviceTypeInput.Value = ServiceTypeService.GetServiceType(contentTemplate.ServiceTypeID).ServiceTypeDescription;
           this.LoadAttachement(contentTemplate.ContentTemplateID);
       }
    }

    protected void GetContentTemplate()
    {
        Int32 contentTemplateId = Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString());
        contentTemplate = ContentTemplateService.GetContentTemplate(contentTemplateId);
    }

    public void LoadAttachement(Int32 contentTemplateId)
    {
        Int32 totalRow = 0;
        this.contentTemplateAttchementDropDownList.DataSource = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplateId, ContentTemplateAttachementColumns.ModifiedDate, "DESC", 1, 20, out totalRow);
        this.contentTemplateAttchementDropDownList.DataTextField = "AttachementDocument";
        this.contentTemplateAttchementDropDownList.DataValueField = "ContentTemplateAttachementID";
        this.contentTemplateAttchementDropDownList.DataBind();
    }

    protected void Send_Click(object sender, EventArgs e)
    {
        if (this.templateDropDownList.SelectedValue == "0" || this.templateDropDownList.SelectedValue == string.Empty)
        {
            messageLiteral.Text = Resources.UIResource.InvalidTemplateContent;
            messageLiteral.Visible = true;
            return;
        }

        if (Page.IsValid == false)
            return;
        try
        {
            this.GetContentTemplate();
            if (contentTemplate != null)
            {
                MessageContent messageContent = new MessageContent();
                messageContent.BodyContentType = contentTemplate.BodyContentType;
                messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                messageContent.BodyMessage = contentTemplate.BodyMessage;
                messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                messageContent.CreatedDate = DateTime.Now;
                messageContent.ModifiedDate = DateTime.Now;
                messageContent.Receiver = this.receiverTextBox.Text;
                messageContent.Sender = contentTemplate.Sender;
                messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                messageContent.Subject = contentTemplate.Subject;

                if (contentTemplate.ServiceTypeID == 1)
                {
                    VfsCustomerService.Business.SendTemplateEmailWithParam sendMail = new SendTemplateEmailWithParam();
                    contentTemplate.Receiver = this.receiverTextBox.Text;
                    sendMail.Send(contentTemplate);
                }
                else
                {
                    SendSMS sendSMS = new SendSMS();
                    sendSMS.UserName = ApplicationHelper.SmsUserName;
                    sendSMS.Password = ApplicationHelper.SmsPassword;
                    sendSMS.Send(messageContent);
                }

                messageLiteral.Text = Resources.UIResource.MeesageSucessfullySent;
                messageLiteral.Visible = true;
            }
        }
        catch (Exception ex)
        {
            messageLiteral.Text = Resources.UIResource.SimulationError + " :" + ex.Message;
            messageLiteral.Visible = true;
            log4net.Util.LogLog.Error(ex.InnerException.Message);
        }
    }
}
