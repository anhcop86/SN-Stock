using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Vfs.WebCrawler.Destination.Data
{
    public abstract class SymbolPermLongTestToolDaoBase
    {
        public virtual SymbolPermLongTestTool CreateSymbolPermLongTestToolFromReader(IDataReader reader)
        {
            SymbolPermLongTestTool item = new SymbolPermLongTestTool();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("SymbolTo"))) item.SymbolTo = (string)reader["SymbolTo"];
                if (!reader.IsDBNull(reader.GetOrdinal("SymbolFrom"))) item.SymbolFrom = (string)reader["SymbolFrom"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceLowFrom"))) item.PriceLowFrom = (double)reader["PriceLowFrom"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceHighTo"))) item.PriceHighTo = (double)reader["PriceHighTo"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceCloseTo"))) item.PriceCloseTo = (double)reader["PriceCloseTo"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceCloseFrom"))) item.PriceCloseFrom = (double)reader["PriceCloseFrom"];
                if (!reader.IsDBNull(reader.GetOrdinal("AVGVolume"))) item.AVGVolume = (double)reader["AVGVolume"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_NewFromReaderException, ex);
            }
            return item;
        }
        public virtual SymbolPermLongTestToolCollection SymbolPermLongGetListTestTool(DateTime fromDate, DateTime toDate, int countAVG)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spSymbolPermLongGetListTestTool");

                database.AddInParameter(dbCommand, "@PermDateFrom", DbType.DateTime, fromDate);
                database.AddInParameter(dbCommand, "@PermDateto", DbType.DateTime, toDate);
                database.AddInParameter(dbCommand, "@countAVG", DbType.Int32, countAVG);

                SymbolPermLongTestToolCollection symbolPLTT = new SymbolPermLongTestToolCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        SymbolPermLongTestTool symbolPermLongTestTool = CreateSymbolPermLongTestToolFromReader(reader);
                        symbolPLTT.Add(symbolPermLongTestTool);
                    }
                    reader.Close();
                }
                return symbolPLTT;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_NewException, ex);
            }
        }
    }
}
