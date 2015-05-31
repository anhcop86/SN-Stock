using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using LumenWorks.Framework.IO.Csv;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Utility;

namespace Vfs.WebCrawler.Destination.Business
{
    public class ImportService
    {
        private const char CSV_DELIMITER = '\t';   

        private static bool ValidateHeaders(string[] typicalHeaders, string[] headers)
        {
            return true;
        }

        private static void ReadCvsData(string csvFilePath, out string[] headers, out List<string[]> data)
        {
            TextReader textReader = null;
            try
            {
                textReader = new StreamReader(csvFilePath, Encoding.Default);

                using (CsvReader csv = new CsvReader(textReader, true, CSV_DELIMITER))
                {
                    headers = csv.GetFieldHeaders();
                    data = new List<string[]>();
                    while (csv.ReadNextRecord())
                    {
                        string[] items = new string[headers.Length];
                        for (int i = 0; i < headers.Length; i++)
                        {
                            items[i] = csv[i];
                        }
                        data.Add(items);
                    }
                }
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        public static void UpdateStockPrice(DateTime date, Stream stream, string filePath, string fileName)
        {
            string[] typicalHeaders = {"Symbol", "BuyOrders", "BuyQuantity", "SellOrders", "SellQuantity"};
            string[] headers;
            List<string[]> data = null;
            try
            {
                string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
                ReadCvsData(filePath + uploadFileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {

                    //shareHolder.ShareHolderCode = items[0];
                    stock_SymbolPermLong stockSymbolPermLong = null;
                    int SymbolId = GetStockSymbolId(items[0]);
                    stockSymbolPermLong = stock_SymbolPermLongService.Getstock_SymbolPermLong(SymbolId, date);
                    if (stockSymbolPermLong != null)
                    {
                        stockSymbolPermLong.BuyCount = Convert.ToDouble(items[1] == string.Empty ? "0" : items[1]);
                        stockSymbolPermLong.BuyQuantity = Convert.ToDouble(items[2] == string.Empty ? "0" : items[2]);
                        stockSymbolPermLong.SellCount = Convert.ToDouble(items[3] == string.Empty ? "0" : items[3]);
                        stockSymbolPermLong.SellQuantity = Convert.ToDouble(items[4] == string.Empty ? "0" : items[4]);
                        stock_SymbolPermLongService.Updatestock_SymbolPermLong(stockSymbolPermLong);
                    }
                }
                if (File.Exists(filePath + uploadFileName))
                {
                    File.Delete(filePath + uploadFileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_UpdateStockInformationException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }
        public static void UpdateStockPriceForeign(DateTime date, Stream stream, string filePath, string fileName)
        {
            string[] typicalHeaders = { "Symbol", "BuyOrders", "SellingOrders", "BuyQuantity", "SellQuantity" };
            string[] headers;
            List<string[]> data = null;
            try
            {
                string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
                ReadCvsData(filePath + uploadFileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {

                    //shareHolder.ShareHolderCode = items[0];
                    stock_SymbolPermLong stockSymbolPermLong = null;
                    int SymbolId = GetStockSymbolId(items[0]);
                    stockSymbolPermLong = stock_SymbolPermLongService.Getstock_SymbolPermLong(SymbolId, date);
                    if (stockSymbolPermLong != null)
                    {
                        stockSymbolPermLong.BuyForeignQuantity = Convert.ToDouble(items[1] == string.Empty ? "0" : items[1]);
                        stockSymbolPermLong.SellForeignQuantity = Convert.ToDouble(items[2] == string.Empty ? "0" : items[2]) ;
                        stockSymbolPermLong.BuyForeignValue = Convert.ToDouble(items[3] == string.Empty ? "0" : items[3]) * 1000;
                        stockSymbolPermLong.SellForeignValue = Convert.ToDouble(items[4] == string.Empty ? "0" : items[4]) * 1000;
                        stock_SymbolPermLongService.Updatestock_SymbolPermLong(stockSymbolPermLong);
                    }
                }
                if (File.Exists(filePath + uploadFileName))
                {
                    File.Delete(filePath + uploadFileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_UpdateStockInformationException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }
        public static void UpdateStockPriceMiss4colume(DateTime date, Stream stream, string filePath, string fileName)
        {
            string[] typicalHeaders = { "Symbol", "PriceHigh", "PriceLow", "Volume", "TotalValue" };
            string[] headers;
            List<string[]> data = null;
            try
            {
                string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
                ReadCvsData(filePath + uploadFileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {

                    //shareHolder.ShareHolderCode = items[0];
                    stock_SymbolPermLong stockSymbolPermLong = null;
                    int SymbolId = GetStockSymbolId(items[0]);
                    stockSymbolPermLong = stock_SymbolPermLongService.Getstock_SymbolPermLong(SymbolId, date);
                    if (stockSymbolPermLong != null)
                    {
                        stockSymbolPermLong.PriceHigh = Convert.ToDouble(items[1] == string.Empty ? "0" : items[1]);
                        stockSymbolPermLong.PriceLow = Convert.ToDouble(items[2] == string.Empty ? "0" : items[2]);
                        stockSymbolPermLong.Volume = Convert.ToDouble(items[3] == string.Empty ? "0" : items[3]);
                        stockSymbolPermLong.TotalValue = Convert.ToDouble(items[4] == string.Empty ? "0" : items[4]);
                        stock_SymbolPermLongService.Updatestock_SymbolPermLong(stockSymbolPermLong);
                    }
                }
                if (File.Exists(filePath + uploadFileName))
                {
                    File.Delete(filePath + uploadFileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_UpdateStockInformationException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }
        private static int GetStockSymbolId(string symbol)
        {
            Int32 stockSymbolId = 0;
            stock_SymbolCollection stock_symbolCollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
            foreach (stock_Symbol stockSymbol in stock_symbolCollection)
            {
                if (stockSymbol.Symbol == symbol)
                {
                    stockSymbolId = stockSymbol.SymbolID;
                    break;
                }
            }
            return stockSymbolId;
        }       
    }   
}
