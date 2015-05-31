using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{
    public abstract class statisticTransactionDAOBase
    {
        public virtual statisticTransaction CreateStatisticTransactionFromReader(IDataReader reader)
        {
            statisticTransaction item = new statisticTransaction();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) item.Symbol = (string)reader["Symbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyCount"))) item.BuyCount = (double)reader["BuyCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyQuantity"))) item.BuyQuantity = (double)reader["BuyQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellCount"))) item.SellCount = (double)reader["SellCount"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellQuantity"))) item.SellQuantity = (double)reader["SellQuantity"];
                if (!reader.IsDBNull(reader.GetOrdinal("Change"))) item.Change = (double)reader["Change"];
                if (!reader.IsDBNull(reader.GetOrdinal("DVDMTrungBinhTrenLenh"))) item.DVDMTrungBinhTrenLenh = (double)reader["DVDMTrungBinhTrenLenh"];
                if (!reader.IsDBNull(reader.GetOrdinal("DVDBTrungBinhTrenLenh"))) item.DVDBTrungBinhTrenLenh = (double)reader["DVDBTrungBinhTrenLenh"];
                if (!reader.IsDBNull(reader.GetOrdinal("Volume"))) item.Volume = (double)reader["Volume"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalValue"))) item.TotalValue = (double)reader["TotalValue"];
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolPermLongFromReaderException, ex);
            }
            return item;
        }
        public virtual statisticTransactionCollection ExportStatisticTransaction(DateTime PermDate, string Market)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spStatisticTransaction");

                database.AddInParameter(dbCommand, "@PermDate", DbType.DateTime, PermDate);
                database.AddInParameter(dbCommand, "@Market", DbType.String, Market.ToString());

                statisticTransactionCollection statisticTransactionCollection = new statisticTransactionCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        statisticTransaction statisticTransaction = CreateStatisticTransactionFromReader(reader);
                        statisticTransactionCollection.Add(statisticTransaction);
                    }
                    reader.Close();
                }
                return statisticTransactionCollection;
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
