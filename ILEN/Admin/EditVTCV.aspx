<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditVTCV.aspx.cs" Inherits="Admin_EditVTCV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Edit vị trí công việc
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenVTCV"></h2>
    <p class="text-center">Quản Lý Chức Danh Vị Trí Công Việc</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Job Position Details</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="jobposition">Tên vị trí CV: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenVT" placeholder="Please enter job position"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="jobposition">Job Position: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenVTEN" placeholder="Please enter job position"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="jobposition">Divsion: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlBP" runat="server"></asp:DropDownList>
                    </div>
                    <label class="control-lable col-sm-2" for="status">Status: </label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList ID="rblTT" runat="server"></asp:RadioButtonList>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnLuu"
                            OnClick="btnLuu_Click"
                            class="btn btn-info" Text="Save"></asp:Button>
                        <input type="reset" class="btn btn-info" value="Clear all" />
                        <asp:Button runat="server" ID="btnDelete"
                            OnClick="btnDelete_Click"
                            class="btn btn-danger" Text="Delete Job Position"></asp:Button>
                        <br />
                        <asp:Label runat="server" ID="lblTB"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

