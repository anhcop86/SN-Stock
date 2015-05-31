<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="NewsList" Title="VFS - News crawler" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<p >
    
    <div>
        <asp:Button ID="approveButton" runat="server" OnClick="approveButton_Click"/>
        <input type="button" id="rejectedButton" runat="server" onserverclick="rejectedButton_Click"/>
    </div>
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" PageSize="10" runat="server">
    </cc1:Pager>
        <table width="1000" border="0">  
            <tr class="title">                
                <td><input type="checkbox" id="checkAll" onclick="CheckAllForNewsList(this);"/></td>
                <td><input type="checkbox" id="CheckAllHome" onclick="CheckAllForHome(this);"/><%=Resources.UIResource.HotNews %></td>
                <td><%= Resources.NewsList.title %></td>
                <td><%= Resources.NewsList.newsDate %></td>
                <td><%= Resources.NewsList.newsGroup %></td>                
                <td style="text-align:center"><%= Resources.NewsList.detail %></td>                
                <td width="0%"></td>                
            </tr>
            <tr>
                <td colspan="7" style="vertical-align:top">
                    <table width="100%" height="252" border="0" align="left">
                        <asp:Repeater ID="newsRepeater" runat="server">
                            <ItemTemplate>                               
                                <tr>                                    
                                    <td><input id="selectedItem_<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId%>" name="selectedItem_<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId%>" type="checkbox" value="<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId %>" onclick="SingleCheckForNewsList(this);"/></td>
                                    <td><input id="selectedGroupNewsItem" name="selectedGroupNewsItem" type="checkbox" value="<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId %>" onclick="SingleCheckItemGroupNews(this);"/></td>                                    
                                    <td width="50%"><%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsTitle%></td>
                                    <td><%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsDate.ToShortDateString()%></td>
                                    <td width="30%">
                                        <select id="newsCategorySelect_<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId %>" name="newsCategorySelect_<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId %>">
                                            <option value="0" ><%= Resources.NewsList.marketNews %></option>
                                            <option value="1"><%= Resources.NewsList.updateMarketNews %></option>
                                            <option value="2" selected="selected"><%= Resources.NewsList.announcement %></option>
                                            <option value="3"><%= Resources.NewsList.stockNews %></option>
                                        </select>
                                    </td>
                                    <td width="15%"><a href="#" onclick="showNewDetail(<%# ((Vfs.WebCrawler.Entities.StockNew)(Container.DataItem)).NewsId %>);" ><%= Resources.NewsList.viewDetail %></a>
                                    </td>  
                                      <td width="0%"></td>                                    
                                </tr>                                
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
                <td style="vertical-align:top">
                    <table width="500" height="123" border="0">
                        <tr class="title">
                            <td width="446" ><%= Resources.NewsList.title%>
                            </td>
                        </tr>
                        <tr>
                            <td width="446" id="newTitle">
                            </td>
                        </tr>
                         <tr class="title">
                            <td width="446"><%= Resources.NewsList.content %>
                            </td>
                        </tr>
                        <tr>
                            <td width="446" id="newContent">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" id="bottomPaging" PageSize="10" runat="server">
    </cc1:Pager>        
</p>
</asp:Content>

