using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Data
{
    public class StockRealTimeDAO
    {
        #region Common methods
        private static StockRealTime CreateShareTypeFromReader(IDataReader reader)
        {
            StockRealTime item = new StockRealTime();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CompanyID"))) item.CompanyID = (string)reader["CompanyID"];
                if (!reader.IsDBNull(reader.GetOrdinal("FinishPrice"))) item.FinishPrice = (decimal)reader["FinishPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("Diff"))) item.Diff = (decimal)reader["Diff"];
                if (!reader.IsDBNull(reader.GetOrdinal("DiffRate"))) item.DiffRate = (decimal)reader["DiffRate"];               

            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
            return item;

        }
        #endregion

        public static List<StockRealTime> GetALLTwoMarket()
        {
            List<StockRealTime> listItem = new List<StockRealTime>();

            #region Get stock of HOSE
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabasePriceHSX");
                DbCommand dbCommand = database.GetStoredProcCommand("VFS_HSX_GETALLStockRealTime_IncludeIndex");

                //Parameter if have                

                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        StockRealTime item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }

            #endregion

            #region get stock of HNX
            // get HNX

            try
            {
                Database database2 = DatabaseFactory.CreateDatabase("DatabasePriceHNX");
                DbCommand dbCommand2 = database2.GetStoredProcCommand("VFS_HNX_GETALLStockRealTime_IncludeIndex");

                //Parameter if have                

                using (IDataReader reader = database2.ExecuteReader(dbCommand2))
                {
                    while (reader.Read())
                    {
                        StockRealTime item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");

            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }

            #region region get stock of UPCOM
            try
            {
                Database database2 = DatabaseFactory.CreateDatabase("DatabasePriceHNX");
                DbCommand dbCommand2 = database2.GetStoredProcCommand("VFS_UPcom_GETALLStockRealTime_IncludeIndex");

                //Parameter if have                

                using (IDataReader reader = database2.ExecuteReader(dbCommand2))
                {
                    while (reader.Read())
                    {
                        StockRealTime item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");

            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
            #endregion


            #endregion
            return listItem;
        }

        
    }
}
