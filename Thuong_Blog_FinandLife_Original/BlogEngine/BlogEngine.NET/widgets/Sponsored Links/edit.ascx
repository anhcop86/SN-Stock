<%@ Control Language="C#" AutoEventWireup="true" CodeFile="edit.ascx.cs" Inherits="widgets_SponsoredLinks_edit" %>
<style type="text/css">
    .style1
    {
        font-family: Verdana;
    }
    .style2
    {
        font-family: Verdana;
        font-size: x-small;
    }
    .style3
    {
        font-size: x-small;
    }
    .style4
    {
        font-family: Verdana;
        font-size: x-small;
        font-weight: bold;
    }
    #previewArea
    {
        height: 174px;
        width: 99%;
    }
</style>
<table>
<tr>
<td><div>
    <label for="<%=cbAuthZAds.ClientID %>">
    <br class="style2" />
    <span class="style2">Don&#39;t show ads for authenticated users</span></label>
    <span class="style4">
    <asp:CheckBox runat="Server" ID="cbAuthZAds" Checked="True" />
    </span><b>
    <br class="style2" />
    <br class="style2" />
    </b>
    <label><span class="style4">Script name</span><b><br class="style2" />
    </b> </label>
    <span class="style1"><span class="style3"><b>
    <asp:TextBox ID="txtName" runat="server" BorderWidth="1px" Width=159px></asp:TextBox>
    </b></span></span><b>
    <br class="style2" />
    <br class="style2" />
        </b>
        <label><span class="style4">Script Tag</span><b><br class="style2" />
    </b> </label>
    <span class="style1"><span class="style3"><b>
    <asp:TextBox ID="txtTag" TextMode="MultiLine" Height=113px Width=327px 
        runat="server" BorderWidth="1px" ></asp:TextBox>
    </b></span></span><b>
    <br class="style2" />
    <br class="style2" />
    </b><span class="style4">Rotation </span>
    <label><span class="style4">weight</span><br />
    </label>
    <asp:TextBox ID="txtWeight" runat="server" BorderWidth="1px" Width=159px></asp:TextBox>
    <br />
    <br />


</div>
</td>
<td>&nbsp;&nbsp; &nbsp;</td>
<td>
<label><span class="style4">Ad Preview window</span></label><hr /><br />
    <div id=previewArea runat="server">
    </div>
</td>
</tr>
</table>

<asp:Button ID="btnPrev" runat="server" BorderStyle="Solid" BorderWidth="1px" 
    Text="&lt;&lt;" onclick="btnPrev_Click" />
&nbsp;<asp:Button ID="btnNext" runat="server" BorderStyle="Solid" 
    BorderWidth="1px" Text="&gt;&gt;" onclick="BtnNext_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnAdd" runat="server" BorderStyle="Solid" BorderWidth="1px" 
    Text="Add" onclick="btnAdd_Click" />
&nbsp;<asp:Button ID="btnDelete" runat="server" BorderStyle="Solid" 
    BorderWidth="1px" Text="Delete" onclick="btnDelete_Click" />


