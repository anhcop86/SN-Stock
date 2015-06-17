<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SACommon.master" AutoEventWireup="true" CodeFile="QLAdminDV.aspx.cs" Inherits="SuperAdmin_QLAdminDV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="divParent" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading" runat="server" id="divProfile"></div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2"
                        for="adminfullname">
                        Đơn vị:
                    </label>
                    <asp:Label runat="server" ID="lblDonVi" class="form-control col-sm-6"></asp:Label>
                    
                    <label class="control-lable col-sm-2" for="adminfullname">Full Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtTenAdmin" placeholder="Please enter admin name" />
                    </div>
                    <label class="control-lable col-sm-2" for="adminid">Username</label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtTenDN" placeholder="Please enter username" />
                    </div>
                    <label class="control-lable col-sm-2" for="adminsex">Sex: </label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList runat="server" ID="rblGT" class="radio"></asp:RadioButtonList>
                    </div>
                    <label class="control-lable col-sm-2" for="adminemail">Email: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtAdEmail" name="adminemail" placeholder="Please enter admin email" />
                    </div>
                    <label class="control-lable col-sm-2" for="adminmobileno">Mobile No: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtAdDienThoai" placeholder="Please enter admin mobile no" />
                    </div>

                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" class="btn btn-info sua" Text="Update"
                            OnClick="btnCapNhatAdmin_Click"
                            ID="btnCapNhatAdmin"></asp:Button>
                        <input type="reset" value="Clear all" class="btn btn-info sua" />
                        <br />
                        <asp:Label runat="server" ID="lblKQAdmin"></asp:Label>
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
                        <asp:RadioButtonList runat="server" ID="rblTT" class="radio"></asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="numberofusers">Number of Users: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            class="form-control" ID="txtSoLuongAccount" placeholder="Please enter number of users" />
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" class="btn btn-info" Text="Update"
                            ID="btnUpdateAcc" OnClick="btnUpdateAcc_Click"></asp:Button>
                        <br />
                        <asp:Label runat="server" ID="lblKQStatus"></asp:Label>
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
                        <asp:TextBox runat="server" class="form-control" ID="txtPass" TextMode="Password" />
                    </div>
                    <label class="control-lable col-sm-2" for="retypenewpassword">Retype New Password: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtRePass" TextMode="Password" />
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" class="btn btn-info" Text="Update"
                            ID="btnUpdatePass" OnClick="btnUpdatePass_Click"></asp:Button>
                        <br />
                        <asp:Label runat="server" ID="lblMatKhau"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button runat="server" ID="btnXoaAdmin"
            OnClick="btnXoaAdmin_Click"
            class="btn btn-danger" Text="Delete account"></asp:Button>
        <br />
        <asp:Label runat="server" ID="lblXoaAdmin"></asp:Label>
    </div>
</asp:Content>

