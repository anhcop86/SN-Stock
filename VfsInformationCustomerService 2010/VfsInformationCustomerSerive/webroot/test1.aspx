<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="test1.aspx.cs" Inherits="test1" Title="Import Stock Information" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><input type="button"  id="importUpdateButton" runat="server" onserverclick="importUpdateButton_Click"  />
            <input type="button"  id="ShowInforMarket" runat="server" onserverclick="ShowInforMarketUpdateButton_Click"  />
    </div>
    <div><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.Date %></label>
    </div>
    <div><ww:jquerydatepicker id="importDateInput" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                        width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01"></ww:jquerydatepicker>
        <asp:RequiredFieldValidator ID="importDateRequiredFieldValidator" runat="server" ControlToValidate="importDateInput"
            ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator></div>
    <div><label><%= Resources.UIResource.SelectFile%></label>
    </div>
    <div>
        <asp:FileUpload id="uploadFile" runat="server"/>
        <asp:RequiredFieldValidator ID="uploadFileRequiredFieldValidator" runat="server" ControlToValidate="uploadFile"
        ErrorMessage="uploadFileRequired" EnableClientScript="false"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Literal runat="server" ID="successMessage"></asp:Literal>
    </div>    
    
</asp:Content>

