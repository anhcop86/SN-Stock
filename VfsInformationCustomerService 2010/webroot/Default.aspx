<%@ Page Language="C#" AutoEventWireup="true" ClassName="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="Stylesheet" href="_assets/css/accordion.css" />
    <style>
        body
        {
           padding:0px;
           font-size:10pt;
           font-family:Tahoma;           
           z-index: 0px;
        }  
        .demo
        {
            padding:1px; 
            border:solid 1px #333333; 
            width:200px;
        }        
    </style>
</head>
<body>
    <form runat="server">
        <div id="demo">
            <asp:ScriptManager runat="server" />   
            <asp:ObjectDataSource ID="odsMailItems" runat="server" SelectMethod="FetchMailItems" TypeName="MailBoxItems" />
            <asp:ObjectDataSource ID="odsNoteItems" runat="server" SelectMethod="FetchNoteItems" TypeName="MailBoxItems" />
            <asp:ObjectDataSource ID="odsContactItems" runat="server" SelectMethod="FetchContactItems" TypeName="MailBoxItems" />                        
            <br />
            <div class="demo">
                <ajaxToolkit:Accordion 
                    ID="accordion" runat="server" 
                    FadeTransitions="false" FramesPerSecond="100" TransitionDuration="250" 
                    CssClass="accordion" HeaderCssClass="header" ContentCssClass="content" 
                    RequireOpenedPane="True" AutoSize="None">
                    <Panes>
                        <ajaxToolkit:AccordionPane ID="mail" runat="server">
                            <Header>
                                <div style="background-image:url(_assets/img/mail_large.gif)">
                                    <span>Hose</span>
                                </div>
                            </Header>
                            <Content>
                                <asp:ListView ID="lvMailItems" runat="server" DataSourceID="odsMailItems">
                                    <LayoutTemplate>
                                        <ul>
                                            <li id="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li style='background-image:url(<%# Eval("ImageUrl") %>)'><%# Eval("Name") %></li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="notes" runat="server">
                            <Header>
                                <div style="background-image:url(_assets/img/notes_large.gif)">
                                    <span>Notes</span>
                                </div>
                            </Header>
                            <Content>
                                <asp:ListView ID="lvNoteItems" runat="server" DataSourceID="odsNoteItems">
                                    <LayoutTemplate>
                                        <ul>
                                            <li id="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li style='background-image:url(<%# Eval("ImageUrl") %>)' title='<%# Eval("Name")%>'><%# Eval("Name") %></li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </Content>
                        </ajaxToolkit:AccordionPane> 
                        <ajaxToolkit:AccordionPane ID="contacts" runat="server">
                            <Header>
                                <div style="background-image:url(_assets/img/contact_large.gif)">
                                    <span>Contacts</span>
                                </div>
                            </Header>
                            <Content>
                                <asp:ListView ID="lvContactItems" runat="server" DataSourceID="odsContactItems">
                                    <LayoutTemplate>
                                        <ul>
                                            <li id="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li style='background-image:url(<%# Eval("ImageUrl") %>)' title='<%# Eval("Name")%>'><%# Eval("Name") %></li>
                                    </ItemTemplate>
                                </asp:ListView>                        
                            </Content>
                        </ajaxToolkit:AccordionPane>  
                    </Panes>
                </ajaxToolkit:Accordion>   
            </div>
        </div>
    </form>
</body>
</html>
