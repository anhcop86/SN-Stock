<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLTieuChi.aspx.cs" Inherits="Admin_QLNTC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <ul class="nav nav-tabs">
        <li class="active"><a data-toggle="tab" href="#home">Profile</a></li>
    </ul>
    <div class="tab-content">
        <div id="home" class="tab-pane fade in active">
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Họ và Tên:

                            </th>
                            <td>
                                <asp:Label runat="server" ID="lblHoTen"></asp:Label></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>Mã Nhân Viên:</th>
                            <td>
                                <asp:Label runat="server" ID="lblMaNV"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>Chức Danh Vị Trí Công Việc:</th>
                            <td>
                                <asp:Label runat="server" ID="lblVTCV"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>Bộ Phận Trực Thuộc:</th>
                            <td>
                                <asp:Label runat="server" ID="lblBoPhan"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--co the tu dua them cac du lieu lien quan den nhom tieu chi vao-->
    <div class="panel-heading">
        <h3 class="text-center">Danh sách tiêu chí</h3>
        <span runat="server" id="spnSoTK"
            class="badge">?/ Tổng số</span>
    </div>
    <div class="panel-body">
        <asp:ListView runat="server" ID="lvDSNTC" class="list-group">
            <ItemTemplate>
                <div class="list-group-item">
                    <span class="badge" runat="server" id="spnTrongSo">Tỉ trọng: <%#Eval("TrongSo") %>%</span>
                    
                    <asp:LinkButton runat="server" class="badge" ID="lbtnTTTC"
                                OnClick="lbtnEditTTNTC_Click"
                                CommandArgument='<%#Eval("MaNTC") %>'>
                                <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                            </asp:LinkButton>
                    
                    <h4 class="list-group-item-heading" runat="server" id="h4TenNTC">
                        <asp:LinkButton runat="server" ID="lbtnTenNTC"
                            OnClick="lbtnEditNTC_Click"
                            CommandArgument='<%#Eval("MaNTC") %>'
                            Text='<%#Eval("TenNTC") %>'></asp:LinkButton>
                    </h4>
                </div>
                <asp:HiddenField runat="server" ID="hdfNTC"
                    Value='<%#Eval("MaNTC") %>' />
                <asp:ListView runat="server" ID="lvDSTC" class="list-group">
                    <ItemTemplate>
                        <div class="list-group-item">
                            <span class="badge" runat="server" id="spnTrongSo">Tỉ trọng: <%#Eval("TrongSo") %>%</span>
                            
                            <asp:LinkButton runat="server" class="badge" ID="lbtnTTTC"
                                OnClick="lbtnEditTTTC_Click"
                                CommandArgument='<%#Eval("MaTC") %>'>
                                <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                            </asp:LinkButton>
                            
                            <h5 class="list-group-item-heading" runat="server" id="h5TenTC">
                                 <asp:LinkButton runat="server" ID="lbtnTenTC"
                                    OnClick="lbtnEditTC_Click"
                                    CommandArgument='<%#Eval("MaTC") %>'
                                    Text='<%#Eval("TenTC") %>'></asp:LinkButton>
                            </h5>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ItemTemplate>
        </asp:ListView>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate"
                OnClick="btnCreate_Click"
                Text="Them tieu chi" class="btn btn-info" />
            <asp:Button runat="server" ID="btnCreateNTC"
                OnClick="btnCreateNTC_Click"
                Text="Them Nhom tieu chi" class="btn btn-info" />
        </div>
    </div>
</asp:Content>

