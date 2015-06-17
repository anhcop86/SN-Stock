<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLKetQuaDG.aspx.cs" Inherits="Admin_QLKetQuaDG" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenCK">dua ten chu ky danh gia vao day</h2>
    <p class="text-center">Quản Lý Danh Sách Người Được Đánh Giá</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Kết Quả Đánh Giá Trong Chu Kỳ</div>
            <div class="panel-body">
                <h1>dua bieu do chung vao day</h1>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Thêm người được đánh giá</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="jobposition">Người Được Đánh Giá: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" ID="ddlDSNV" class="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <button type="submit" class="btn btn-info">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách Người Được Đánh Giá</h3>
        <center><span runat="server" id="spnSoTK"
             class="badge">Bind so tai khoan / tong so duoc cap</span></center>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSDV">
                <ItemTemplate>
                    <a href="#" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblTenDN" Text="Bind Vi tri cong viec"></asp:Label>
                        </h4>
                        <span class="badge" runat="server" id="spnSoTKDS">Số tài khoản: 30</span>
                        <span runat="server" id="spnTT"
                            class="badge">Active</span>
                        <asp:Label runat="server" ID="lblTenNV" Text="Bind ho ten nha nvien vao day"></asp:Label>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>

