<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stocks.aspx.cs" Inherits="Stocks" %>
<%@ Register Src="User controls/PostListStocks.ascx" TagName="PostListStocks" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
   <div id="divError" runat="Server" />
   <p><iframe style="height: 25px; width: 600px;"
     src="http://testsbsgateway.vfs.com.vn:9991/SnapShotForBlog.aspx" frameborder="0" scrolling="no"></iframe></p>
  <uc1:PostListStocks ID="PostList1" runat="server" />
    
  <blog:PostCalendar runat="server" ID="calendar" 
    EnableViewState="false"
    ShowPostTitles="true" 
    BorderWidth="0"
    NextPrevStyle-CssClass="header"
    CssClass="calendar" 
    WeekendDayStyle-CssClass="weekend" 
    OtherMonthDayStyle-CssClass="other" 
    UseAccessibleHeader="true"
    Visible="false" 
    Width="100%" />
</asp:Content>
