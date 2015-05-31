<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessageFromExcelStep2.aspx.cs" Inherits="CreateMessageFromExcelStep2" Title="Create message from Excel step 2" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<%@ Register Assembly="Jiffycms.Net.Toolkit" Namespace="Jiffycms.Net.Toolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc" TagName="CollectColumn" Src="~/UserControl/CollectColumnUC.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">    
    <uc:CollectColumn id="CollectColumnUc1" runat="server" />
</asp:Content>
