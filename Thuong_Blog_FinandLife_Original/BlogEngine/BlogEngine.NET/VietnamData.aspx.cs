using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Collections.Generic;
using BlogEngine.Core;
using System.Web.UI;
public partial class VietnameData : BlogEngine.Core.Web.Controls.BlogBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Month.aspx");
    }

   
}