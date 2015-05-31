<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSBank.aspx.cs" Inherits="SMSBank" Title="Dịch vụ nhắn tin chuyển khoản" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="button"  id="sendButton" runat="server"  value="Send" onserverclick="Send_Click" style="width: 129px" />            
        <br />
        Số điện thoại:
    <asp:TextBox id="receiverTextBox" runat="server" /><br />
        Nội dung tin nhắn<br />
        &nbsp;<asp:TextBox ID="ContentTextBox" runat="server" Height="110px" TextMode="MultiLine" Width="507px"></asp:TextBox>
    </div>
    <div><label id="statusLabel" runat="server"></label></div>    
    </form>
</body>
</html>
