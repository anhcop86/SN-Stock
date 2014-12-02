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
    public class INDEXDAOBase
    {
        #region Common methods
        public virtual INDEX CreateShareTypeFromReader(IDataReader reader)
        {
            INDEX item = new INDEX();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("IndexName"))) item.IndexName = (string)reader["IndexName"];
                if (!reader.IsDBNull(reader.GetOrdinal("Index"))) item.Index = (decimal)reader["Index"];
                if (!reader.IsDBNull(reader.GetOrdinal("SessionDate"))) item.SessionDate = (DateTime)reader["SessionDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("Total"))) item.Total = (decimal)reader["Total"];
                if (!reader.IsDBNull(reader.GetOrdinal("TotalShare"))) item.TotalShare = (int)reader["TotalShare"];
                if (!reader.IsDBNull(reader.GetOrdinal("Diff"))) item.Diff = (decimal)reader["Diff"];
                if (!reader.IsDBNull(reader.GetOrdinal("DiffRate"))) item.DiffRate = (decimal)reader["DiffRate"];

                if (!reader.IsDBNull(reader.GetOrdinal("Advances"))) item.Advances = (int)reader["Advances"];
                if (!reader.IsDBNull(reader.GetOrdinal("Declines"))) item.Declines = (int)reader["Declines"];
                if (!reader.IsDBNull(reader.GetOrdinal("NoChange"))) item.NoChange = (int)reader["NoChange"];

                
                
                
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
     

        #region GetShareTypeList methods
        public virtual INDEX GetINDEXHSX()
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabasePriceHSX");
                DbCommand dbCommand = database.GetStoredProcCommand("HN_AGStock_Session_SelectIndex");

                INDEX item = new INDEX();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        item = CreateShareTypeFromReader(reader);
                        break;
                        //listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return item;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
        }

        public virtual INDEX GetINDEXHNX()
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabasePriceHNX");
                DbCommand dbCommand = database.GetStoredProcCommand("HN_AGStock_Session_SelectHNXIndex");

                INDEX item = new INDEX();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        item = CreateShareTypeFromReader(reader);
                        break;
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return item;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw;
            }
        }
        #endregion
        #endregion
    }
}
