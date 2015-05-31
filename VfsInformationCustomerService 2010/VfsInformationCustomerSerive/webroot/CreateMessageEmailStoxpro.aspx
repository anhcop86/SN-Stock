<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessageEmailStoxpro.aspx.cs" Inherits="CreateMessageEmailStoxpro" Title="Untitled Page" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Gởi Mail tặng Phần mềm Phân tích chứng khoán StoxPro</h1>            
    <table>
            <tr>
                <td style="width:90px"><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.Template%>:</label></td>
                <td style="width:220px"><asp:DropDownList runat="server" ID="templateDropDownList"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><label><%= Resources.UIResource.SelectFile%>:</label></td>
                <td><asp:FileUpload id="uploadFile" runat="server"/></td>
                <td><label runat="server" id="infoError"></label></td>
            </tr>            
        </table>
        <div>
        <input type="button" runat="server" id="SendMessengerButton" onserverclick="SendMessengerButton_onserverclick" />
    </div>
</asp:Content>

