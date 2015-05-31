<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessage.aspx.cs" Inherits="CreateMessage" Title="Create Message" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><h1 style="font-size:25px">Gởi Tin</h1> </div>
    <div>
    <div><input type="button" id="importCreateMessageButton" runat="server" onserverclick="importCreateMessageButton_Click" value="Create Message" />            
    </div>
    <div>
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
    </div>  
    </div>              
    <br />
    <div>
        <input type="button" runat="server" id="SendMessengerButton" onserverclick="SendMessengerButton_onserverclick" />
    </div>
    <div>
          <asp:Menu
            ID="Menu1"
            Width="168px"
            runat="server"
            Orientation="Horizontal"
            StaticEnableDefaultPopOutImage="False"
            OnMenuItemClick="Menu1_MenuItemClick">
            <Items>                
                <asp:MenuItem ImageUrl="~/_assets/img/selectedtab2.GIF" Text=" " Value="0"></asp:MenuItem>
                <asp:MenuItem ImageUrl="~/_assets/img/selectedtab.GIF" Text=" " Value="1"></asp:MenuItem>                
                <asp:MenuItem Enabled="false" ImageUrl="~/_assets/img/selectedtab3.GIF"></asp:MenuItem>                                                
            </Items>
        </asp:Menu>
        <asp:MultiView runat="server" ID="MultiviewCustomer" ActiveViewIndex="0">    
            <asp:View ID="TabMoTK" runat="server">
            <div><h1>Khách hàng đã mở tài khoản</h1>            </div>
               <div>
                    <div><label runat="server" id="infoTab1Label"></label></div>
                <fieldset border="1">
                    <legend>Tìm kiếm khách hàng:</legend>
                        <br />
                        <table>
                            <tr>
                                <td><b><%=Resources.UIResource.CustomerId%></b></td>
                                <td><input id="InputSearchCustomerid" runat="server" maxlength="10"/></td>
                            </tr>                
                            <tr>
                                <td><b><%=Resources.UIResource.CustomerName%></b></td>
                                <td><input id="InputSearchCustomerName" runat="server" maxlength="100"/></td>
                            </tr>
                            <tr>
                                <td><b><%=Resources.UIResource.CustomerNameViet%></b></td>
                                <td><input id="InputSearchCustomerNameViet" runat="server" maxlength="100"/></td>
                            </tr>
                            <tr>
                                <td><b><%=Resources.UIResource.SearchEmail%></b></td>
                                <td><input id="InputSearchEmail" runat="server" maxlength="100"/></td>
                            </tr>
                            <tr>
                                <td><b><%=Resources.UIResource.TelDetail%></b></td>
                                <td><input id="InputTel" runat="server" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td><asp:Button Text="Tìm" CssClass="button" id="SearchInputTab1" runat="server" OnClick="SearchInputTab1_Click" /></td>                        
                                <td class="totalCustomer"><%= getTotalCustomer()%></td>                                            
                            </tr>                            
                        </table>                        
                </fieldset>                
            </div>             
            
                <br />
                  <div  class="createmesseng_div">   
                    <table class="widthForTable">            
                        <tr>
                            <td>
                                <table>
                                    <asp:Repeater ID="RepeaterDataTab1" runat="server" >
                                        <HeaderTemplate>
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                                <td><input type="checkbox" id="checkAll" onclick="CheckAllForNewsList(this);"/></td>
                                                <td style="width:100px"><a runat="server" onserverclick="SortCustomerIdListtab1" href="#"><b><%=Resources.UIResource.CustomerIdList%></b></a><a><%#GetOrderDirectionIndicatorTab1("CustomerId")%></a></td>                                    
                                                <td style="width:250px"><a runat="server" onserverclick="SortCustomerNameTab1" href="#"><b><%=Resources.UIResource.CustomerNameList%></b></a><a><%#GetOrderDirectionIndicatorTab1("CustomerNameViet")%></a></td>
                                                <td style="width:100px"><a runat="server" onserverclick="SortDOBTab1" href="#"><b><%=Resources.UIResource.DobList%></b></a><a><%#GetOrderDirectionIndicatorTab1("Dob")%></a></td>                                   
                                                <td style="width:100px"><a runat="server" onserverclick="SortMobileTab1" href="#"><b><%=Resources.UIResource.Mobile1List%></b></a><a><%#GetOrderDirectionIndicatorTab1("Mobile")%></a></td>                                   
                                                <td style="width:210px"><a runat="server" onserverclick="SortEmailTab1" href="#"><b><%=Resources.UIResource.EmailList%></b></a><a><%#GetOrderDirectionIndicatorTab1("Email")%></a></td>                                    
                                            </tr>
                                        </HeaderTemplate>   
                                        <ItemTemplate>
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                                <td><input id="selectedItem" name="selectedItem" type="checkbox" value="<%#Eval("CustomerId")%>" onclick="SingleCheckForNewsList(this);"/></td>
                                                <td><%#Eval("CustomerId")%></td>
                                                <td><%#Eval("CustomerNameViet")%> </a></td>
                                                <td><%#ConverFormat(Eval("Dob"))%></td>
                                                <td><%#Eval("Mobile") %></td>
                                                <td><%#Eval("Email")%></td>                                                                                                      
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate> 
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                            <td><input id="selectedItem" name="selectedItem" type="checkbox" value="<%#Eval("CustomerId")%>" onclick="SingleCheckForNewsList(this);"/></td>
                                               <td><%#Eval("CustomerId")%></td>
                                                <td><%#Eval("CustomerNameViet")%> </a></td>
                                                <td><%#ConverFormat(Eval("Dob"))%></td>
                                                <td><%#Eval("Mobile") %></td>
                                                <td><%#Eval("Email")%></td>                                                                                                     
                                            </tr>
                                        </AlternatingItemTemplate>                    
                                    </asp:Repeater>
                                </table>
                            </td>                
                        </tr>
                    </table>
                </div>
                
            </asp:View>
            <asp:View ID="TabChuaMoTK" runat="server">
            <div><h1>Khách hàng chưa mở tài khoản</h1> </div>
                <div>                  
                  <div><label runat="server" id="infoTab2Label"></label></div>
                    <fieldset border="1">
                        <legend>Tìm kiếm khách hàng:</legend>
                            <br />
                            <table>
                                <tr>
                                    <td><b><%=Resources.UIResource.CustomerId%></b></td>
                                    <td><input id="InputSearchCustomeridTab2" runat="server" maxlength="10"/></td>
                                </tr>                
                                <tr>
                                    <td><b><%=Resources.UIResource.CustomerName%></b></td>
                                    <td><input id="InputSearchCustomerNameTab2" runat="server" maxlength="100"/></td>
                                </tr>
                                <tr>
                                    <td><b><%=Resources.UIResource.CustomerNameViet%></b></td>
                                    <td><input id="InputSearchCustomerNameVietTab2" runat="server" maxlength="100"/></td>
                                </tr>
                                <tr>
                                    <td><b><%=Resources.UIResource.SearchEmail%></b></td>
                                    <td><input id="InputSearchEmailTab2" runat="server" maxlength="100"/></td>
                                </tr>
                                <tr>
                                    <td><b><%=Resources.UIResource.TelDetail%></b></td>
                                    <td><input id="InputTelTab2" runat="server" maxlength="20" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Button Text="Tìm" CssClass="button" id="SearchInputTab2" runat="server" OnClick="SearchInputTab2_Click" /></td>                        
                                     <td class="totalCustomer"><%= getTotalCustomer()%></td>  
                                </tr>
                            </table>
                    </fieldset>
                </div>
                <br />
                <div  class="createmesseng_div">   
                    <table class="widthForTable">            
                        <tr>
                            <td>
                                <table>
                                    <asp:Repeater ID="RepeaterDataTab2" runat="server">
                                        <HeaderTemplate>
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                                <td><input id="checkAll" type="checkbox" onclick="CheckAll(this);" /></td>                        
                                                <td style="width:100px"><a id="A1" runat="server" onserverclick="SortCustomerIdListtab2" href="#" ><b><%=Resources.UIResource.CustomerIdList%></b></a><a><%#GetOrderDirectionIndicatorTab2("CustomerId")%></a></td>                                    
                                                <td style="width:250px"><a id="A2" runat="server" onserverclick="SortCustomerNameTab2" href="#"><b><%=Resources.UIResource.CustomerNameList%></b></a><a><%#GetOrderDirectionIndicatorTab2("CustomerNameViet")%></a></td>
                                                <td style="width:100px"><a id="A3" runat="server" onserverclick="SortDOBTab2" href="#"><b><%=Resources.UIResource.DobList%></b></a><a><%#GetOrderDirectionIndicatorTab2("Dob")%></a></td>                                   
                                                <td style="width:100px"><a id="A4" runat="server" onserverclick="SortMobileTab2" href="#"><b><%=Resources.UIResource.Mobile1List%></b></a><a><%#GetOrderDirectionIndicatorTab2("Mobile")%></a></td>                                   
                                                <td style="width:210px"><a id="A5" runat="server" onserverclick="SortEmailTab2" href="#"><b><%=Resources.UIResource.EmailList%></b></a><a><%#GetOrderDirectionIndicatorTab2("Email")%></a></td>                                    
                                            </tr>
                                        </HeaderTemplate>   
                                        <ItemTemplate>
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                                <td><input type="checkbox"  id="CheckBoxSendTab2" name="CheckBoxSendTab2" value="<%#Eval("CustomerId")%>" onclick="SingleCheck(this)"  /></td>                               
                                                <td><%#Eval("CustomerId")%></td>
                                                <td><%#Eval("CustomerNameViet")%> </a></td>
                                                <td><%#ConverFormat(Eval("Dob"))%></td>
                                                <td><%#Eval("Mobile") %></td>
                                                <td><%#Eval("Email")%></td>                                                                        
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate> 
                                            <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                               <td><input type="checkbox"  id="CheckBoxSendTab2" name="CheckBoxSendTab2"  value="<%#Eval("CustomerId")%>" onclick="SingleCheck(this)"  /></td>
                                               <td><%#Eval("CustomerId")%></td>
                                                <td><%#Eval("CustomerNameViet")%> </a></td>
                                                <td><%#ConverFormat(Eval("Dob"))%></td>
                                                <td><%#Eval("Mobile") %></td>
                                                <td><%#Eval("Email")%></td>                                                                                                                        
                                            </tr>
                                        </AlternatingItemTemplate>                    
                                    </asp:Repeater>
                                </table>
                            </td>                
                        </tr>
                    </table>
                </div>
                
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

