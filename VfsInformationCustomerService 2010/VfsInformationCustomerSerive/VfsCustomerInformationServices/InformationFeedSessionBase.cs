using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsCustomerInformationServices
{
    public abstract class InformationFeedSessionBase
    {
        private List<Exception> listError;
        private int numberOfitem;
        
        public IList<Exception> ListError
        {
            get { return listError; }
        }

        public int NumberOfitem
        {
            get { return numberOfitem; }
        }

        public InformationFeedSessionBase(int numberOfItem)
        {
            this.numberOfitem = numberOfItem;
            this.Initialize();
        }

        //abstract public void FeedNewsList();
        //abstract public void FeedNewsItem(string listUrl, string shortUrl, int linkId);       

        public bool IsFeedAlready()
        {
            return false;
        }

        //public DateTime ConvertStringToDate(string dateString)
        //{
        //    DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
        //    datefomatProvider.DateSeparator = "/";
        //    datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
        //    datefomatProvider.LongDatePattern = "dd/MM/yyyy";
        //    return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

        //    //return DateTime.Parse(dateString);
        //    //return Convert.ToDateTime(dateString, datefomatProvider);
        //}
        public bool FeedItem(StockNew stockNew)
        {
            bool returnVal = true;
            try
            {
                StockNewService.CreateStockNew(stockNew);
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.InnerException);
                returnVal = false;
            }
            return returnVal;
        }

        private void Initialize()
        {
            listError = new List<Exception>();
            numberOfitem = 0;
        }

        private static void WriteReportToDisk(string contents)
        {
            string fileName = ConfigurationManager.AppSettings["logTextFileName"].ToString();
            FileStream fStream = null;
            if (File.Exists(fileName))
            {
                fStream =File.OpenWrite(fileName);
                //fStream = File.AppendText(fileName);
            }
            else
            {
                fStream = File.OpenWrite(fileName);
            }

            using (TextWriter writer = new StreamWriter(fStream))
            {
                writer.WriteLine(contents);
                writer.Flush();
            }
            fStream.Dispose();
        }
    }
}
