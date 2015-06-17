<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditNhanVien.aspx.cs" Inherits="Admin_EditNhanVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Cập nhật tài khoản nhân viên</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <div class="row">
        <div class="col-sm-9">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home">Profile</a></li>
                <li><a data-toggle="tab" href="#menu1">More Information</a></li>
            </ul>
            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <div class="table-responsive">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th>Họ và Tên:</th>
                                    <th>
                                        <asp:Label runat="server" ID="lblHoTen"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th>Mã Nhân Viên:</th>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaNV"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <th>Chức Danh Vị Trí Công Việc:</th>
                                    <td>
                                        <asp:Label runat="server" ID="lblVTCV"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <th>Bộ Phận Trực Thuộc:</th>
                                    <td>
                                        <asp:Label runat="server" ID="lblTenBP"></asp:Label>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <div class="table-responsive">
                        <table class="table table-hover">
                            <tbody>
                                <tr>
                                    <th>Sex:</th>
                                    <td>
                                        <asp:Label runat="server" ID="lblGioiTinh"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <th>Date of Birth:</th>
                                    <td>
                                        <asp:Label runat="server" ID="lblNgaySinh"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenNV"></h2>
    <p class="text-center">Quản Lý Tài Khoản User</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">User Profile</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="employeeid">Employee ID: </label>

                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            Enabled="false"
                            class="form-control" ID="txtMaNV" name="employeeid" placeholder="Please enter employee id" />
                    </div>
                    <label class="control-lable col-sm-2" for="fullname">Full Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtHoTen" placeholder="Please enter user full name" />
                    </div>

                    <label class="control-lable col-sm-2" for="department">Division / Department: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList AutoPostBack="true"
                            class="form-control"
                            OnSelectedIndexChanged="ddlBP_SelectedIndexChanged"
                            runat="server" ID="ddlBP">
                        </asp:DropDownList>
                        <br />
                    </div>
                    <label class="control-lable col-sm-2" for="jobposition">Job Title / Position: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList
                            class="form-control"
                            runat="server" ID="ddlVTCV">
                        </asp:DropDownList>
                    </div>
                    <label class="control-lable col-sm-2" for="usersex">Sex: </label>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:RadioButtonList runat="server" ID="rblGT" class="radio"></asp:RadioButtonList>
                    </div>

                    <label class="control-lable col-sm-2">Date of Birth: </label>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:TextBox runat="server" ID="txtNgaySinh" class="form-control"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="mobileno">Mobile No: </label>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:TextBox runat="server" ID="txtDT" class="form-control"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="useremail">Email: </label>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:TextBox runat="server" ID="txtEmail" class="form-control"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="status">Status: </label>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:RadioButtonList runat="server" ID="rblTT" class="radio"></asp:RadioButtonList>
                    </div>
                    <label class="control-lable col-sm-2" for="status">Hình: </label>
                    <asp:Image runat="server" ID="imgNV" AlternateText="no image" Width="100" />
                    <div class="col-sm-10">
                        &nbsp
                        <asp:FileUpload runat="server" ID="fulProfile" class="btn btn-info" />
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnSaveProfile"
                            OnClick="ddlBP_SelectedIndexChanged" Text="Save" class="btn btn-info" />
                        <input type="reset" value="Clear all" class="btn btn-info" />
                        <br />
                        <asp:Label runat="server" ID="lblThongBaoProfile"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">Reset Password</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="newpassword">New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            TextMode="Password"
                            class="form-control" ID="txtPass" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="retypenewpassword">Retype New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            TextMode="Password"
                            class="form-control" ID="txtRetype" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnLuuPass"
                            OnClick="btnLuuPass_Click"
                            Text="Save" class="btn btn-info" />
                        <input type="reset" value="Clear all" class="btn btn-info" />
                        <br />
                        <asp:Label runat="server" ID="lblTBPass"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate"
                OnClick="btnCreate_Click"
                Text="Create New Account" class="btn btn-info" />
        </div>
    </div>
</asp:Content>

