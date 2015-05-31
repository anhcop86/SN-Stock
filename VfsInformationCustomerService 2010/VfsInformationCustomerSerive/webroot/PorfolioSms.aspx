<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PorfolioSms.aspx.cs" Inherits="PorfolioSms" Title="" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<div><h1 style="font-size:25px"><%=Resources.UIResource.PorfolioSMS %></h1> </div>    
    <div>
        <table>
            <tr>
                <td style="width:90px"><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.Symbol%>:</label></td>
                <td style="width:220px"><asp:TextBox MaxLength="3" ID="TitleTextBox" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="TitleTextBoxValidate" EnableClientScript="false" runat="server" ControlToValidate="TitleTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><label><%= Resources.UIResource.Content%>:</label></td>
                <td><asp:TextBox Rows="10" ID="ContentTextBox" runat="server" Columns="20"  TextMode="MultiLine"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="ContentTextBoxValidate" EnableClientScript="false" runat="server" ControlToValidate="ContentTextBox"></asp:RequiredFieldValidator></td>
                <td><label runat="server" id="infoError"></label></td>
            </tr>
        </table>
    </div>      
    <br />
    <div>
        <table>
            <tr>
                <td><input type="button" runat="server" id="SendMessengerButton" onserverclick = "SendMessengerButton_Click"/></td>
                <td><input type="button" runat="server" id="DeleteMessengerButton" onserverclick = "DeleteMessengerButton_Click"/></td>
            </tr>
        </table>
        
    </div>
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" >
                            <HeaderTemplate>
                                <tr>
                                    <td><input id="checkAll" type="checkbox" onclick="CheckAll(this);" /></td>
                                    <td style="width:15%"><b><%=Resources.UIResource.StockSymbol%></b></td>                                    
                                    <td style="width:70%"><b><%=Resources.UIResource.Content%></b></td>
                                    <td style="width:10%"><b><%=Resources.UIResource.createDateMessage%></b></td>                                                                       
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>                                
                                    <td><input type="checkbox"  id="CheckBoxDelete" name="CheckBoxDelete" value='<%#Eval("ExtensionMessageID") %>' onclick="SingleCheck(this)"  /></td>                                    
                                    <td><%#Eval("Title")%></td>
                                    <td><%#Eval("Content") %></td>                                                                                                            
                                    <td><%#Vfs.WebCrawler.Utility.ApplicationHelper.returnDate(Eval("CreatedDate"))%></td>
                                   
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                   <td><input type="checkbox"  id="CheckBoxDelete" name="CheckBoxDelete" value='<%#Eval("ExtensionMessageID") %>' onclick="SingleCheck(this)" /></td>                                    
                                   <td><%#Eval("Title") %></td>
                                    <td><%#Eval("Content") %></td>                                                                                                            
                                    <td><%#Vfs.WebCrawler.Utility.ApplicationHelper.returnDate(Eval("CreatedDate"))%></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div> 
</asp:Content>