using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication4.Areas;
using PorfolioInvesment.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication4.Models
{
    public class Authentication
    {
        public Authentication()
        {


        }

        public static bool AuthenApi(string username, string password)
        {
            string MD5Password = ApplicationHelper.GetMD5Hash(password);
            string MD5PasswordConfig = ApplicationHelper.GetMD5Hash(ApplicationHelper.Password);

            if (ApplicationHelper.Username == username && MD5Password == MD5PasswordConfig)
            {
                return true;
            }
                
            else
            {
                return false;
            }
        }
    }

    public class UserAuthen
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}