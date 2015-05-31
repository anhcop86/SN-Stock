<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unsubcribe.aspx.cs" Inherits="Public_Unsubcribe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Resources.UIResource.InsubcribeTitle%></title>
</head>
<body> 
<form id="CheckUnsubcribeForm" runat="server">
    <div>
        <asp:Label runat="server" ID="CheckUnsubcribeLabel"><%=Resources.UIResource.CheckUnsubcribe%> <br/>          
          <asp:Button runat="server" ID="UnsubcribeButton" Text="Từ chối" OnClick="UnsubcribeButton_onclick" />
        </asp:Label>
    </div>
    <div>
    <asp:Label runat="server" Visible="false" ID="UnsubcribeLabel"> <%=Resources.UIResource.Unsubcribe1%><br/>
        <%=Resources.UIResource.Unsubcribe2%><a href="http://www.vfs.com.vn/Contact.aspx" target="_blank" >liên hệ</a> chúng tôi.
    </asp:Label>
    </div>
    </form>       
</body>
</html>
