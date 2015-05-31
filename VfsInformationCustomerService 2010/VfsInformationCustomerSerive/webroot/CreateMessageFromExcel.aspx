<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessageFromExcel.aspx.cs" Inherits="CreateMessageFromExcel" Title="Create message from Excel step 1" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<%@ Register Assembly="Jiffycms.Net.Toolkit" Namespace="Jiffycms.Net.Toolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><input type="button"  id="importStep1Button" runat="server" onserverclick="ImportStep1Button_Onclick" />            
    </div>
     <div>
        <asp:Literal runat="server" ID="successMessage"></asp:Literal>
    </div>    
    <div><label><%= Resources.UIResource.SelectFile%></label>
    </div>
    <div>
        <asp:FileUpload id="SourceFile" runat="server"/>
        <asp:RequiredFieldValidator ID="uploadFileRequiredFieldValidator" runat="server" ControlToValidate="SourceFile"
        ErrorMessage="" EnableClientScript="false"></asp:RequiredFieldValidator>     
    </div>
    <div><label><%= Resources.UIResource.Template%></label>
    </div>
    <div>
       <select id="contentTemplateDropDownList" runat="server" name="contentTemplateDropDownList">
       </select>       
    </div>   
    <div><%=Resources.UIResource.BodyMessager%></div>
    <div id="InputBodyMessager" style="width:600px; height:300px" runat="server" >
    </div>
   
</asp:Content>

