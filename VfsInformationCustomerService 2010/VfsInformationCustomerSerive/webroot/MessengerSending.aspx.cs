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
using System.Collections.Specialized;
public partial class MessengerSending : System.Web.UI.Page
{    
    string action;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setTextForPageLoad();
        if (!IsPostBack)
        {
            this.FromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.ToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.LoadFilterServicetypeDropdown();
            this.UpdateInterface();
        }
    }

    private void setTextForPageLoad()
    {        
        this.SearchInput.Text = Resources.UIResource.SearchButton;
        this.InsertButton.Value = Resources.UIResource.Add;
        this.ResentMessengerButton.Value = Resources.UIResource.ResentMessengerButton;
        this.ButtonDeleteSelect.Value = Resources.UIResource.Delete;
        this.ButtonDeleteAllYear.Value = Resources.UIResource.DeleteAllYear;
        this.Page.Title = Resources.UIResource.MessageSendingTitle;        
    }

    private void LoadFilterServicetypeDropdown()
    {
        this.FilterServiceTypeDropdownlis.DataSource = ServiceTypeService.GetServiceTypeList(ServiceTypeColumns.ServiceTypeDescription, "ASC");        
        this.FilterServiceTypeDropdownlis.DataTextField = "ServiceTypeDescription";
        this.FilterServiceTypeDropdownlis.DataValueField = "ServiceTypeID";
        this.FilterServiceTypeDropdownlis.DataBind();
        this.FilterServiceTypeDropdownlis.Items.Insert(0, new ListItem(Resources.UIResource.AllOfFilter, "0"));
    }
    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);     
    }
   
    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }

    private void UpdateInterface()
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = MessageContentService.MessageContentGetListFilterByServiceTypeID(Convert.ToInt32(this.FilterServiceTypeDropdownlis.SelectedValue),
            this.SearchSenderInput.Value == "" ? "All" : this.SearchSenderInput.Value,
            this.SearchReceiverInput.Value == "" ? "All" : this.SearchReceiverInput.Value,
            this.FromDate.Text == "" ? ApplicationHelper.ConvertStringToDate("01/01/1900") : ApplicationHelper.ConvertStringToDate(this.FromDate.Text),
            this.ToDate.Text == "" ? ApplicationHelper.ConvertStringToDate("01/01/1900") : ApplicationHelper.ConvertStringToDate(this.ToDate.Text),
                MessageContentColumns.MessageContentID, "ASC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterData.DataBind();
        this.topPaging.PageSize = this.bottomPaging.PageSize = ApplicationHelper.PageSize;
        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }
    protected void InsertButton_Click(object ob, EventArgs e)
    {
        Response.Redirect("MessengerSendingDetail.aspx?action=new");
    }
    protected void ResentMessengerButton_Click(object ob, EventArgs e)
    {
        string selectedItems = Request.Form["CheckBoxDelete"];
        foreach (string selectedItemId in selectedItems.Split(','))
        {
            MessageContent messageContent = new MessageContent();
            messageContent = MessageContentService.GetMessageContentIDAndDate(Convert.ToInt32(selectedItemId), ApplicationHelper.ConvertStringToDate(this.FromDate.Text));
            messageContent.Status = 0;
            MessageContentService.UpdateMessageContent(messageContent);
        }
        this.UpdateInterface();
    }
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            MessageContentService.DeleteMessageContentAndAttachement(Convert.ToInt32(e.CommandArgument));
        }
        this.UpdateInterface();
    }
    protected void RepeaterData_OnItemDataBound(object ob, RepeaterItemEventArgs e)
    {
        RepeaterItem(e.Item);
    }

    private void RepeaterItem(RepeaterItem e)
    {
        if (e.ItemType == ListItemType.Item || e.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                ImageButton lbtnDelete = (ImageButton)e.FindControl("deleteImage");                
                HtmlImage image = (HtmlImage)e.FindControl("imgDelete");

                if (Common.ExistsMessageContentForMessageContentSent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true)
                
                {
                    lbtnDelete.Enabled = false;                
                }
                else
                {                    
                    lbtnDelete.Enabled = true;
                    lbtnDelete.Attributes["onClick"] = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
                }
                //}
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message);
                //this.latestException = ex;
            }
        }
    }
    protected string GetNameServiceType(object serviceTypeId)
    {
        string s;
        s = ServiceTypeService.GetServiceType(Convert.ToInt32(serviceTypeId)).ServiceTypeDescription;
        return s;
    }
    protected string GetNameContentTemplate(object contentTemplateId)
    {
        string s;
        s = ContentTemplateService.GetContentTemplate(Convert.ToInt32(contentTemplateId)).Description;
        return s;
    }
    protected string GetStatus(object statusId)
    {
        string s;
        if (Convert.ToInt32(statusId) == 0) s = Resources.UIResource.NotStart;
        else if (Convert.ToInt32(statusId) == 1) s = Resources.UIResource.SuccessAndFinish;
        else s = Resources.UIResource.FailedAndSuccess;
        return s;
    }
    protected void ButtonDeleteSelect_Click(object ob, EventArgs e)
    {
        string selectedItems = Request.Form["CheckBoxDelete"];
        Int32 messageContentId;        
        if (selectedItems == string.Empty || selectedItems == null) return;
        foreach (string selectedItemId in selectedItems.Split(','))
        {
            messageContentId = Convert.ToInt32(selectedItemId);
            MessageContentAttachementService.DeleteMessageContentAttachementByMessageContent(messageContentId);
            MessageContentService.DeleteMessageContent(messageContentId);            
        }
        UpdateInterface();
    }

    protected void ButtonDeleteAllYear_Click(object ob, EventArgs e)
    {
        MessageContentService.DeleteMessageContentYear(ApplicationHelper.ConvertStringToDate(this.ToDate.Text));
        UpdateInterface();
    }

    protected string returnDisable(object messageContent)
    {
        int messageContentId;
        messageContentId = Convert.ToInt32(messageContent.ToString());
        string stringReturn;
        if (Common.ExistsMessageContentForMessageContentSent(messageContentId) == true)
        {
            stringReturn = "disabled";         
        }
        else
        {
            stringReturn = "";
        }
        return stringReturn;
    }
    protected void FilterServiceTypeDropdownlis_OnSelectedIndexChanged(object ob, EventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = 1;
        this.UpdateInterface();
    }
    protected void SearchInput_Click(object ob, EventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = 1;
        this.UpdateInterface();
        
    }
}
