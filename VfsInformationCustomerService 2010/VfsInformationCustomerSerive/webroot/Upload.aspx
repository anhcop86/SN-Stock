<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" Title="Untitled Page" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<div>
    <label><%= Resources.UIResource.SelectFile%></label>
</div>
<div>
    <asp:FileUpload id="uploadFile" runat="server"/>
</div>
</asp:Content>

