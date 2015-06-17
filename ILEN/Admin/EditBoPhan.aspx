<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditBoPhan.aspx.cs" Inherits="Admin_EditBoPhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenBP"></h2>
    <p class="text-center">Quản Lý Bộ Phận Trực Thuộc</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Division Details</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="divisionname">Tên bộ phận: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenBP" placeholder="Please enter division name" />
                    </div>
                    <label class="control-lable col-sm-2" for="divisionname">Division Name: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenBPEN" placeholder="Please enter division name" />
                    </div>
                    <label class="control-lable col-sm-2" for="status">Status: </label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList runat="server" ID="rblTrangThai" class="radio"></asp:RadioButtonList>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuBP"
                            OnClick="btnLuuBP_Click"
                            Text="Save" class="btn btn-info"></asp:Button>
                        <input type="reset" class="btn btn-info" value="Clear all" />
                        <asp:Button runat="server" ID="btnXoaTT"
                            OnClick="btnXoaTT_Click"
                            Text="Delete" class="btn btn-danger"></asp:Button>
                        <br />
                        <asp:Label runat="server"
                            ID="lblThongBao"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

