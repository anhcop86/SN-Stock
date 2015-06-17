using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OptionGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ListItem item1 = new ListItem("A", "1");
            item1.Attributes["OptionGroup"] = "Alphabet";

            ListItem item2 = new ListItem("B", "2");
            item2.Attributes["OptionGroup"] = "Alphabet";

            ListItem item3 = new ListItem("One", "3");
            item3.Attributes["OptionGroup"] = "Numbers";

            ListItem item4 = new ListItem("Two", "4");
            item4.Attributes["OptionGroup"] = "Numbers";

            ddlTest.Items.Add(item1);
            ddlTest.Items.Add(item2);
            ddlTest.Items.Add(item3);
            ddlTest.Items.Add(item4);
        }
    }
}