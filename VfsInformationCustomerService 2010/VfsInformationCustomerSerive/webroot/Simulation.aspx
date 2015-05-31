<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Simulation.aspx.cs" Inherits="CustomerService_Simulation" Title="Mo phong" %>
<%@ Register Assembly="Jiffycms.Net.Toolkit" Namespace="Jiffycms.Net.Toolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
 <div>
    <asp:Literal runat="server" ID="messageLiteral" Visible="false"></asp:Literal>
 </div>
 <div>
 <input type="button"  id="sendButton" runat="server"  value="Create Message" onserverclick="Send_Click" />            
    </div>
    <div><label id="importDateLable" for="templateDropDownList"><%= Resources.UIResource.Template%></label>
    </div>
    <div><asp:DropDownList runat="server" ID="templateDropDownList" AutoPostBack="true" OnSelectedIndexChanged="templateDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
    </div>
    <div><label><%= Resources.UIResource.Receiver%><span class="inforError">(*)</span></label>
    </div>
    <div>        
        <asp:TextBox id="receiverTextBox" runat="server" />
        <asp:RequiredFieldValidator ID="receiverTextBoxRequiredFieldValidator" runat="server" ControlToValidate="receiverTextBox"
        ErrorMessage="receiverInfoRequired" EnableClientScript="false"></asp:RequiredFieldValidator>
    </div>
    <div><label><%= Resources.UIResource.ServiceType%></label></div>     
    <div><input id="serviceTypeInput" runat="server" readonly="readonly" /></div>         
    <div><label for="contentTemplateAttchementDropDownList"><%=Resources.UIResource.Attachement %></label></div>
    <div><asp:ListBox runat="server" ID="contentTemplateAttchementDropDownList"></asp:ListBox></div>
    <div><label for="InputBodyMessager"><%=Resources.UIResource.BodyMessager%></label></div>
    <div><cc1:Editor runat="server" ID="InputBodyMessager" readonly="readonly"></cc1:Editor></div>    
</asp:Content>

