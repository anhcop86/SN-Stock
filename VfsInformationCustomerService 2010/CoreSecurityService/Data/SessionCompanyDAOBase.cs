using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

using CoreSecurityService.Entities;

namespace CoreSecurityService.Data
{
    public class SessionCompanyDAOBase
    {
        public virtual SessionCompany CreateSessionCompanyFromReader(IDataReader reader)
        {
            SessionCompany item = new SessionCompany();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CompanyID"))) item.CompanyID = (string)reader["CompanyID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CeilingPrice"))) item.CeilingPrice = (decimal)reader["CeilingPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("FloorPrice"))) item.FloorPrice = (decimal)reader["FloorPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("RefPrice"))) item.RefPrice = (decimal)reader["RefPrice"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyPrice1"))) item.BuyPrice1 = (decimal)reader["BuyPrice1"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyAmount1"))) item.BuyAmount1 = (int)reader["BuyAmount1"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyPrice2"))) item.BuyPrice2 = (decimal)reader["BuyPrice2"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyAmount2"))) item.BuyAmount2 = (int)reader["BuyAmount2"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyPrice3"))) item.BuyPrice3 = (decimal)reader["BuyPrice3"];
                if (!reader.IsDBNull(reader.GetOrdinal("BuyAmount3"))) item.BuyAmount3 = (int)reader["BuyAmount3"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellPrice1"))) item.SellPrice1 = (decimal)reader["SellPrice1"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellAmount1"))) item.SellAmount1 = (int)reader["SellAmount1"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellPrice2"))) item.SellPrice2 = (decimal)reader["SellPrice2"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellAmount2"))) item.SellAmount2 = (int)reader["SellAmount2"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellPrice3"))) item.SellPrice3 = (decimal)reader["SellPrice3"];
                if (!reader.IsDBNull(reader.GetOrdinal("SellAmount3"))) item.SellAmount3 = (int)reader["SellAmount3"];
            }
            catch(Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateCustomerFromReaderException, ex);
            }
            return item;
        }
        public virtual SessionCompanyCollection GetSessionCompanyHastcList(DateTime UpdateTime)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("OnlinePriceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spAGStock_HASTC_SessionCompanyGet");

                database.AddInParameter(dbCommand, "@UpdateTime", DbType.DateTime, UpdateTime);

                SessionCompanyCollection sessionCompanyCollection = new SessionCompanyCollection();

                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        SessionCompany sessionCompany = CreateSessionCompanyFromReader(reader);
                        sessionCompanyCollection.Add(sessionCompany);
                    }
                    reader.Close();
                }
                return sessionCompanyCollection;                
            }                
            catch(Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }            
        }

        public virtual SessionCompanyCollection GetSessionCompanyHoseList(DateTime UpdateLETime)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("OnlinePriceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spAGStock_HOSE_SessionCompanyGet");

                database.AddInParameter(dbCommand, "@UpdateLETime", DbType.DateTime, UpdateLETime);

                SessionCompanyCollection sessionCompanyCollection = new SessionCompanyCollection();

                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        SessionCompany sessionCompany = CreateSessionCompanyFromReader(reader);
                        sessionCompanyCollection.Add(sessionCompany);
                    }
                    reader.Close();
                }
                return sessionCompanyCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }
    }
}
