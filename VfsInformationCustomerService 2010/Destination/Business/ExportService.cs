using System;
using System.Collections.Generic;
using Ader.TemplateEngine;
using System.Text;
using System.IO;
using System.Security;

using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Business
{    
    public class ExportService
    {
        public static string getDate;
        public static string getMarket;
        public static double SumBuyCount;
        public static double SumBuyQuatity;
        public static double SumSellCount;
        public static double SumSellQuatity;
        public static double SumChange;
        public static double SumDVDMTrungBinhTrenLenh;
        public static double SumDVDBTrungBinhTrenLenh;
        public static double SumVolume;
        public static double SumTotalValue;

        public static byte[] ExportDataForMetaStoxToExcel(ExportDataForMetaStoxCollection exportDataForMetaStoxCollection, string templatePath)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();

            model["ExportDataForMetaStoxCollection"] = exportDataForMetaStoxCollection;
            return ExportToExcel(model, templatePath);
        }
        public static byte[] ExportStock_SymbolPermLongToExcel(stock_SymbolPermLongExtensionCollection stock_SymbolPermLongExtensionCollection, string templatePath)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();

            model["stock_SymbolPermLongExtensionCollection"] = stock_SymbolPermLongExtensionCollection;
            return ExportToExcel(model, templatePath);
        }
        public static byte[] ExportStatisticTransactionToExcel(statisticTransactionCollection statisticTransactionCollection, string templatePath)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();

            model["statisticTransactionCollection"] = statisticTransactionCollection;
            return ExportToExcel(model, templatePath);
        }   
        private static byte[] ExportToExcel(Dictionary<string, object> model, string templatePath)
        {
            TemplateManager template = TemplateManager.FromFile(templatePath);
            if (model != null)
            {
                foreach (string key in model.Keys)
                {
                    template.SetValue(key, model[key]);
                }
            }
            template.Functions.Add("DateToString", new TemplateFunction(DateToString));
            template.Functions.Add("GetDate", new TemplateFunction(GetDate));
            template.Functions.Add("GetMarket", new TemplateFunction(GetMarket));
            template.Functions.Add("GetSumBuyCount", new TemplateFunction(GetSumBuyCount));
            template.Functions.Add("GetSumBuyQuatity", new TemplateFunction(GetSumBuyQuatity));
            template.Functions.Add("GetSumSellCount", new TemplateFunction(GetSumSellCount));
            template.Functions.Add("GetSumSellQuatity", new TemplateFunction(GetSumSellQuatity));
            template.Functions.Add("GetSumChange", new TemplateFunction(GetSumChange));
            template.Functions.Add("GetSumDVDMTrungBinhTrenLenh", new TemplateFunction(GetSumDVDMTrungBinhTrenLenh));
            template.Functions.Add("GetSumDVDBTrungBinhTrenLenh", new TemplateFunction(GetSumDVDBTrungBinhTrenLenh));
            template.Functions.Add("GetSumVolume", new TemplateFunction(GetSumVolume));
            template.Functions.Add("GetSumTotalValue", new TemplateFunction(GetSumTotalValue));          
                        
            StringWriter writer = new StringWriter();
            template.Process(writer);
            writer.Flush();

            byte[] data = Encoding.UTF8.GetBytes(writer.GetStringBuilder().ToString());

            return data;
        }
        private static object DateToString(object[] args)
        {
            DateTime value = (DateTime)args[0];
            string dateFormat = "dd/MM/yyyy";

            if (args.Length > 1)
            {
                dateFormat = (string)args[1];
            }
            if (value > new DateTime(1900, 1, 1, 0, 0, 0, 0))
            {
                return value.ToString(dateFormat);
            }
            else
            {
                return "";
            }
        }
        private static object GetDate(object[] args)
        {
            return getDate;
        }
        private static object GetMarket(object[] args)
        {
            return getMarket;
        }
        private static object GetSumBuyCount(object[] args)
        {
            return SumBuyCount;
        }
        private static object GetSumBuyQuatity(object[] args)
        {
            return SumBuyQuatity;
        }
        private static object GetSumSellCount(object[] args)
        {
            return SumSellCount;
        }
        private static object GetSumSellQuatity(object[] args)
        {
            return SumSellQuatity;
        }
        private static object GetSumChange(object[] args)
        {
            return SumChange;
        }
        private static object GetSumDVDMTrungBinhTrenLenh(object[] args)
        {
            return SumDVDMTrungBinhTrenLenh ;
        }
        private static object GetSumDVDBTrungBinhTrenLenh(object[] args)
        {
            return SumDVDBTrungBinhTrenLenh;
        }
        private static object GetSumVolume(object[] args)
        {
            return SumVolume;
        }
        private static object GetSumTotalValue(object[] args)
        {
            return SumTotalValue;
        }

    }
}
