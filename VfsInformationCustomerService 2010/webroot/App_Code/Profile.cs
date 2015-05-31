using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace Vfs.WebCrawler.Utility
{
    public class Profile
    {
        public class Anonymous : Profile
        {
            public Anonymous()
                : base()
            {
                this.userId = 0;
                this.username = "Anonymous";
                this.firstname = "AnonymousFirstname";
                this.lastname = "AnonymousLastname";
            }
            public override long UserId
            {
                get { return base.UserId; }
                set { }
            }
            public override string Username
            {
                get { return base.Username; }
                set { }
            }
            public override bool IsAuthenticated
            {
                get { return false; }
            }
        }

        private long userId;
        private string lastname;
        private string firstname;
        private string username;
        private string dayXEvent;

        public Profile()
        {
            this.userId = 0;
            this.lastname = string.Empty;
            this.firstname = string.Empty;
            this.username = string.Empty;
        }

        public string DayXEvent
        {
            get { return dayXEvent; }
            set { dayXEvent = value; }
        }

        public virtual long UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public virtual string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public virtual string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public virtual string Username
        {
            get { return username; }
            set { username = value; }
        }
        public virtual bool IsAuthenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }
    }
}