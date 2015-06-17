<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLNhomTC.aspx.cs" Inherits="Admin_QLNhomTC" %>

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
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách nhom tieu chi</h3>
        <span runat="server" id="spnSoTK"
            class="badge">?/ Tổng số</span>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSDV">
                <ItemTemplate>
                    <a href="#" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblTenBP" Text="Ben bo phan"></asp:Label>
                        </h4>
                        <span runat="server" id="spnTrangThai"
                            class="badge">Active</span>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate" 
                 OnClick="btnCreate_Click"
                Text="Them nhom tieu chi" class="btn btn-info" />
        </div>
    </div>
</asp:Content>

