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
using Vfs.WebCrawler.Destination.Entities;      
using Vfs.WebCrawler.Destination.Business;
using System.Linq;
using Vfs.WebCrawler.Utility;

public partial class TestTool : System.Web.UI.Page
{
    public static stock_SymbolCollection stock_symbolCollection;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SearchInput.Text = "Update";
            countAVGInput.Value = "10";
            stock_symbolCollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
        }
    }
    
    protected void SearchInput_Click(object sender, EventArgs e)
    {
        this.SettingInterface();
        if (Page.IsValid == false) return;
        SymbolPermLongTestToolCollection fromCollection = Vfs.WebCrawler.Destination.Business.SymbolPermLongTestToolService.SymbolPermLongGetListTestTool(ApplicationHelper.ConvertStringToDate(FromDate.Text), ApplicationHelper.ConvertStringToDate(ToDate.Text), Convert.ToInt32(countAVGInput.Value));


        IndexToRepeater.DataSource = Vfs.WebCrawler.Destination.Business.IndexTestToolService.GetIndexTestTool(ApplicationHelper.ConvertStringToDate(FromDate.Text));
        IndexToRepeater.DataBind();

        IndexFromRepeater.DataSource = Vfs.WebCrawler.Destination.Business.IndexTestToolService.GetIndexTestTool(ApplicationHelper.ConvertStringToDate(ToDate.Text));
        IndexFromRepeater.DataBind();

        //fromCollection.Sort(new SortingClass().Compare(       
        //toCollection.Add(new stock_SymbolPermLong());
        try
        {

            symbolPermLongRepeater.DataSource = fromCollection;
            symbolPermLongRepeater.DataBind();            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    public static string GetStockSymbolById(object Id)
    {
        string stockSymbolResual = string.Empty;

        foreach (stock_Symbol stockSymbol in stock_symbolCollection)
        {
            if (stockSymbol.SymbolID == Convert.ToInt32(Id))
            {
                stockSymbolResual = stockSymbol.Symbol;
                break;
            }
        }
        return stockSymbolResual;
    }
    private void SettingInterface()
    {
        this.FromDateValidator.ErrorMessage = Resources.UIResource.EmptyInfo;
        this.ToDateValidator.ErrorMessage = Resources.UIResource.EmptyInfo;        
    }

}