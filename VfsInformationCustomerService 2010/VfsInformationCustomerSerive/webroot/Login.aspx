<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%=Resources.UIResource.Login %></title>
    <link type="text/css" rel="Stylesheet" href="_assets/css/general.css" />
    <link type="text/css" rel="Stylesheet" href="_assets/css/accordion.css" />
    <link type="text/css" rel="Stylesheet" href="_assets/css/MessageContent.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="login">
        <div>
            <label for="<%= userNameTextBox.ClientID %>"><%=Resources.UIResource.UserName %><span class="inforError"> (*)</span></label>
        </div>
        <div>
            <asp:TextBox ID="userNameTextBox" runat="server" MaxLength="30"></asp:TextBox>
        </div>
        <div>
            <label for="<%= passwordTextBox.ClientID %>"><%=Resources.UIResource.Password %><span class="inforError"> (*)</span></label>
        </div>
        <div>
            <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="loginButton" runat="server" OnClick="loginButton_Click"></asp:Button>
        </div>
        <div>
            <asp:Literal ID="loginMessage" runat="server" ></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
