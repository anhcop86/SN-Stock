using System;
using System.Collections.Generic;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class UserBase
    {
        private string userId = string.Empty;

        public string UserId
        {
            set { value = this.userId; }
            get { return UserId; }
        }

        private string userName;

        public string UserName
        {
            set { value = this.userName; }
            get { return userName; }
        }
        
        private string email;

        public string Email
        {
            set { value = this.email; }
            get { return email; }
        }

        public UserBase() { }

        public UserBase( string userId,string userName, string email )
        {
            userId = this.userId;
            userName = this.userName;
            email = this.email;
        }
    }
}
