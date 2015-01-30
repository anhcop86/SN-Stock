<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    function getContentFromIframe(iFrameName) {

        var myIFrame = document.getElementById(iFrameName);
        var content = myIFrame.contentWindow.document.body.innerHTML;

        alert('content: ' + content);

        content = 'The inside of my frame has now changed';
        test.contentWindow.document.body.innerHTML = content;

    }
 
</script>
<a href="#" onclick="getContentFromIframe('MainContent_test')">Get the content</a>
   <iframe src="https://abacuswebstart.abacus.com.sg/hnh/flight-search.aspx" width="100%" height="100%" runat="server" id="test" on ></iframe>
</asp:Content>
