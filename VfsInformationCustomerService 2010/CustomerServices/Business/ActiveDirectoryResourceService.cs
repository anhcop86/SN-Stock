using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;

namespace VfsCustomerService.Business
{
    public class ActiveDirectoryResourceService
    {
        public User AuthenticateUser(string username, string password)
        {
            try
            {
                DirectoryEntry deSystem = new DirectoryEntry();
                deSystem.AuthenticationType = AuthenticationTypes.Secure;
                deSystem.Username = username;
                deSystem.Password = password;

                // Bind to the native AdsObject to force authentication.
                Object obj = deSystem.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(deSystem);
                ds.Filter = "(SAMAccountName=" + username + ")";
                ds.PropertiesToLoad.Add("name");
                ds.PropertiesToLoad.Add("mail");

                SearchResult sr = ds.FindOne();
                if (sr == null)
                {
                    return null;
                }

                DirectoryEntry de = sr.GetDirectoryEntry();
                User user = new User();
                user.UserId = username;
                user.UserName = de.Properties["name"].Value.ToString();
                user.Email = de.Properties["mail"].Value.ToString();
                return user;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }
        }
    }
}
