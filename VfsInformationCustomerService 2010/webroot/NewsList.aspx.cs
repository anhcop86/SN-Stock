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

using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Utility;

public partial class NewsList : System.Web.UI.Page
{
    private int linkId;
    private StockNewCollection stockNewCollection;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.UpdateInterface();
        SetClientEvents();      
    }

    private void SetClientEvents()
    {
        //if (stockNewCollection.Count > 0)
        //{
        //    if (stockNewCollection[0].NewsId > 0 && stockNewCollection[0] != null)
        //        this.RegisterStartupScript("onload", "<script type='text/javascript'> showNewDetail('" + stockNewCollection[0].NewsId.ToString() + "');</script>");
        //}
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.SetInterfacedescription();
    }

    protected void GetInputs()
    {
        linkId = AppConstants.GetInt32(AppConstants.QS_LINK);
        linkId = (linkId == 0 ? 1 : linkId);
    }

    protected void LoadNewsList()
    {
        Int32 totalRows;
        stockNewCollection = StockNewService.GetStockNewList(linkId, StockNewColumns.NewsDate, "DESC", this.topPaging.CurrentIndex, ApplicationHelper.PageSize, out totalRows);
        newsRepeater.DataSource = stockNewCollection;
        newsRepeater.DataBind();
        this.topPaging.PageSize = this.bottomPaging.PageSize = ApplicationHelper.PageSize;
        this.topPaging.ItemCount = this.bottomPaging.ItemCount =  totalRows;        
    }

    protected void topPaging_Command(object sender, CommandEventArgs e)
    {
        this.bottomPaging.CurrentIndex = this.topPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();        
    }

    protected void bottomPaging_Command(object sender, CommandEventArgs e)
    {
        this.topPaging.CurrentIndex = this.bottomPaging.CurrentIndex = Convert.ToInt32(e.CommandArgument);
        this.UpdateInterface();        
    }

    protected void UpdateInterface()
    {
        this.GetInputs();
        this.LoadNewsList();
    }

    protected void SetInterfacedescription()
    {
        this.approveButton.Text = Resources.UIResource.approve;
        this.rejectedButton.Text = Resources.UIResource.reject;
    }

    protected void approveButton_Click(object sender, EventArgs e)
    {
        this.ApprovedNewStockList();
        UpdateInterface();
        SetClientEvents();
    }

    protected void rejectedButton_Click(object sender, EventArgs e)
    {
        this.RejecteNewStockList();
        UpdateInterface();
        SetClientEvents();
    }    

    private void ApprovedNewStockList()
    {
        string selectedItems = Request.Form["selectedItem"];
        int pos, symbolId;
        Int32 stockId;
        Int32 newGroupId;

        if (selectedItems == string.Empty || selectedItems == null) return;

        foreach (string selectedItemId in selectedItems.Split(','))
        {
            stockId = Convert.ToInt32(selectedItemId);
            newGroupId = Convert.ToInt32(Request.Form["newsCategorySelect_" + stockId.ToString()]);
            StockNew stockNew = StockNewService.GetStockNew(stockId);
            stock_New stockNewDestination = new stock_New();
            stock_NewsGroup stock_NewsGroupDestination = new stock_NewsGroup();

            stockNewDestination.NewsTitle = stockNew.NewsTitle;
            stockNewDestination.NewsDescription = stockNew.NewsDescription;
            stockNewDestination.NewsContent = stockNew.NewsContent;
            stockNewDestination.NewsDate = stockNew.NewsDate;
            stockNewDestination.IsApproved = false;
            stockNewDestination.LanguageID = 2;// should be dymamic 
            stockNewDestination.NewsSource = stockNew.NewsSource;
            pos = stockNewDestination.NewsTitle.IndexOf(':');
            symbolId = 0;
            if (linkId == 2)
            {
                if (pos > 0) symbolId = GetStockSymbolId(stockNewDestination.NewsTitle.Substring(0, pos));

                if (symbolId > 0)
                {
                    stockNew.ShareSymbol = stockNewDestination.NewsTitle.Substring(0, pos);
                }
            }

            stockNewDestination.SymbolID = (symbolId > 0 ? symbolId : new int());            
            stock_NewService.Createstock_New(stockNewDestination);
            stock_NewsGroupDestination.NewsID = stockNewDestination.NewsID;
            stock_NewsGroupDestination.NewsGroup = newGroupId;
            stock_NewsGroupService.Createstock_NewsGroup(stock_NewsGroupDestination);
            StoredToApprovedNews(stockNew);
            StockNewService.DeleteStockNew(stockId);
        }
    }

    private void RejecteNewStockList()
    {
        string selectedItems = Request.Form["selectedItem"];
        Int32 stockId;
        Int32 newGroupId;

        if (selectedItems == string.Empty || selectedItems == null) return;

        foreach (string selectedItemId in selectedItems.Split(','))
        {
            stockId = Convert.ToInt32(selectedItemId);
            newGroupId = Convert.ToInt32(Request.Form["newsCategorySelect_" + stockId.ToString()]);
            StockNew stockNew = StockNewService.GetStockNew(stockId);
            this.StoreToRejectedNews(stockNew);
            StockNewService.DeleteStockNew(stockId);
        }
    }

    private int GetStockSymbolId(string symbol)
    {
        Int32 stockSymbolId = 0;
        stock_SymbolCollection stock_symbolCollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
        foreach(stock_Symbol stockSymbol in stock_symbolCollection)
        {
            if(stockSymbol.Symbol == symbol)
            {
                stockSymbolId = stockSymbol.SymbolID;
                break;
            }
        }
        return stockSymbolId;
    }

    private void StoredToApprovedNews(StockNew stockNew)
    {
        ApprovedStockNew approvedStockNew = new ApprovedStockNew();
        approvedStockNew.NewsId = stockNew.NewsId;
        approvedStockNew.NewsDate = stockNew.NewsDate;
        approvedStockNew.NewsTitle = stockNew.NewsTitle;
        approvedStockNew.NewsDescription = stockNew.NewsDescription;
        approvedStockNew.NewsContent = stockNew.NewsContent;
        approvedStockNew.NewsSource = stockNew.NewsSource;
        approvedStockNew.ShareSymbol = stockNew.ShareSymbol;
        approvedStockNew.UseUrl = stockNew.UseUrl;
        approvedStockNew.NewsUrl = stockNew.NewsUrl;
        approvedStockNew.LanguageID = stockNew.LanguageID;
        approvedStockNew.ImageUrl = stockNew.ImageUrl;
        approvedStockNew.LinkId = stockNew.LinkId;
        approvedStockNew.OriginalUrl = stockNew.OriginalUrl;
        approvedStockNew.ApprovedDate = DateTime.Now;
        ApprovedStockNewService.CreateApprovedStockNew(approvedStockNew);
    }

    private void StoreToRejectedNews(StockNew stockNew)
    {
        RejectedStockNew rejectedStockNew = new RejectedStockNew();
        rejectedStockNew.NewsId = stockNew.NewsId;
        rejectedStockNew.NewsDate = stockNew.NewsDate;
        rejectedStockNew.NewsTitle = stockNew.NewsTitle;
        rejectedStockNew.NewsDescription = stockNew.NewsDescription;
        rejectedStockNew.NewsContent = stockNew.NewsContent;
        rejectedStockNew.NewsSource = stockNew.NewsSource;
        rejectedStockNew.ShareSymbol = stockNew.ShareSymbol;
        rejectedStockNew.UseUrl = stockNew.UseUrl;
        rejectedStockNew.NewsUrl = stockNew.NewsUrl;
        rejectedStockNew.LanguageID = stockNew.LanguageID;
        rejectedStockNew.ImageUrl = stockNew.ImageUrl;
        rejectedStockNew.LinkId = stockNew.LinkId;
        rejectedStockNew.OriginalUrl = stockNew.OriginalUrl;
        rejectedStockNew.RejectedDate = DateTime.Now;
        RejectedStockNewService.CreateRejectedStockNew(rejectedStockNew);
    }
}
