<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SACommon.master" AutoEventWireup="true" CodeFile="QLDonVi.aspx.cs" Inherits="Admin_QLDonVi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .donvi li {
            display: block;
        }

        .phantrang {
            padding-top: 10px;
            text-align: center;
        }
    </style>
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
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách Đơn Vị Sử Dụng Dịch Vụ</h3>
        <center><span runat="server" id="spnTong"
             class="badge">Bind so don vi/ tong so don vi su dung dich vu</span></center>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSDV">
                <%--moi don vi la 1 item cua listview nay--%>
                <ItemTemplate>
                    <a href="./EditDonVi.aspx?dv=<%#Eval("MaDV") %>" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblTenDN" Text='<%#Eval("TenDV") %>'></asp:Label>
                        </h4>
                        <span runat="server" id="spnSoTK" class="badge">Số tài khoản: <%#Eval("TongSoTK") %></span>
                        <span runat="server" id="spnTT" class="badge">Active</span>
                        <ul class="donvi">
                            <li>
                                <asp:Label runat="server" ID="lblNguoiDD" Text='<%#Eval("AdminDV.HoTen") %>'></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblDienThoai" Text='<%#Eval("DienThoai") %>'></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblFax" Text='<%#Eval("Fax") %>'></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("Email") %>'></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblDiaChi" Text='<%#Eval("DiaChi") %>'></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblWebsite" Text='<%#Eval("Website") %>'></asp:Label>
                            </li>
                        </ul>
                    </a>
                </ItemTemplate>
            </asp:ListView>
            <div class="phantrang">
                <asp:DataPager ID="dtpLV"
                    PageSize="4"
                    PagedControlID="lvDSDV"
                    OnPreRender="dtpLV_PreRender"
                    runat="server">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ShowFirstPageButton="True" ShowNextPageButton="False" ButtonType="Button" FirstPageText="Trang đầu" LastPageText="Trang cuối" NextPageText="Tiếp" PreviousPageText="Trước" ButtonCssClass="btn btn-success" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" ButtonType="Button" FirstPageText="Trang đầu" LastPageText="Trang cuối" NextPageText="Tiếp" PreviousPageText="Trước" ButtonCssClass="btn btn-success" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server"
                OnClick="btnCreate_Click"
                ID="btnCreate" Text="Create New Account" class="btn btn-info" />
        </div>
    </div>
</asp:Content>

