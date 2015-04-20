<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Economics.aspx.cs" Inherits="Economics" %>
<%@ Register Src="User controls/PostListStocks.ascx" TagName="PostListStocks" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <div id="divError" runat="Server" />
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