using System;
using System.Collections.Generic;
using System.Text;

namespace VfsCustomerService.Entities
{
    [Serializable]
    public class UserBase
    {
        private string userId;

        public string UserId
        {
            set { value = this.UserId; }
            get { return userId; }
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

        public UserBase(string userId, string userName, string email)
        {
            this.userId = userId;
            this.userName = userName;
            this.email = email;
        }
    }
}
