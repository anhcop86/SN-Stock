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
public partial class MessengerSending : System.Web.UI.Page
{
    string action;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UpdateInterface();
        }
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
        this.RepeaterData.DataSource =MessageContentService.GetMessageContentList(MessageContentColumns.MessageContentID, "ASC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
        this.RepeaterData.DataBind();
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
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            MessageContentService.DeleteMessageContent(Convert.ToInt32(e.CommandArgument));
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
                lbtnDelete.Enabled = true;
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
    
}
