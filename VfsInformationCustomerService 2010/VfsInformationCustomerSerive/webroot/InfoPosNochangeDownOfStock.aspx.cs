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

using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Utility;

public partial class InfoPosNochangeDownOfStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setTextForPageLoad();
        if (!IsPostBack)
        {
            Page.Title = Resources.UIResource.PosNochangeDownofStokSymbol;
            this.FromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.ToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");            
            this.UpdateInterface();
        }
    }
    private void UpdateInterface()
    {
        this.RepeaterData.DataSource = PosNochangeDownOfStockService.GetPosNochangeDownOfStockCollection(ApplicationHelper.ConvertStringToDate(FromDate.Text), ApplicationHelper.ConvertStringToDate(ToDate.Text));
        this.RepeaterData.DataBind();
    }
    protected void SearchInput_Click(object ob, EventArgs e)
    {        
        this.UpdateInterface();
    }    
    private void setTextForPageLoad()
    {     
        //this.Page.Title = Resources.UIResource.MessageRecieveTitle;
        this.SearchInput.Text = Resources.UIResource.SearchButton;
    }
}
