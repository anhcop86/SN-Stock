using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace SnapShot
{
    public class ApplicationHelper
    {
        private ApplicationHelper()
        {
        }
        public static string NameSheet
        {
            get
            {
                return ConfigurationManager.AppSettings["NameSheet"].ToString();
            }
        }
        public static string GetSourceFolerHOSE
        {
            get
            {
                return ConfigurationManager.AppSettings["GetSourceFolerHOSE"];
            }
        }
        public static string GetTargetFolerHOSE
        {
            get
            {
                return ConfigurationManager.AppSettings["GetTargetFolerHOSE"];
            }
        }
        public static string GetSourceFolerHNX
        {
            get
            {
                return ConfigurationManager.AppSettings["GetSourceFolerHNX"];
            }
        }
        public static string GetTargetFolerHNX
        {
            get
            {
                return ConfigurationManager.AppSettings["GetTargetFolerHNX"];
            }
        }
        public static void CopyDirectory(string Soure, string target)
        {
            DirectoryInfo sourceDI = new DirectoryInfo(Soure);
            DirectoryInfo targetDI = new DirectoryInfo(target);
            if (!Directory.Exists(targetDI.FullName))
            {
                Directory.CreateDirectory(target);
            }
            foreach (FileInfo fi in sourceDI.GetFiles())
            {
                fi.CopyTo(target.ToString() + "\\" + fi.Name, true);
                Console.WriteLine("Da copy {0}", fi.Name);
            }
        }
    }
}
