using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{
    public abstract class PosNochangeDownOfStockDAOBase
    {
        public virtual PosNochangeDownOfStock CreatePosNochangeDownOfStockFromReader(IDataReader reader)
        {
            PosNochangeDownOfStock item = new PosNochangeDownOfStock();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Date"))) item.Date = (string)reader["Date"];
                if (!reader.IsDBNull(reader.GetOrdinal("Pos"))) item.Pos = (int)reader["Pos"];
                if (!reader.IsDBNull(reader.GetOrdinal("Nochange"))) item.Nochange = (int)reader["Nochange"];
                if (!reader.IsDBNull(reader.GetOrdinal("Down"))) item.Down = (int)reader["Down"];
                if (!reader.IsDBNull(reader.GetOrdinal("Market"))) item.Market = (string)reader["Market"];
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
        public virtual PosNochangeDownOfStockCollection GetPosNochangeDownOfStockCollection(DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spGetPosNochangeDownOfStock");

                database.AddInParameter(dbCommand, "@Fromdate", DbType.DateTime, fromDate);
                database.AddInParameter(dbCommand, "@Todate", DbType.DateTime, toDate);

                PosNochangeDownOfStockCollection posNochangeDownOfStockCollection = new PosNochangeDownOfStockCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        PosNochangeDownOfStock posNochangeDownOfStock = CreatePosNochangeDownOfStockFromReader(reader);
                        posNochangeDownOfStockCollection.Add(posNochangeDownOfStock);
                    }
                    reader.Close();
                }
                return posNochangeDownOfStockCollection;
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
