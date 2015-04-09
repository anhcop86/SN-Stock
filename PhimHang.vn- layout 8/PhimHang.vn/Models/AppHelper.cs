﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace PhimHang.Models
{
    public static class AppHelper
    {
        private static testEntities db = new testEntities();
        
        public static string FilteringWord(string messege)
        {
            //get list keyword
            FilterKeyworkSingleton instance = FilterKeyworkSingleton.Instance;
            List<PosistionFilter> listPosistionFilter = new List<PosistionFilter>();
            var stringOriginal = new StringBuilder(messege);
            string messegeNonUnicode = ConvertToNonUnicode(messege).ToLower();
            var keywordList = instance.getListKeyWord();
            foreach (var item in keywordList)
            {
                int index = messegeNonUnicode.IndexOf(item, 0);
                while (index != -1)
                {
                    listPosistionFilter.Add(new PosistionFilter { Index = index, Posistion = item.Length });
                    index = messegeNonUnicode.IndexOf(item, index + item.Length);
                }
            }
            foreach (var item in listPosistionFilter)
            {
                stringOriginal.Remove(item.Index, item.Posistion);
                stringOriginal.Insert(item.Index, "*", item.Posistion);
            }

            return  stringOriginal.ToString();
        }
        public static string ConvertToNonUnicode(string str)
        {
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
            
        }
        private static readonly string[] VietNamChar = new string[]
        {
             "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũứừựửữư",
            "ÚÙỤỦŨỨỪỰỬỮƯ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string AbsolutePathHostName
        {
            get
            {
                return ConfigurationManager.AppSettings["AbsolutePathHostName"].ToString();
            } 
        }
        public static string TinyURL
        {
            get
            {
                return ConfigurationManager.AppSettings["TinyURL"].ToString();
            }
        }

        public static string GetDomain(string domainFull)
        {
            return new System.Uri(domainFull).Host;
        }

        public static async Task<List<string>> GetListHotStock()
        {
            using (db = new testEntities())
            {
                return await (from hoststock in db.TickerHots // danh sach co phieu nong trong db
                                       select hoststock.THName).ToListAsync();                
            }
            
        }
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static string Encode(string password)
        {
            return Crypto.HashPassword(password);
        }
        public static bool CheckTimeUpdatePrice()
        {
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 15 && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool sendEmail(string username, string fromEmail,string fromEmailPass, String toEmail , string fileTemplateName)
        {
            MailMessage message = new MailMessage();
            MailAddress sender = new MailAddress(fromEmail);
            MailAddress receiver = new MailAddress(toEmail);
            message.From = sender;
            message.Sender = sender;
            message.To.Add(receiver);
            message.Subject = "Đăng ký thành công | cungphim.com";
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = GetContentTemplate(fileTemplateName);
            message.Priority = MailPriority.High;

            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 25;
            smtpClient.Host = "mail.vfs.com.vn";
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = false;

            smtpClient.Credentials = new NetworkCredential(fromEmail, fromEmailPass);

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        private static string GetContentTemplate(string fileName)
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            reader = new StreamReader(fileName, System.Text.UTF8Encoding.UTF8);
            string bodyText = reader.ReadToEnd();
            reader.Close();

            parameters.Add("Password", "123456789");
            parameters.Add("UserName", "ToiLaAi");

            for (int i = 0; i < parameters.Keys.Count; i++)
            {
                string replaceKey = "${" + parameters.GetKey(i) + "}$";
                bodyText = bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
            }
            return bodyText;

        }
        public static string ResetPasswordEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["ResetPasswordEmailTemplatePath"];
            }
        }
    }
    public class PosistionFilter
    {
        public int Index { get; set; }
        public int Posistion { get; set; }
    }



    
}