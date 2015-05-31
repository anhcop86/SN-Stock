using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


using CoreSecurityService.Entities;
using Ader.TemplateEngine;

namespace CoreSecurityService.Business
{
    public class ExportService
    {
        public static int TotalSymbol;
        public static string getMarket;

        public static byte[] ExportSessionCompanyToExcel(SessionCompanyCollection sessionCompanyCollection, string templatePath)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();

            model["sessionCompanyCollection"] = sessionCompanyCollection;
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
            template.Functions.Add("GetSumTotalSymbol", new TemplateFunction(GetSumTotalSymbol));
            template.Functions.Add("GetMarket", new TemplateFunction(GetMarket));

            StringWriter writer = new StringWriter();
            template.Process(writer);
            writer.Flush();

            byte[] data = Encoding.UTF8.GetBytes(writer.GetStringBuilder().ToString());

            return data;
        }
        private static object GetSumTotalSymbol(object[] args)
        {
            return TotalSymbol;
        }
        private static object GetMarket(object[] args)
        {
            return getMarket;
        }
    }
}
