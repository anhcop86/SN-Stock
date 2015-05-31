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

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Utility;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;

public partial class Public_Unsubcribe : System.Web.UI.Page
{
    string email;
    protected void Page_Load(object sender, EventArgs e)
    {
        Unsubcribe();        
    }

    protected void Unsubcribe()
    {
        email = AppConstants.GetString("email");
        if (email == string.Empty || email == null)
        {
            UnsubcribeButton.Enabled = false;
            return;
        }
        else
        {
            UnsubcribeButton.Enabled = true;
            SimpleAES encryptEmail = new SimpleAES();
            email = encryptEmail.DecryptString(email);
        }
    }

    protected void getEmail()
    {
        CustomerCollection customerCollection = CustomerService.GetCustomerListByEmail(email);
        foreach (Customer customer in customerCollection)
        {
            customer.SendYN = "N";
            CustomerService.UpdateCustomer(customer);
        }
    }

    protected void UnsubcribeButton_onclick(object sender, EventArgs e)
    {
        this.getEmail();
        CheckUnsubcribeLabel.Visible = false;
        UnsubcribeLabel.Visible = true;        
    }
}