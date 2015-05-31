<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SnapShot.aspx.cs" Inherits="SnapShot" Title="Untitled Page" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><input type="button"  id="importUpdateButton" runat="server" onserverclick="CreateSnapShotButton_Click" />            
    </div>    
    <div><label><%= Resources.UIResource.SelectFile%></label>
    </div>
    <div>
        <asp:FileUpload id="snapShotFile" runat="server"/>
        <asp:RequiredFieldValidator ID="snapShotFileRequiredFieldValidator" runat="server" ControlToValidate="snapShotFile"
        ErrorMessage="uploadFileRequired" EnableClientScript="false"></asp:RequiredFieldValidator>     
    </div>
    <div>
        <asp:Literal runat="server" ID="successMessage"></asp:Literal>
    </div>
</asp:Content>


