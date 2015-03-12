using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace PhimHang.Models
{
    public static class AppHelper
    {
        
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
        public static string GetDomain(string domainFull)
        {
            return new System.Uri(domainFull).Host;
        }
    }
    public class PosistionFilter
    {
        public int Index { get; set; }
        public int Posistion { get; set; }
    }

    
}