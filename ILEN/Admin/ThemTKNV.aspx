<%@ Page Title="Thêm tài khoản nhân viên" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="ThemTKNV.aspx.cs" Inherits="Admin_ThemTKNV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Thêm tài khoản nhân viên</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenNV"></h2>
    <p class="text-center">Thêm Tài Khoản Nhân Viên</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Employee's Profile</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2">Choose role for Employee:</label>
                    <div class="col-sm-12">
                        <asp:DropDownList runat="server"
                            class="form-control" ID="ddlVaiTro" name="employeeid" />
                    </div>
                    <label class="control-lable col-sm-2">Chuyên môn:</label>
                    <div class="col-sm-12">
                        <asp:DropDownList runat="server"
                            class="form-control" ID="ddlChuyenMon" />
                    </div>
                    <label class="control-lable col-sm-2">Employee ID: </label>
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
                    <div class="col-sm-10">
                        <asp:Image runat="server" ID="imgNV" AlternateText="no image" Width="100" />
                    </div>
                    <div class="col-sm-10">
                        &nbsp
                        <asp:FileUpload runat="server" ID="fulProfile" class="btn btn-info" />
                    </div>

                    <label class="control-lable col-sm-2" for="newpassword">New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            TextMode="Password"
                            class="form-control" ID="txtPass" />
                    </div>
                    <label class="control-lable col-sm-2" for="retypenewpassword">Retype New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            TextMode="Password"
                            class="form-control" ID="txtRetype" />
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnCreate"
                            OnClick="btnCreate_Click"
                            Text="Create" class="btn btn-info" />
                        <input type="reset" value="Clear all" class="btn btn-info" />
                        <br />
                        <asp:Label runat="server" ID="lblThongBao"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

