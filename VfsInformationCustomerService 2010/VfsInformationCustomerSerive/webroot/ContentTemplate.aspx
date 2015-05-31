<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContentTemplate.aspx.cs" Inherits="CustomerService_ModelContent"%>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Danh mục nội dung mẫu</h1>    
    <div><asp:Button CssClass="button" ID="InsertButton" runat="server" OnClick="InsertButton_Click" Text="Thêm" />                </div>
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" runat="server">
    </cc1:Pager>    
    
    <div>
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand" OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td style="width:300px"><b><%=Resources.UIResource.TypeTemplate%></b></td>
                                    <td style="width:200px"><b><%=Resources.UIResource.ServiceType %></b></td>
                                    <td style="width:200px"><b><%=Resources.UIResource.SenderForContentTemplate%></b></td>
                                    <td style="width:200px"><b><%=Resources.UIResource.ReceiverForContentTemplate%></b></td>  
                                     <td style="width:30px"></td>                                
                                     <td style="width:30px"></td>    
                                     <td style="width:80px"></td>                             
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id='<%#Eval("ContentTemplateID") %>'>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#Eval("Description")%> </a></td>
                                    <td><%#GetNameServiceType(Eval("ServiceTypeID"))%></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>                                    
                                    <td><a href="javascript:void();"><img id="Img1" alt=""  onclick="return DeletePost(this);" src="_assets/img/deleteButon.gif" /></a></td>
                                    <td><span id="result-<%#Eval("ContentTemplateID") %>"></span></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id='<%#Eval("ContentTemplateID") %>'>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#Eval("Description")%> </a></td>
                                    <td><%#GetNameServiceType(Eval("ServiceTypeID"))%></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>
                                    <td><a href="javascript:void();"><img id="deleteImage" alt=""  onclick="return DeletePost(this);" src="_assets/img/deleteButon.gif" /></a></td>
                                    <td><span id="result-<%#Eval("ContentTemplateID") %>"></span></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
     
     <script type="text/javascript">
         function DeletePost(obj) {

             var id = $(obj).closest("tr").attr("id");
             
             var srv = $(obj).closest("table").attr("id");
             var that = $("[id$='" + id + "']");
             var dto = { "id": id };
             var currPage = $('#PagerCurrentPage').html();

             $.ajax({
                 url: SiteVars.ApplicationRelativeWebRoot + "/api/TemplateServices.asmx/DeleteTemplate", // dc khai báo trong file masterPage
                 data: JSON.stringify(dto),
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 beforeSend: onAjaxBeforeSend,
                 success: function (result) {
                     var rt = result.d;
                     if (rt.Success) {
                         //$(that).fadeOut(500, function () {
                         //    $(that).remove();
                         //});
                         //ShowStatus("success", rt.Message);
                         $('#result-' + id).text(rt.Message)
                         
                         
                     }
                     else {
                         //ShowStatus("warning", rt.Message);
                         $('#result-' + id).text(rt.Message)
                         
                     }
                 }
             });
             return false;
         }

         function onAjaxBeforeSend(jqXHR, settings) {

             // AJAX calls need to be made directly to the real physical location of the
             // web service/page method.  For this, SiteVars.ApplicationRelativeWebRoot is used.
             // If an AJAX call is made to a virtual URL (for a blog instance), although
             // the URL rewriter will rewrite these URLs, we end up with a "405 Method Not Allowed"
             // error by the web service.  Here we set a request header so the call to the server
             // is done under the correct blog instance ID.

             //jqXHR.setRequestHeader('x-blog-instance', SiteVars.BlogInstanceId);
         }


         </script>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" id="bottomPaging" runat="server">
    </cc1:Pager>   
</asp:Content>

