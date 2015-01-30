using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;
using System.IO;

namespace ShareHoderFrontEndV2.Ext.Utility
{
    public class ChangePasswordEmail : SendEmail
    {
        protected Int32 propertyID = 0;
        protected string message = string.Empty;
		protected string password = string.Empty;
		protected string userName = string.Empty;

        public Int32 PropertyId
        {
            set
            {
                this.propertyID = value;
            }
            get
            {
                return this.propertyID;
            }

        }

        public string Message
        {
            set
            {
                this.message = value;
            }
            get
            {
                return this.message;
            }
        }

		public string Password
		{
			set
			{
				this.password = value;
			}
			get
			{
				return this.password;
			}
		}

		public string UserName
		{
			set
			{
				this.userName = value;
			}
			get
			{
				return this.userName;
			}
		}

        protected override void GetParamaters()
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            if (useContentTemplate == true)
            {
                reader = new StreamReader(this.fileName, System.Text.UTF8Encoding.UTF8);
                this.bodyText = reader.ReadToEnd();
                reader.Close();

                parameters.Add("UserName", this.userName);
				parameters.Add("Password", this.password);
                

                for (int i = 0; i < parameters.Keys.Count; i++)
                {
                    string replaceKey = "${" + parameters.GetKey(i) + "}$";
                    this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                }
            }
        }
    }
}
