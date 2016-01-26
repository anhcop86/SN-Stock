using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PhimHang.Models
{
    public static class AppHelper
    {
        public static string MD5Hash(string passwordInput)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(passwordInput);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X1"));
            }
            return sb.ToString();
        }

        public static string UserNameAdmin
        {
            get
            {
                return ConfigurationManager.AppSettings["UserNameAdmin"].ToString();
            }
        }

        public static string PasswordAdmin
        {
            get
            {
                return ConfigurationManager.AppSettings["PasswordAdmin"].ToString();
            }
        }
    }
}