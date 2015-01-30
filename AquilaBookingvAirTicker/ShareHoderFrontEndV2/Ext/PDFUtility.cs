using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;



namespace ShareHoderFrontEndV2.Ext
{
    public static class PDFUtility
    {
        [STAThread]
        public static void getPDFFromHTML()
        {
            //PdfDocument doc = new PdfDocument();
            //String url = "Http://apple.com/";
            //doc.LoadFromHTML(url, false, true, true);
            //doc.SaveToFile("webpageaspdf.pdf");
            //doc.Close();
            //System.Diagnostics.Process.Start("webpageaspdf.pdf");
        }

        public static string getContentXML(string drectoryxml)
        {
            /*XmlTextReader xmlReader = new XmlTextReader(drectoryxml);
            XmlDocument document = new XmlDocument();
            FileStream fs; fs = File.OpenRead(drectoryxml);
            document.Load(fs);
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            document.WriteTo(tx);

            string str = sw.ToString();// 
            return str;
            */
            StreamReader reader = null;
            reader = new StreamReader(drectoryxml, System.Text.UTF8Encoding.UTF8);
            string str = reader.ReadToEnd();

            return str;
        }

        //public static string convertXMLToWordDocument(string drectoryxml)
        //{
        //    Document document = new Document();
        //    document.LoadFromFile(drectoryxml);

        //    document.SaveToFile("word.doc", FileFormat.Doc);
        //}

    }
}