<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Quản lý tài khoản nhân viên</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <ul class="nav nav-tabs">
        <li class="active"><a data-toggle="tab" href="#home">Admin's Profile</a></li>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách Tài Khoản Con</h3>
        <center>
         <span runat="server" id="spnSoTK"
            class="badge">Bind so tai khoan / tong so duoc cap</span>
           </center>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSTK">
                <ItemTemplate>
                    <a href="./EditNhanVien.aspx?id=<%#Eval("MaNV") %>" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblVTCV" Text='<%#Eval("ViTriCongViec.TenVTCV") %>'></asp:Label>

                        </h4>
                        <asp:Label runat="server" ID="lblBP" Text='<%#Eval("ViTriCongViec.BoPhan.TenBP") %>'></asp:Label>
                        <br />
                        <span runat="server" id="spnTT" class="badge"><%#(bool)Eval("TrangThai")?"Actvie":"Inactive" %></span>
                        <asp:Label runat="server" ID="lblTenNV" Text='<%#Eval("HoTen") %>'></asp:Label>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate"
                OnClick="btnCreate_Click"
                Text="Create New Account" class="btn btn-info" />
        </div>
    </div>
</asp:Content>

