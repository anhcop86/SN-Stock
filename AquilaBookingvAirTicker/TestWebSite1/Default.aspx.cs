using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        test.Load += new EventHandler(test_Load);
        System.Net.WebClient wc = new System.Net.WebClient();
        Byte[] aa = wc.DownloadData("https://abacuswebstart.abacus.com.sg/hnh/flight-search.aspx");
        String s = wc.DownloadString("https://abacuswebstart.abacus.com.sg/hnh/flight-search.aspx");
        //this.hnh.InnerHtml = s
       String test1 = this.test.Attributes["src"];
        
    }

    protected void test_Load(object sender, EventArgs e)
    {
       
    }
}
