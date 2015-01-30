using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShareHoderFrontEndV2.Ext.Utility;
using System.Collections.Specialized;
using System.IO;

namespace ShareHoderFrontEndV2.Ext
{
    public class BookingEmail : ChangePasswordEmail 
    {
        public string BookingCode { get; set; }
        /* protected override void GetParamaters()
         {
             NameValueCollection parameters = new NameValueCollection();
             StreamReader reader = null;

             if (useContentTemplate == true)
             {
                 reader = new StreamReader(this.fileName, System.Text.UTF8Encoding.UTF8);
                 this.bodyText = reader.ReadToEnd();
                 reader.Close();

                 parameters.Add("BookingCode", this.BookingCode);
                 //parameters.Add("Password", this.password);


                 for (int i = 0; i < parameters.Keys.Count; i++)
                 {
                     string replaceKey = "${" + parameters.GetKey(i) + "}$";
                     this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                 }
             } 
         }*/
    }
}