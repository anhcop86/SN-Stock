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

public partial class CustomerService_MessengerSent : System.Web.UI.Page
{
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

    private void LoadFilterServicetypeDropdown()
    {
        this.FilterServiceTypeDropdownlis.DataSource = ServiceTypeService.GetServiceTypeList(ServiceTypeColumns.ServiceTypeDescription, "ASC");
        this.FilterServiceTypeDropdownlis.DataTextField = "ServiceTypeDescription";
        this.FilterServiceTypeDropdownlis.DataValueField = "ServiceTypeID";
        this.FilterServiceTypeDropdownlis.DataBind();
        this.FilterServiceTypeDropdownlis.Items.Insert(0, new ListItem(Resources.UIResource.AllOfFilter, "0"));
    }
    private void setTextForPageLoad()
    {        
     //   this.ButtonDeleteSelect.Value = Resources.UIResource.Delete;
        this.Page.Title = Resources.UIResource.MessageRecieveTitle;
        this.SearchInput.Text = Resources.UIResource.SearchButton;
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }

    private void UpdateInterface()
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = MessageContentSentService.MessageContentSentGetListFilterByServiceTypeID(Convert.ToInt32(this.FilterServiceTypeDropdownlis.SelectedValue),
            this.SearchSenderInput.Value == "" ? "All" : this.SearchSenderInput.Value,
            this.SearchReceiverInput.Value == "" ? "All" : this.SearchReceiverInput.Value,
            this.FromDate.Text == "" ? ApplicationHelper.ConvertStringToDate("01/01/1900") : ApplicationHelper.ConvertStringToDate(this.FromDate.Text),
            this.ToDate.Text == "" ? ApplicationHelper.ConvertStringToDate("01/01/1900") : ApplicationHelper.ConvertStringToDate(this.ToDate.Text),
            MessageContentSentColumns.ContentTemplateID, "ASC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterData.DataBind();
        this.topPaging.PageSize = this.bottomPaging.PageSize = ApplicationHelper.PageSize;
        this.topPaging.ItemCount = this.bottomPaging.ItemCount = totalRow;
    }
    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }
    protected void InsertButton_Click(object ob, EventArgs e)
    {
        Response.Redirect("MessengerSentDetail.aspx?action=new");
    }
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            MessageContentSentService.DeleteMessageContentSent(Convert.ToInt32(e.CommandArgument));
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

                //if (Common.ExistServiceTypeIdForMessageContent(Convert.ToInt32(lbtnDelete.CommandArgument)) == true)
                //{
                //    lbtnDelete.Enabled = false;
                //}
                //else
                //{
                lbtnDelete.Enabled = false;
                lbtnDelete.Attributes["onClick"] = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
                //}
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message);
                //this.latestException = ex;
            }
        }
    }
    protected string GetNameContentTemplate(object contentTemplateId)
    {
        string s;
        s = ContentTemplateService.GetContentTemplate(Convert.ToInt32(contentTemplateId)).Description;
        return s;
    }
    protected string GetNameServiceType(object serviceTypeId)
    {
        string s;
        s = ServiceTypeService.GetServiceType(Convert.ToInt32(serviceTypeId)).ServiceTypeDescription;
        return s;
    }
    protected string GetNameBodyContentType(object bodyContentTypeID)
    {
        string s;
        if (Convert.ToInt32(bodyContentTypeID) == 1)
        {
            s = Resources.UIResource.bodyContentTypeID1;
        }
        else
        {
            s = Resources.UIResource.bodyContentTypeID2;
        }
        return s;
    }
    protected string GetNameBodyEncoding(object bodyEncodingID)
    {
        string s;
        if (Convert.ToInt32(bodyEncodingID) == 1)
        {
            s = Resources.UIResource.bodyEncodingID1;
        }
        else
        {
            s = Resources.UIResource.bodyEncodingID2;
        }
        return s;
    }

    protected void ButtonDeleteSelect_Click(object ob, EventArgs e)
    {
        string selectedItems = Request.Form["CheckBoxDelete"];
        Int32 messageContentSentId;        
        if (selectedItems == string.Empty || selectedItems == null) return;
        foreach (string selectedItemId in selectedItems.Split(','))
        {
            messageContentSentId = Convert.ToInt32(selectedItemId);
            MessageContentSentService.DeleteMessageContentSent(messageContentSentId);
        }
        this.UpdateInterface();
        
    }


    protected void FilterServiceTypeDropdownlis_OnSelectedIndexChanged(object ob,EventArgs e)
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
