<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ShareHolderDataService.Models.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password - My ASP.NET MVC Application
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Change Password.</h1>
        <h2>Use the form below to change your password.</h2>
    </hgroup>

    <p class="message-info">
        New passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length.
    </p>

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <%: Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) { %>
        <fieldset>
            <legend>Change Password Form</legend>
            <ol>
                <li>
                    <%: Html.LabelFor(m => m.OldPassword) %>
                    <%: Html.PasswordFor(m => m.OldPassword) %>
                    <%: Html.ValidationMessageFor(m => m.OldPassword) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.NewPassword) %>
                    <%: Html.PasswordFor(m => m.NewPassword) %>
                    <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                </li>
                <li>
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </li>
            </ol>
            <input type="submit" value="Change Password" title="Change password" />
        </fieldset>
    <% } %>
</asp:Content>
