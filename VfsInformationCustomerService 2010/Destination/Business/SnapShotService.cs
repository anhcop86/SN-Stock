using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;

namespace Vfs.WebCrawler.Destination.Business
{
    public class SnapShotService
    {
        public SnapShotService()
        {
        }
        private string _NameSheet;
        public string NameSheet
        {
            get { return _NameSheet; }
            set { _NameSheet = value; }
        }
        public void CreateSnapShot(Stream stream, string SampleFilePath, string HOSESnapShotfilePath, string HNXSnapShotfilePath,string fileNameSnapShot,string cellNameNameSnapShot)
        {
            string fileName=fileNameSnapShot;            
            string upLoadedfileName = string.Empty;

            if (File.Exists(SampleFilePath + fileName))
            {
                File.Delete(SampleFilePath + fileName);
            }

            try
            {
                string uploadFileName = Vfs.WebCrawler.Destination.Utility.UploadService.UploadDocument(stream, SampleFilePath, fileName, true);
                
                stock_SymbolCollection stock_Symbolcollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
                foreach (stock_Symbol stock_symbol in stock_Symbolcollection)
                {
                    if (stock_symbol.Symbol.Length == 3)
                    {
                        if (stock_symbol.MarketID == 4)
                            upLoadedfileName = HNXSnapShotfilePath + "\\" + stock_symbol.Symbol + uploadFileName;                                                        
                        else
                            upLoadedfileName = HOSESnapShotfilePath + "\\" + stock_symbol.Symbol + uploadFileName;                                                
                        File.Copy(SampleFilePath + uploadFileName, upLoadedfileName, true);                
                    }
                }

                foreach (stock_Symbol stock_symbol in stock_Symbolcollection)
                {
                    if (stock_symbol.Symbol.Length == 3)
                    {
                        if (stock_symbol.MarketID == 4)
                            upLoadedfileName = HNXSnapShotfilePath + "\\" + stock_symbol.Symbol + uploadFileName;
                        else
                            upLoadedfileName = HOSESnapShotfilePath + "\\" + stock_symbol.Symbol + uploadFileName;
                        SetStockSymbol(upLoadedfileName, stock_symbol.Symbol,cellNameNameSnapShot);
                    }
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(ex.Message, ex);
            }

            finally
            {
                
            }
        }

        public void CreateSnapShot(string PhysicalSampleFile, string PhysicalHOSESnapShotfilePath, string PhysicalHNXSnapShotfilePath, string fileNameSnapShot, string cellNameSnapShot)
        {
            string fileName = fileNameSnapShot;
            string upLoadedfileName = string.Empty;

            if (!File.Exists(PhysicalSampleFile))
            {
                throw new ApplicationException("Sample file not found");
            }

            try
            {
                string uploadFileName = PhysicalSampleFile;

                stock_SymbolCollection stock_Symbolcollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
                foreach (stock_Symbol stock_symbol in stock_Symbolcollection)
                {
                    if (stock_symbol.Symbol.Length == 3)
                    {
                        if (stock_symbol.MarketID == 4)
                            upLoadedfileName = PhysicalHNXSnapShotfilePath + stock_symbol.Symbol + fileName;
                        else
                            upLoadedfileName = PhysicalHOSESnapShotfilePath + stock_symbol.Symbol + fileName;
                        File.Copy(PhysicalSampleFile, upLoadedfileName, true);
                    }
                }

                foreach (stock_Symbol stock_symbol in stock_Symbolcollection)
                {
                    if (stock_symbol.Symbol.Length == 3)
                    {
                        if (stock_symbol.MarketID == 4)
                            upLoadedfileName = PhysicalHNXSnapShotfilePath + stock_symbol.Symbol + fileName;
                        else
                            upLoadedfileName = PhysicalHOSESnapShotfilePath + stock_symbol.Symbol + fileName;
                        SetStockSymbol(upLoadedfileName, stock_symbol.Symbol,cellNameSnapShot);
                    }
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(ex.Message, ex);
            }

            finally
            {

            }
        }

        private void SetStockSymbol(string filePath, string stockSymbol,string cellName)
        {
            try
            {
                Excel.Application sXL = new Excel.Application();
                Excel.Workbook oWB = sXL.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Excel.Worksheet oSheet = (Excel.Worksheet)oWB.Sheets[NameSheet];
                Excel.Range oRng = oSheet.get_Range(cellName, cellName);
                oRng.set_Value(Missing.Value, stockSymbol);
                //oRng.Sort(oRng.Columns[1, Type.Missing], Excel.XlSortOrder.xlAscending, oRng.Columns[2,Type.Missing], Type.Missing, Excel.XlSortOrder.xlAscending, Type.Missing, Excel.XlSortOrder.xlAscending, Excel.XlYesNoGuess.xlNo, Type.Missing, Type.Missing, Excel.XlSortOrientation.xlSortColumns, Excel.XlSortMethod.xlPinYin, Excel.XlSortDataOption.xlSortNormal, Excel.XlSortDataOption.xlSortNormal, Excel.XlSortDataOption.xlSortNormal); 
                oWB.Save();
                sXL.Quit();
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(ex.Message, ex);
            }

            finally
            {

            }
        }
    }
}
