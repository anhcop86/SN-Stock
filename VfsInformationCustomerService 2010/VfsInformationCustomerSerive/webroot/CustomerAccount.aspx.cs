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

using CoreSecurityServiceBusiness = CoreSecurityService.Business;
using CoreSecurityServiceEntities = CoreSecurityService.Entities;


public partial class CustomerAccount : System.Web.UI.Page
{
    protected CustomerColumns orderByCustomerAccount
    {
        get
        {
            if (Session["orderByCustomerAccount"] == null) Session["orderByCustomerAccount"] = CustomerColumns.CustomerId;
            return (CustomerColumns)Session["orderByCustomerAccount"];
        }
        set { Session["orderByCustomerAccount"] = value; }
    }
    protected string orderDirectionCustomerAccount
    {
        get
        {
            if (Session["orderDirectionCustomerAccount"] == null) Session["orderDirectionCustomerAccount"] = "ASC";
            return (string)Session["orderDirectionCustomerAccount"];
        }
        set { Session["orderDirectionCustomerAccount"] = value; }
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
        orderByCustomerAccount = CustomerColumns.CustomerId;
        orderDirectionCustomerAccount = "ASC";
    }

    private void setTextForPageLoad()
    {
        this.ButtonSynchronize.Value = Resources.UIResource.CoreSynchronize;
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
        this.RepeaterData.DataSource = CustomerService.GetCustomerListSearch(1,
            this.InputSearchCustomerid.Value == string.Empty ? "All" : this.InputSearchCustomerid.Value,
            this.InputSearchCustomerName.Value == string.Empty ? "All" : this.InputSearchCustomerName.Value,
            this.InputSearchCustomerNameViet.Value == string.Empty ? "All" : this.InputSearchCustomerNameViet.Value,
            this.InputSearchEmail.Value == string.Empty ? "All" : this.InputSearchEmail.Value,
            InputTel.Value == string.Empty ? "All" : InputTel.Value,orderByCustomerAccount
           , orderDirectionCustomerAccount, this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRow);
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
                //lbtnDelete.Attributes"onClick" = "confirmAction(event, '" + Resources.UIResource.ConfirmDeleteServiceType + "');";
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
        //string selectedItems = Request.Form"CheckBoxDelete";
        //Int32 messageContentSentId;
        //if (selectedItems == string.Empty || selectedItems == null) return;
        //foreach (string selectedItemId in selectedItems.Split(','))
        //{
        //    messageContentSentId = Convert.ToInt32(selectedItemId);
        //    MessageContentSentService.DeleteMessageContentSent(messageContentSentId);
        //}
        //this.UpdateInterface();
    }

    protected void SearchInput_Click(object ob, EventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = 1;
        this.UpdateInterface();
    }

    protected void ButtonSynchronize_Click(object sender, EventArgs e)
    {
        //string[] data = new string[0];        
        //int numberCustomerSent = 0;     
        CoreSecurityServiceEntities.CustomerCollection customerCollectionCore = new CoreSecurityServiceEntities.CustomerCollection();
        customerCollectionCore = CoreSecurityServiceBusiness.CustomerService.GetCustomerList(CoreSecurityService.Entities.CustomerColumns.CustomerName, "ASC");
        ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(Convert.ToInt32(ApplicationHelper.ContentemplateId));
        foreach (CoreSecurityServiceEntities.Customer customer in customerCollectionCore)
        {
            Customer customervfs = null;
            Customer customerCheck = CustomerService.GetCustomer(customer.CustomerId);
            if (customerCheck == null)
            {
                customervfs = new Customer();
            }
            else
            {
                customervfs = customerCheck;
            }
            
            customervfs = this.getSynchronize(customervfs, customer);

            if (customerCheck == null)
            {
                CustomerService.CreateCustomer(customervfs);
                ImportService.CreateMessageWhenNewCustomer(customervfs, contentTemplate, "M");
            }
            else
            {
                CustomerService.UpdateCustomer(customervfs);
            }
        }
        this.UpdateInterface();
    }

