using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class DatePickerExtension
    {
        public static string DatePicker(this HtmlHelper htmlHelper, string name)
        {
            return "<input type=\"text\" id=\"" + name +
            "\" name=\"" + name + "\" value=\"\"/>";
        }
    }   
}