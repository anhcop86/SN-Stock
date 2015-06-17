<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditTaiKhoan.aspx.cs" Inherits="Admin_EditTaiKhoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center">ABC Bakery</h2>
    <p class="text-center">Quản Lý Tài Khoản Doanh Nghiệp</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">ABC Bakery Profile</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="enterprisename">Enterpise Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox
                            runat="server"
                            class="form-control" ID="txtName" placeholder="Please enter enterprise name"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterpriseid">Enterprise ID: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtID" placeholder="Please enter enterprise id"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterpriseaddress">Address: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAddress" placeholder="Please enter enterprise address"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisephoneno">Phone No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="enterprisephoneno" placeholder="Please enter enterprise phone no"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisefaxno">Fax No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtFax" placeholder="Please enter enterprise fax no"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterpriseemail">Email: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtEmail" placeholder="Please enter enterprise email"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisewebsite">Website: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtWeb" placeholder="Please enter enterprise website"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="enterprisewebsite">Logo: </label>
                    <div class="col-sm-10">
                        <asp:FileUpload runat="server" ID="fulLogo"></asp:FileUpload>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuu" class="btn btn-info" Text="Save"></asp:Button>
                        <asp:Button runat="server" ID="btnXoa" class="btn btn-info" Text="Clear All"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Kevin's Profile</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="adminfullname">Full Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminName" placeholder="Please enter admin name"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="adminid">Admin ID: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminID" placeholder="Please enter admin id"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="adminsex">Sex: </label>
                    <div class="col-sm-10">
                        <div class="radio">
                            <asp:RadioButtonList ID="rblGioiTinh" runat="server"></asp:RadioButtonList>
                        </div>
                        <br />
                    </div>
                    <label class="control-lable col-sm-2" for="adminemail">Email: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminEmail" placeholder="Please enter admin email"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="adminmobileno">Mobile No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminPhone" placeholder="Please enter admin mobile no"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="adminjobposition">Job Title / Position: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminJob" placeholder="Please enter admin job position"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="admindepartment">Division / Department: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdminDep" placeholder="Please enter admin department"></asp:TextBox>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuAdmin" class="btn btn-info" Text="Save"></asp:Button>
                        <asp:Button runat="server" ID="btnXoaAdmin" class="btn btn-info" Text="Clear All"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Account Status</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="status">Status: </label>
                    <div class="col-sm-10">
                        <div class="radio">
                            <asp:RadioButtonList ID="rblTinhTrangAcc" runat="server"></asp:RadioButtonList>
                        </div>
                        <br />
                    </div>
                    <label class="control-lable col-sm-2" for="numberofusers">Number of Users: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtNumberofUser" placeholder="Please enter number of users"></asp:TextBox>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuTinhTrangAcc" class="btn btn-info" Text="Save"></asp:Button>
                        <asp:Button runat="server" ID="btnXoaTinhTrangAcc" class="btn btn-info" Text="Clear All"></asp:Button>
                        <asp:Button runat="server" ID="btnDeleteTinhTrangAcc" class="btn btn-danger" Text="Delete Account"></asp:Button>
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
                        <asp:TextBox runat="server" class="form-control" ID="txtNewPass" placeholder="Please enter new admin password"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="retypenewpassword">Retype New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtConfirmPass" placeholder="Please reenter new admin password"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuPassword" class="btn btn-info" Text="Save"></asp:Button>
                        <asp:Button runat="server" ID="btnXoaPassword" class="btn btn-info" Text="Clear All"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

