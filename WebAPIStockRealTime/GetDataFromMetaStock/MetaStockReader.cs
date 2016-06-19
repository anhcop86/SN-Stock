using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMetaStock
{
    public struct MetaStockRow
    {
        public DateTime Day;
        public double Open;
        public double High;
        public double Low;
        public double Close;
        public double Volume;
        public double OpenInterest;
    }

    public class MetaStockReader
    {
        // stock cache  
        public Dictionary<string, MetaStockTable> StockCache = new Dictionary<string, MetaStockTable>();

        // lists all simbols and maps to F*.DAT or F*.MWD
        public class CatalogEntry
        {
            public string filename;
            public string symbol;
            public string stockname;
            public string timeframe;
            public int numfields;
            public DateTime StartDate;
            public DateTime EndDate;
        }

        // main list of symbols
        public List<CatalogEntry> MainDir = new List<CatalogEntry>();

        public MetaStockReader()
        {
            MainDir = new List<CatalogEntry>();
        }

        // adds a folders (scans for symbols there contained)
        public void AddPath(string path)
        {
            if (!Directory.Exists(path)) throw new Exception("Directory '" + path + "' does not exist");
            //ReadEmaster(MainDir,path);
            ReadXmaster(MainDir, path);
        }

        public MetaStockTable ReadSymbol(string simbolo)
        {
            if (StockCache.ContainsKey(simbolo)) return StockCache[simbolo];

            // trova il simbolo
            foreach (CatalogEntry CE in MainDir) {
                if (CE.symbol.Trim() == simbolo) {
                    MetaStockTable tab = new MetaStockTable();
                    tab.Symbol = CE.symbol;

                    FileStream F = new FileStream(CE.filename, FileMode.Open, FileAccess.Read);
                    BinaryReader BR = new BinaryReader(F);

                    UInt16 max_recs = BR.ReadUInt16();  // 0 ==> unlimited size 
                    UInt16 last_rec = BR.ReadUInt16();  // dathdr7 = 1; ctdata7 starts with 2 

                    if (CE.numfields == 5) BR.ReadBytes(16); // zeroes
                    else if (CE.numfields == 6) BR.ReadBytes(20); // zeroes
                    else if (CE.numfields == 7) BR.ReadBytes(24); // zeroes

                    // mega toppa, talvolta il file inizia con 20 zeri invece di 24!
                    // if(bbb[23]!=0) F.Seek(-4, SeekOrigin.Current);

                    for (int t = 1; t < last_rec; t++) {
                        MetaStockRow data = new MetaStockRow();
                        data.Day = DateFromSingle(ConvertMbf4ToFloat(BR.ReadBytes(4)));
                        data.Open = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        data.High = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        data.Low = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        data.Close = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        if (CE.numfields > 5) data.Volume = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        if (CE.numfields > 6) data.OpenInterest = ConvertMbf4ToFloat(BR.ReadBytes(4));
                        tab.Add(data);
                    }

                    BR.Close();
                    F.Close();

                    StockCache.Add(simbolo, tab);

                    return tab;
                }
            }
            throw new Exception("Symbol " + simbolo + " not found");
        }

        private void ReadEmaster(List<CatalogEntry> MainDir, string path)
        {
            string EMasterName = path + "\\EMASTER";
            if (!File.Exists(EMasterName)) return;

            // reads header structure          
            FileStream F = new FileStream(EMasterName, FileMode.Open, FileAccess.Read);
            BinaryReader BR = new BinaryReader(F);

            // reads header
            int num_files = 200;   // number of files in emaster 
            int file_num = 200;    // last (highest) file number           
            BR.ReadBytes(188);                 // [188]

            for (int t = 0; t < num_files; t++) {
                CatalogEntry DE = new CatalogEntry();
                BR.ReadBytes(2); // char asc30[2] = "30"
                DE.filename = String.Format("{1}\\F{0}.dat", BR.ReadByte(), path); // file number F# 
                BR.ReadBytes(3); // filler
                DE.numfields = (int)BR.ReadByte();   // u_char num_fields;	    number of 4-byte data fields 
                if (DE.numfields == 0) DE.numfields = 7;   // in some files 0 means "default" 7 fields
                BR.ReadBytes(2); // filler
                BR.ReadBytes(1); // char flag;		          /* ' ' or '*' for autorun 
                BR.ReadBytes(1); // filler 
                DE.symbol = AsciiString(BR.ReadBytes(14)); // stock symbol 
                BR.ReadBytes(7); // filler 
                DE.stockname = AsciiString(BR.ReadBytes(16)).Trim(); // stock name
                BR.ReadBytes(12); // filler  
                DE.timeframe = AsciiString(BR.ReadBytes(1));  // data format: 'D'/'W'/'M'/ etc. 
                BR.ReadBytes(3); // filler
                BR.ReadSingle(); //DE.StartDate = DateFromSingle(BR.ReadSingle());                                           
                BR.ReadBytes(4); // filler  
                BR.ReadSingle(); //DE.EndDate = DateFromSingle(BR.ReadSingle());               
                BR.ReadBytes(116); // filler                
                MainDir.Add(DE);
            }

            BR.Close();
            F.Close();
        }

        private void ReadXmaster(List<CatalogEntry> MainDir, string path)
        {
            string XMasterName = path + "\\XMASTER";
            if (!File.Exists(XMasterName)) return;

            // reads header structure          
            FileStream F = new FileStream(XMasterName, FileMode.Open, FileAccess.Read);
            BinaryReader BR = new BinaryReader(F);

            // reads header
            BR.ReadBytes(10); // filler
            int num_files = BR.ReadUInt16();   // number of files in emaster                  
            BR.ReadBytes(138);                 // filler

            for (int t = 0; t < num_files; t++) {
                CatalogEntry DE = new CatalogEntry();
                BR.ReadBytes(1); // unknown
                DE.symbol = AsciiString(BR.ReadBytes(15)); // stock symbol 
                DE.stockname = AsciiString(BR.ReadBytes(46)).Trim(); // stock name
                DE.numfields = 7;
                DE.timeframe = AsciiString(BR.ReadBytes(1));  // data format: 'D'/'W'/'M'/ etc.               
                BR.ReadBytes(2); // unknown
                DE.filename = String.Format("{1}\\F{0}.mwd", BR.ReadUInt16(), path); // file number F#               
                BR.ReadBytes(13); // filler
                BR.ReadUInt32();  // dividend date?                           
                BR.ReadBytes(20); // filler
                BR.ReadUInt32();  // last split date?            
                DE.StartDate = DateFromInt(BR.ReadUInt32()); // start date again?
                BR.ReadBytes(4); // unknown
                DE.EndDate = DateFromInt(BR.ReadUInt32()); // enddate again?
                BR.ReadBytes(30); // unknown
                MainDir.Add(DE);
            }

            BR.Close();
            F.Close();
        }

        private static string AsciiString(byte[] source)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int t = 0;
            for (t = 0; t < source.Length; t++) {
                if (source[t] == '\0') break;
            }
            return ascii.GetString(source, 0, t);
        }

        private static DateTime DateFromSingle(float s)
        {
            int si = (int)s;
            int d = si % 100; si = si / 100;
            int m = si % 100; si = si / 100;
            int y = si + 1900;

            // check for corrupt data
            if (d < 1 || m < 1 || y < 1900 || d > 31 || m > 12 || y > 2020) return new DateTime(1870, 1, 1);

            return new DateTime(y, m, d);
        }

        private static DateTime DateFromInt(UInt32 s)
        {
            int si = (int)s;
            int d = si % 100; si = si / 100;
            int m = si % 100; si = si / 100;
            int y = si;
            return new DateTime(y, m, d);
        }

        // courtesy of Julian M Bucknall http://www.boyet.com/Articles/MBFSinglePrecision.html
        private static float ConvertMbf4ToFloat(byte[] mbf)
        {
            if ((mbf == null) || (mbf.Length != 4)) throw new ArgumentException("Invalid MBF array");

            if (mbf[3] == 0) return 0.0f;
            //if(mbf[3] <= 2) throw new ArgumentException("Underflow when converting from MBF to single");

            UInt32 temp = BitConverter.ToUInt32(mbf, 0);
            temp = (((temp - 0x02000000) & 0xFF000000) >> 1) | ((temp & 0x00800000) << 8) | (temp & 0x007FFFFF);
            byte[] single = BitConverter.GetBytes(temp);
            return BitConverter.ToSingle(single, 0);
        }
    }
}
