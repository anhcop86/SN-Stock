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

using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Utility;


public partial class ImportUpdateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.SettingInterfaceLoad();
    }

    private void SettingInterfaceLoad()
    {
        this.importUpdateButton.Value = Resources.UIResource.importUpdateStock;
        this.ShowInforMarket.Value = Resources.UIResource.ShowInfoMartket;
    }

    private void SettingInterface()
    {       
        this.importDateRequiredFieldValidator.ErrorMessage = Resources.UIResource.importUpdateErrorMessage;
        this.uploadFileRequiredFieldValidator.ErrorMessage = Resources.UIResource.uploadFileErrorMessage;
        this.successMessage.Text = string.Empty;
    }

    protected void importUpdateButton_Click(object sender, EventArgs e)
    {
        this.SettingInterface();
        if (Page.IsValid == false) return;
        try
        {
            Vfs.WebCrawler.Destination.Business.ImportService.UpdateStockPrice(new DateTime(Convert.ToInt16(importDateInput.Text.Substring(6, 4)), Convert.ToInt16(importDateInput.Text.Substring(3, 2)), Convert.ToInt16(importDateInput.Text.Substring(0, 2))), this.uploadFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.UploadFolderPath), this.uploadFile.FileName);
            successMessage.Text = Resources.UIResource.importUpdateSuccessMessage;
        }
        catch(Exception ex)
        {
            log4net.Util.LogLog.Error(ex.Message, ex);
            successMessage.Text = ex.Message;
        }
    }
    protected void ShowInforMarketUpdateButton_Click(object sender, EventArgs e)
    {        
        this.updateview();
        this.SettingInterface2();
    }

    private void SettingInterface2()
    {
        this.importDateRequiredFieldValidator.ErrorMessage = Resources.UIResource.ShowInfoDateError;
        this.uploadFileRequiredFieldValidator.ErrorMessage = string.Empty;
    }
    protected void updateview()
    {
        try
        {
            this.symbolPermLongRepeater.DataSource = Vfs.WebCrawler.Destination.Business.stock_SymbolPermLongService.GetUpdated_SymbolPermLongGetList(ApplicationHelper.ConvertStringToDate(importDateInput.Text));
            this.symbolPermLongRepeater.DataBind();
        }
        catch(Exception ex)
        {
        }
    }
    public static string GetStockSymbolById(object Id)
    {
        string stockSymbolResual = string.Empty;
        stock_SymbolCollection stock_symbolCollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
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
}
