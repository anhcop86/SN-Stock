<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExportList.aspx.cs" Inherits="ExportList" Title="Untitled Page" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<div><input type="button"  id="exportButton" runat="server" onserverclick="exportButton_ServerClick" />
</div>
<div><label id="exportDateLable" for="exportDateInput"><%= Resources.UIResource.date %></label>
</div>
<div>
    <ww:jquerydatepicker id="exportDateInput" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                        width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
    </ww:jquerydatepicker>
    <asp:RequiredFieldValidator ID="ErrorDateInput" runat="server" ControlToValidate="exportDateInput"
                    ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator>
</div>
<div><label id="exportMarketLable" for="exportMarketInput"><%= Resources.UIResource.market %></label>    
</div>
<div>
    <select runat="server" id="exportMarketInput">
        <option value="HOSE">HOSE</option>
        <option value="HASTC">HASTC</option>
    </select>
</div>
</asp:Content>