    private Customer getSynchronize(Customer customervfs, CoreSecurityService.Entities.Customer customer)
    {
        customervfs.TypeID = 1;
        customervfs.BranchCode = customer.BranchCode;
        customervfs.ContractNumber = customer.ContractNumber;
        customervfs.CustomerId = customer.CustomerId;
        customervfs.BrokerId = customer.BrokerId;
        customervfs.CustomerName = customer.CustomerName;
        customervfs.CustomerNameViet = customer.CustomerNameViet;
        customervfs.CustomerType = customer.CustomerType;
        customervfs.DomesticForeign = customer.DomesticForeign;
        customervfs.Dob = customer.Dob;
        customervfs.Sex = customer.Sex;
        customervfs.SignatureImage1 = customer.SignatureImage1;
        customervfs.SignatureImage2 = customer.SignatureImage2;
        customervfs.OpenDate = customer.OpenDate;
        customervfs.CloseDate = customer.CloseDate;
        customervfs.CardType = customer.CardType;
        customervfs.CardIdentity = customer.CardIdentity;
        customervfs.CardDate = customer.CardDate;
        customervfs.CardIssuer = customer.CardIssuer;
        customervfs.Address = customer.Address;
        customervfs.AddressViet = customer.AddressViet;
        customervfs.Tel = customer.Tel;
        customervfs.Fax = customer.Fax;
        customervfs.Mobile = customer.Mobile;
        customervfs.Mobile2 = customer.Mobile2;
        customervfs.Email = customer.Email;
        customervfs.UserCreate = customer.UserCreate;
        customervfs.DateCreate = customer.DateCreate;
        customervfs.UserModify = customer.UserModify;
        customervfs.DateModify = customer.DateModify;
        customervfs.ProxyStatus = customer.ProxyStatus;
        customervfs.AccountStatus = customer.AccountStatus;
        customervfs.Notes = customer.Notes;
        customervfs.WorkingAddress = customer.WorkingAddress;
        customervfs.UserIntroduce = customer.UserIntroduce;
        customervfs.AttitudePoint = customer.AttitudePoint;
        customervfs.DepositPoint = customer.DepositPoint;
        customervfs.ActionPoint = customer.ActionPoint;
        customervfs.Country = customer.Country;
        customervfs.Website = customer.Website;
        customervfs.TaxCode = customer.TaxCode;
        customervfs.AccountType = customer.AccountType;
        customervfs.OrderType = customer.OrderType;
        customervfs.ReceiveReport = customer.ReceiveReport;
        customervfs.ReceiveReportBy = customer.ReceiveReportBy;
        customervfs.MarriageStatus = customer.MarriageStatus;
        customervfs.KnowledgeLevel = customer.KnowledgeLevel;
        customervfs.Job = customer.Job;
        customervfs.OfficeFunction = customer.OfficeFunction;
        customervfs.OfficeTel = customer.OfficeTel;
        customervfs.OfficeFax = customer.OfficeFax;
        customervfs.HusbandWifeName = customer.HusbandWifeName;
        customervfs.HusbandWifeCardNumber = customer.HusbandWifeCardNumber;
        customervfs.HusbandWifeCardDate = customer.HusbandWifeCardDate;
        customervfs.HusbandWifeCardLocation = customer.HusbandWifeCardLocation;
        customervfs.HusbandWifeTel = customer.HusbandWifeTel;
        customervfs.HusbandWifeEmail = customer.HusbandWifeEmail;
        customervfs.JoinStockMarket = customer.JoinStockMarket;
        customervfs.InvestKnowledge = customer.InvestKnowledge;
        customervfs.InvestedIn = customer.InvestedIn;
        customervfs.InvestTarget = customer.InvestTarget;
        customervfs.RiskAccepted = customer.RiskAccepted;
        customervfs.InvestFund = customer.InvestFund;
        customervfs.DelegatePersonName = customer.DelegatePersonName;
        customervfs.DelegatePersonFunction = customer.DelegatePersonFunction;
        customervfs.DelegatePersonCardNumber = customer.DelegatePersonCardNumber;
        customervfs.DelegateCardDate = customer.DelegateCardDate;
        customervfs.DelegateCardLocation = customer.DelegateCardLocation;
        customervfs.DelegatePersonTel = customer.DelegatePersonTel;
        customervfs.DelegatePersonEmail = customer.DelegatePersonEmail;
        customervfs.ChiefAccountancyName = customer.ChiefAccountancyName;
        customervfs.ChiefAccountancyCI = customer.ChiefAccountancyCI;
        customervfs.ChiefAccountancyCD = customer.ChiefAccountancyCD;
        customervfs.ChiefAccountancyIssuer = customer.ChiefAccountancyIssuer;
        customervfs.ChiefAccountancySign1 = customer.ChiefAccountancySign1;
        customervfs.ChiefAccountancySign2 = customer.ChiefAccountancySign2;
        customervfs.CaProxyName = customer.CaProxyName;
        customervfs.CaProxyCI = customer.CaProxyCI;
        customervfs.CaProxyCD = customer.CaProxyCD;
        customervfs.CaProxyIssuer = customer.CaProxyIssuer;
        customervfs.CaProxySign1 = customer.CaProxySign1;
        customervfs.CaProxySign2 = customer.CaProxySign2;
        customervfs.CompanySign1 = customer.CompanySign1;
        customervfs.CompanySign2 = customer.CompanySign2;
        customervfs.TradeCode = customer.TradeCode;
        customervfs.CustomerAccount = customer.CustomerAccount;
        customervfs.MobileSms = customer.MobileSms;
        customervfs.IsHere = customer.IsHere;
        customervfs.MoneyDepositeNumber = customer.MoneyDepositeNumber;
        customervfs.MoneyDepositeLocation = customer.MoneyDepositeLocation;
        customervfs.DepartmentId = customer.DepartmentId;
        customervfs.PublicCompanyManage = customer.PublicCompanyManage;
        customervfs.PublicCompanyHolder = customer.PublicCompanyHolder;
        customervfs.ParentCompanyName = customer.ParentCompanyName;
        customervfs.ParentCompanyAddress = customer.ParentCompanyAddress;
        customervfs.ParentCompanyEmail = customer.ParentCompanyEmail;
        customervfs.ParentCompanyTel = customer.ParentCompanyTel;
        customervfs.PostType = customer.PostType;
        customervfs.ReOpenDate = customer.ReOpenDate;
        customervfs.UserTakeCared = customer.UserTakeCared;
        return customervfs;
    }
    
    protected void GetValue()
    {
        orderDirectionCustomerAccount = orderDirectionCustomerAccount == "ASC" ? "DESC" : "ASC";
    }

    protected void SortCustomerId(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.CustomerId;
        this.UpdateInterface();
    }
    protected void SortCustomerName(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.CustomerNameViet;
        this.UpdateInterface();        
    }

    protected void SortDOB(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Dob;
        this.UpdateInterface();        
    }

    protected void SortMobile(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Mobile;
        this.UpdateInterface();        
    }

    protected void SortEmail(object ob, EventArgs e)
    {
        GetValue();
        orderByCustomerAccount = CustomerColumns.Email;
        this.UpdateInterface();        
    }
    protected object GetOrderDirectionIndicator(string property)
    {
        if (property.Equals(orderByCustomerAccount.ToString()))
        {
            return string.Format("<img alt=\"{0}\" src=\"_assets/img/{0}.gif\" />", orderDirectionCustomerAccount);
        }
        else
            return "";
    }
    

}
