<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollectColumnUC.ascx.cs" Inherits="UserControl_CollectColumnUC" %>

<div>
    <input type="button" id="importStep1Button" runat="server" onserverclick="ImportStep1Button_Click" />            
    <input type="button" id="previewMessageButton" runat="server" onserverclick="PreviewMessageButton_Click" />            
</div>

<div><label><%= Resources.UIResource.SelectParameater%> </label>
</div>
<div>
    <table>
        <tr>
            <td style="width:200px"><%= Resources.UIResource.ParamaterName%></td>
            <td style="width:200px"><%= Resources.UIResource.ColumnName%></td>
        </tr>
        <tr>
            <td style="width:200px"><asp:PlaceHolder runat="server" id="CollectColumnTable"></asp:PlaceHolder></td>
            <td style="width:200px"><asp:PlaceHolder runat="server" id="MatchColumnTable"></asp:PlaceHolder></td>
        </tr>
        <tr>
            <td style="width:200px" colspan="2"><asp:Literal ID="previewLiteral" runat="server"></asp:Literal></td>            
        </tr>
    </table>       
</div>
<div><%= Resources.UIResource.ImportResult%></div><br />
<div><%= Resources.UIResource.SuccessMessages%>: <%= SuccessCount%></div>
<div><%= Resources.UIResource.FailedMessages%>: <%= FailedCount%></div>
<div><asp:Literal runat="server" ID="successMessage"></asp:Literal>
</div>