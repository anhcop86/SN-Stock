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


public partial class CustomerNoAccount : System.Web.UI.Page
{
    protected CustomerColumns orderByCustomerNoAccount
    {
        get
        {
            if (Session["orderByCustomerNoAccount"] == null) Session["orderByCustomerNoAccount"] = CustomerColumns.CustomerId;
            return (CustomerColumns)Session["orderByCustomerNoAccount"];
        }
        set { Session["orderByCustomerNoAccount"] = value; }
    }
    protected string orderDirectionCustomerNoAccount
    {
        get
        {
            if (Session["orderDirectionCustomerNoAccount"] == null) Session["orderDirectionCustomerNoAccount"] = "ASC";
            return (string)Session["orderDirectionCustomerNoAccount"];
        }
        set { Session["orderDirectionCustomerNoAccount"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setTextForPageLoad();
        if (!IsPostBack)
        {
            this.GetParameters();            
        }
        this.UpdateInterface();
    }

    private void GetParameters()
    {
        orderByCustomerNoAccount = CustomerColumns.CustomerId;
        orderDirectionCustomerNoAccount = "ASC";
    }
    
    private void setTextForPageLoad()
    {
        this.InsertButton.Value = Resources.UIResource.Add;
        this.ButtonDeleteSelect.Value = Resources.UIResource.Delete;        
        this.SearchInput.Text = Resources.UIResource.SearchButton;
        this.ButtonImportCustomerNoAccount.Value = "Import";
    }
    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();
    }

    private void UpdateInterface()
    {
        Int32 totalRow;
        this.RepeaterData.DataSource = CustomerService.GetCustomerListSearch(2,
            this.InputSearchCustomerid.Value == string.Empty ? "All" : this.InputSearchCustomerid.Value,
            this.InputSearchCustomerName.Value == string.Empty ? "All" : this.InputSearchCustomerName.Value,
            this.InputSearchCustomerNameViet.Value == string.Empty ? "All" : this.InputSearchCustomerNameViet.Value,
            this.InputSearchEmail.Value == string.Empty ? "All" : this.InputSearchEmail.Value,
            InputTel.Value == string.Empty ? "All" : InputTel.Value,
            orderByCustomerNoAccount, orderDirectionCustomerNoAccount, this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
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
        Response.Redirect("CustomerNoAccountDetail.aspx?action=new");
    }
    protected void RepeaterData_ItemCommand(object ob, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {            
            CustomerService.DeleteCustomer(Convert.ToString(e.CommandArgument));
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
                //lbtnDelete.Enabled = false;
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
    protected string ConverFormat(object dob)
    {
        string s;
        if (ApplicationHelper.ConvertDateToString(dob) == "01/01/1900")
            s = "";
        else
            s = ApplicationHelper.ConvertDateToString(dob);
        return s;
    }
   
    protected void ButtonDeleteSelect_Click(object ob, EventArgs e)
    {        
        string selectedItems = Request.Form["CheckBoxDelete"];
        string customerId;
        if (selectedItems == string.Empty || selectedItems == null) return;
        foreach (string selectedItemId in selectedItems.Split(','))
        {
            customerId = selectedItemId;
            CustomerService.DeleteCustomer(customerId);            
        }
        this.UpdateInterface();
    }
        
    protected void SearchInput_Click(object ob, EventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = 1;
        this.UpdateInterface();
    }
    protected void GetValue()
    {
        orderDirectionCustomerNoAccount = orderDirectionCustomerNoAccount == "ASC" ? "DESC" : "ASC";
    }

    protected void SortCustomerId(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerNoAccount = CustomerColumns.CustomerId;
        this.UpdateInterface();
    }
    protected void SortCustomerName(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerNoAccount = CustomerColumns.CustomerNameViet;
        this.UpdateInterface();
    }

    protected void SortDOB(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerNoAccount = CustomerColumns.Dob;
        this.UpdateInterface();
    }

    protected void SortMobile(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerNoAccount = CustomerColumns.Mobile;
        this.UpdateInterface();
    }

    protected void SortEmail(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerNoAccount = CustomerColumns.Email;
        this.UpdateInterface();
    }
    protected object GetOrderDirectionIndicator(string property)
    {
        if (property.Equals(orderByCustomerNoAccount.ToString()))
        {
            return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectionCustomerNoAccount);
        }
        else
            return "";
    }
    protected void ButtonImportCustomerNoAccount_onserverclick(object ob, EventArgs e)
    {
        if (uploadFile.FileName == string.Empty)
        {
            this.infoErrorWhenUploadFile.Attributes["class"] = "inforError";
            this.infoErrorWhenUploadFile.InnerText = Resources.UIResource.ErrorImport;
            return;
        }
        VfsCustomerService.Business.ImportService.ImportCustomer(uploadFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.UploadFolderPath),uploadFile.FileName);

        this.infoErrorWhenUploadFile.Attributes["class"] = "inforSeccessful";
        this.infoErrorWhenUploadFile.InnerText = Resources.UIResource.importSucess;

        UpdateInterface();
    }
}
