<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessage.aspx.cs" Inherits="CreateMessage" Title="Create Message" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><input type="button"  id="importCreateMessageButton" runat="server" onserverclick="importCreateMessageButton_Click" />            
    </div>
    <div><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.template%></label>
    </div>
    <div><asp:DropDownList runat="server" ID="templateDropDownList"></asp:DropDownList>
    </div>
    <div><label><%= Resources.UIResource.selectFile%></label>
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

