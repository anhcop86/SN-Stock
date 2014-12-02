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
    public class IPProxyDao
    {
        #region Common methods
        public virtual IPProxy CreateShareTypeFromReader(IDataReader reader)
        {
            IPProxy item = new IPProxy();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Id"))) item.Id = (int)reader["Id"];
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress"))) item.IPAddress = (string)reader["IPAddress"];
                if (!reader.IsDBNull(reader.GetOrdinal("IPPort"))) item.IPPort = (int)reader["IPPort"];
                if (!reader.IsDBNull(reader.GetOrdinal("StatusIP"))) item.StatusIP = (bool)reader["StatusIP"];
                if (!reader.IsDBNull(reader.GetOrdinal("CreateDate"))) item.CreateDate = (int)reader["CreateDate"];             


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
        #region Insert proxy
        public virtual void CreateIPProxy(IPProxy iPProxy)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabaseProxy");
                DbCommand dbCommand = database.GetStoredProcCommand("PR_IPProxy_Insert");

                database.AddInParameter(dbCommand, "@IPAddress", DbType.AnsiString, iPProxy.IPAddress);
                database.AddInParameter(dbCommand, "@IPPort", DbType.Int32, iPProxy.IPPort);
                database.AddInParameter(dbCommand, "@StatusIP", DbType.Boolean, iPProxy.StatusIP);
                database.AddInParameter(dbCommand, "@CreateDate", DbType.Int32, iPProxy.CreateDate);
                database.AddOutParameter(dbCommand, "@Id", DbType.Int32, iPProxy.Id);

                database.ExecuteNonQuery(dbCommand);
                iPProxy.Id = (int)database.GetParameterValue(dbCommand, "@Id");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException();
            }
        }

        public virtual List<IPProxy> GetIPProxyListAll()
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DatabaseProxy");
                DbCommand dbCommand = database.GetStoredProcCommand("PR_IPProxy_SelectAll");

                //database.AddInParameter(dbCommand, "@TickerList", DbType.String, tickerList.ToString());

                List<IPProxy> listItem = new List<IPProxy>();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        IPProxy item = CreateShareTypeFromReader(reader);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
                //totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return listItem;
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

    }
}
