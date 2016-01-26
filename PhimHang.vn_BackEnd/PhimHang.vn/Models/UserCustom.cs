using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhimHang.Models
{
    public class UserCustom : IdentityUser
    {
    }
    public static class InMemoryUserContext
    {
        public static List<UserCustom> DummyUsersList { get; set; }
        static InMemoryUserContext()
        {
            DummyUsersList = new List<UserCustom>();
        }
        public static bool Add(UserCustom user)
        {
            DummyUsersList.Add(user);
            return true;
        }
    }
    public class CustomUserStore : IUserStore<UserCustom>, IUserPasswordStore<UserCustom>
    {
        public Task<UserCustom> FindByNameAsync(string userName)
        {
            UserCustom user = InMemoryUserContext.DummyUsersList.Find(item => item.UserName == userName);
            return Task.FromResult<UserCustom>(user);
        }
        public Task CreateAsync(UserCustom user)
        {
            return Task.FromResult<bool>(InMemoryUserContext.Add(user));
        }
        public Task<string> GetPasswordHashAsync(UserCustom user)
        {
            return Task.FromResult<string>(user.PasswordHash.ToString());
        }
        public Task SetPasswordHashAsync(UserCustom user, string passwordHash)
        {
            return Task.FromResult<string>(user.PasswordHash = passwordHash);
        }
        #region Not implemented methods
        public Task DeleteAsync(UserCustom user)
        {
            throw new NotImplementedException();
        }
        public Task<UserCustom> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(UserCustom user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> HasPasswordAsync(UserCustom user)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}