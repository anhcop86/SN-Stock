<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InfoPosNochangeDownOfStock.aspx.cs" Inherits="InfoPosNochangeDownOfStock"%>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div>
        <fieldset border="1">
            <legend>Tìm kiếm theo:</legend>
                <table>                    
                    <tr>
                        <td><b><%=Resources.UIResource.FromDate%></b></td>
                        <td><ww:jquerydatepicker id="FromDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                        </td>
                        <td><b><%=Resources.UIResource.ToDate%></b></td>
                        <td><ww:jquerydatepicker id="ToDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                        </td>
                    </tr>                                        
                    <tr>
                        <td><asp:Button class="button" id="SearchInput" runat="server" OnClick="SearchInput_Click" /></td>                        
                    </tr>
                </table>
        </fieldset>
    </div>  
    <hr class="widthForTable" />
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server">
                            <HeaderTemplate>
                                <tr>                                    
                                    <td style="width:300px"><b><%=Resources.UIResource.Market%></b></td>                                    
                                    <td style="width:300px"><b><%=Resources.UIResource.TrandingDate%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.Pos%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.Nochange%></b></td>                                   
                                    <td style="width:300px"><b><%=Resources.UIResource.Down%></b></td>                                    
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>                               
                                    <td><%#Eval("Market")%></td>
                                    <td><%# Vfs.WebCrawler.Utility.ApplicationHelper.ConvertStringToFormatDate (Eval("date"))%></td>
                                    <td><%#Eval("Pos")%></td>
                                    <td><%#Eval("Nochange")%></td>
                                    <td><%#Eval("Down")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>                                   
                                    <td><%#Eval("Market")%></td>
                                    <td><%#Vfs.WebCrawler.Utility.ApplicationHelper.ConvertStringToFormatDate(Eval("date"))%></td>
                                    <td><%#Eval("Pos")%></td>
                                    <td><%#Eval("Nochange")%></td>
                                    <td><%#Eval("Down")%></td>                                                                       
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
</asp:Content>
