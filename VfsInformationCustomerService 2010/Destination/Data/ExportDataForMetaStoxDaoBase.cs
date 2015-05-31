using System;
using System.Collections.Generic;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Vfs.WebCrawler.Destination.Data
{
    public abstract class ExportDataForMetaStoxDaoBase
    {
        public virtual ExportDataForMetaStox CreateExportDataForMetaStoxFromReader(IDataReader reader)
        {
            ExportDataForMetaStox item = new ExportDataForMetaStox();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) item.Symbol = (string)reader["Symbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("PermDate"))) item.PermDate = (string)reader["PermDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceOpen"))) item.PriceOpen = (double)reader["PriceOpen"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceClose"))) item.PriceClose = (double)reader["PriceClose"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceHigh"))) item.PriceHigh = (double)reader["PriceHigh"];
                if (!reader.IsDBNull(reader.GetOrdinal("PriceLow"))) item.PriceLow = (double)reader["PriceLow"];
                if (!reader.IsDBNull(reader.GetOrdinal("Volume"))) item.Volume = (double)reader["Volume"];                
                
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
        public virtual ExportDataForMetaStoxCollection GetExportDataForMetaStoxList(DateTime PermDate, int marketId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExportDataForMetaStox");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, PermDate);
                database.AddInParameter(dbCommand, "@MarketId", DbType.Int32, marketId);

                ExportDataForMetaStoxCollection EDMetaCollection = new ExportDataForMetaStoxCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ExportDataForMetaStox EDMeta = CreateExportDataForMetaStoxFromReader(reader);
                        EDMetaCollection.Add(EDMeta);
                    }
                    reader.Close();
                }
                return EDMetaCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
        public virtual ExportDataForMetaStoxCollection GetExportIndexForMetaStoxList(DateTime PermDate, int marketId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spExportIndexForMetaStox");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, PermDate);
                database.AddInParameter(dbCommand, "@MarketId", DbType.Int32, marketId);

                ExportDataForMetaStoxCollection EDMetaCollection = new ExportDataForMetaStoxCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ExportDataForMetaStox EDMeta = CreateExportDataForMetaStoxFromReader(reader);
                        EDMetaCollection.Add(EDMeta);
                    }
                    reader.Close();
                }
                return EDMetaCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolPermLongListException, ex);
            }
        }
    }
}
