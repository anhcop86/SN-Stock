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

public partial class CustomerAccountDetail : System.Web.UI.Page
{
    string customerid;
    string action;
    Customer customer;
    //CoreSecurityServiceEntity.CoreSecurityService.Customer 
    protected void Page_Load(object sender, EventArgs e)
    {
        this.paramater();
        this.setTextForPageLoad();
        if (!IsPostBack)
        {            
            if (this.action == "modify") this.LoadModify();
        }
    }
    private void setTextForPageLoad()
    {        
        this.ButtonCallBack.Value = Resources.UIResource.Cancel;
        this.ButtonSave.Value = Resources.UIResource.Save;        
        this.header1.InnerText = this.Page.Title = Resources.UIResource.CustomerInfo;
    }
    private void LoadModify()
    {
        customer = CustomerService.GetCustomer(customerid);
        if (customer != null)
        {
            this.InputCustomerId.Value = customer.CustomerId;
            this.InputCustomerName.Value = customer.CustomerName;
            this.InputCustomerNameViet.Value = customer.CustomerNameViet;
            this.SelectDomesticForeign.Value = customer.DomesticForeign;
            if (ApplicationHelper.ConvertDateToString(customer.Dob) == "01/01/1900")
            {
                this.Selectdob.Text = "";
            }
            else
            {
                this.Selectdob.Text = ApplicationHelper.ConvertDateToString(customer.Dob);
            }
            this.SelectSex.Value = customer.Sex;
            if (ApplicationHelper.ConvertDateToString(customer.OpenDate) == "01/01/1900")
            {
                this.SelectOpendate.Text = "";
            }
            else
            {
                this.SelectOpendate.Text = ApplicationHelper.ConvertDateToString(customer.OpenDate);
            }
            this.InputCardIdentityDetail.Value = customer.CardIdentity;
            this.InputAddress.Value = customer.AddressViet;
            this.inputTel.Value = customer.Tel;
            this.InputFax.Value = customer.Fax;
            this.InputMobile1.Value = customer.Mobile;
            this.InputModile2.Value = customer.Mobile2;
            this.inputEmail.Value = customer.Email;
            this.SelectDenyEmail.Value = customer.SendYN;
            this.SelectReceiveRelatedStockEmail.Value = customer.ReceiveRelatedStockEmail;
            this.SelectReceiveRelatedStockSms.Value = customer.ReceiveRelatedStockSms;
        }
    }

    private void paramater()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);
        if (this.action == "modify")
        {
            this.customerid = AppConstants.GetString(AppConstants.QS_CUSTOMER_ID);
        }
    }
      
    protected void ButtonCallBack_onserverclick(object sender, EventArgs e)
    {
        Response.Redirect("CustomerAccount.aspx");
    }    
    protected void ButtonSave_onserverclick(object sender, EventArgs e)
    {
        customer = CustomerService.GetCustomer(this.InputCustomerId.Value);
        if (customer != null)
        {
            customer.SendYN = this.SelectDenyEmail.Value;
            customer.ReceiveRelatedStockEmail = this.SelectReceiveRelatedStockEmail.Value;
            customer.ReceiveRelatedStockSms = this.SelectReceiveRelatedStockSms.Value;
            CustomerService.UpdateCustomer(customer);
        }
        Response.Redirect("CustomerAccount.aspx");
    }
    protected void ButtonSynchronize_Click(object sender, EventArgs e)
    {

    }
}
