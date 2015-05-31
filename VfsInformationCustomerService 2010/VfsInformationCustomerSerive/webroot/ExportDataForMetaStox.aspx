<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExportDataForMetaStox.aspx.cs" Inherits="ExportDataForMetaStox" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h2>Xuất dữ liệu phân tích</h2>
    <div><input type="button"  id="exportButton" runat="server" onserverclick="exportButton_ServerClick" />
</div>
<div><label id="exportDateLable" for="exportDateInput"><%= Resources.UIResource.Date %></label>
</div>
<div>
    <ww:jquerydatepicker id="exportDateInput" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                        width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
    </ww:jquerydatepicker>
    <asp:RequiredFieldValidator ID="ErrorDateInput" runat="server" ControlToValidate="exportDateInput"
                    ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator>
</div>
<div><label id="exportMarketLable" for="exportMarketInput"><%= Resources.UIResource.Market %></label>    
</div>
<div>
    <select runat="server" id="exportMarketInput">
        <option value="6">HOSE</option>
        <option value="4">HASTC</option>
        <option value="1">Index gồm CCQ</option>
    </select>
</div>
</asp:Content>
