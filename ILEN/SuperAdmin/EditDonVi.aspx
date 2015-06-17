<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SACommon.master" AutoEventWireup="true" CodeFile="EditDonVi.aspx.cs" Inherits="SuperAdmin_EditDonVi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <div class="col-sm-9">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">Enterprise Profile</a></li>
            <li><a data-toggle="tab" href="#menu1">Admin Profile</a></li>
            <li><a data-toggle="tab" href="#menu2">Menu 2</a></li>
            <li><a data-toggle="tab" href="#menu3">Menu 3</a></li>
        </ul>
        <div class="tab-content">
            <div id="menu1" class="tab-pane fade in active">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Họ và Tên:</th>
                                <th>
                                    <asp:Label runat="server" ID="lblHoTenAdmin"></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th>Tên đăng nhập:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblTenDN"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Giới tính:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblGioiTinh"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Email:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblEmailAdmin"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Mobile No:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblDienThoaiAdmin"></asp:Label></td>
                            </tr>

                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" class="sua btn btn-info" Text="QL Admin đơn vị"
                                        OnClick="btnQLAdminDV_Click"
                                        ID="btnQLAdminDVProfile"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="home" class="tab-pane fade">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Doanh Nghiệp:</th>
                                <th>
                                    <asp:Label runat="server" ID="lblTenDV"></asp:Label></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th>Mã Doanh Nghiệp:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblMaDV"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Address:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblDiaChiDV"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Phone No:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblDienThoaiDV"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Fax No:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblFaxDV"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Email:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblEmailDV"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Website:</th>
                                <td>
                                    <a href="javascript:return void(0);">
                                        <asp:Label runat="server" ID="lblWebsite"></asp:Label>
                                    </a></td>

                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenDV"><%#Eval("") %></h2>
    <p class="text-center">Quản Lý Tài Khoản Doanh Nghiệp</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading"><%#Eval("") %> Profile</div>
            <div class="panel-body" id="parentDV" runat="server">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="enterprisename">Enterpise Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtTenDV" placeholder="Please enter enterprise name" />
                    </div>

                    <label class="control-lable col-sm-2" for="enterpriseaddress">Address: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtDC" placeholder="Please enter enterprise address" />
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisephoneno">Phone No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtDT" placeholder="Please enter enterprise phone no" />
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisefaxno">Fax No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtFax" placeholder="Please enter enterprise fax no" />
                    </div>
                    <label class="control-lable col-sm-2" for="enterpriseemail">Email: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtEmail" placeholder="Please enter enterprise email" />
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisewebsite">Website: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtWebsite" placeholder="Please enter enterprise website" />
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisewebsite">Logo: </label>
                    <div class="col-sm-10">
                        <asp:Image runat="server" ID="imgLogo" Width="200" AlternateText="" />
                        <asp:FileUpload runat="server" ID="fulLogo" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" class="them btn btn-info" Text="Save"
                            OnClick="btnSave_Click"
                            ID="btnSave"></asp:Button>
                        <asp:Button runat="server" class="sua btn btn-info" Text="Update"
                            OnClick="btnCapNhat_Click"
                            ID="btnCapNhat"></asp:Button>
                        <asp:Button runat="server" class="sua btn btn-info" Text="QL Admin đơn vị"
                            OnClick="btnQLAdminDV_Click"
                            ID="btnQLAdminDV"></asp:Button>
                        <input type="reset" class="btn btn-info" value="Clear all" />
                        <br />
                        <asp:Label runat="server" ID="lblKQDV"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

