<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (Request.IsAuthenticated) { %>
    <p>
        Hello, <%: Html.ActionLink(Page.User.Identity.Name, "ChangePassword", "Account", null, new { @class = "username" }) %>!
        <%: Html.ActionLink("Log Off", "LogOff", "Account") %>
    </p>
<% } else { %>
    <ul>
        <li><%: Html.ActionLink("Register", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink", data_dialog_title = "Registration" })%></li>
        <li><%: Html.ActionLink("Log On", "LogOn", "Account", routeValues: null, htmlAttributes: new { id = "logonLink", data_dialog_title = "Identification" })%></li>
    </ul>
<% } %>