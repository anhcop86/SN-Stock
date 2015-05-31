
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class stock_SymbolDAOBase
    {
        #region Common methods
        public virtual stock_Symbol Createstock_SymbolFromReader(IDataReader reader)
        {
            stock_Symbol item = new stock_Symbol();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) item.SymbolID = (int)reader["SymbolID"];
                if (!reader.IsDBNull(reader.GetOrdinal("SourceID"))) item.SourceID = (string)reader["SourceID"];
                if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) item.Symbol = (string)reader["Symbol"];
                if (!reader.IsDBNull(reader.GetOrdinal("MarketID"))) item.MarketID = (int)reader["MarketID"];
                if (!reader.IsDBNull(reader.GetOrdinal("IndustryID"))) item.IndustryID = (int)reader["IndustryID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CompanyType"))) item.CompanyType = (string)reader["CompanyType"];
                if (!reader.IsDBNull(reader.GetOrdinal("SecType"))) item.SecType = (int)reader["SecType"];
                if (!reader.IsDBNull(reader.GetOrdinal("IsListing"))) item.IsListing = (bool)reader["IsListing"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region Createstock_Symbol methods
            
        public virtual void Createstock_Symbol(stock_Symbol stock_Symbol)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolsInsert");
                
                database.AddInParameter(dbCommand, "@SourceID", DbType.AnsiString, stock_Symbol.SourceID);
                database.AddInParameter(dbCommand, "@Symbol", DbType.AnsiString, stock_Symbol.Symbol);
                database.AddInParameter(dbCommand, "@MarketID", DbType.Int32, stock_Symbol.MarketID);
                database.AddInParameter(dbCommand, "@IndustryID", DbType.Int32, stock_Symbol.IndustryID);
                database.AddInParameter(dbCommand, "@CompanyType", DbType.AnsiString, stock_Symbol.CompanyType);
                database.AddInParameter(dbCommand, "@SecType", DbType.Int32, stock_Symbol.SecType);
                database.AddInParameter(dbCommand, "@IsListing", DbType.Boolean, stock_Symbol.IsListing);
                database.AddOutParameter(dbCommand, "@SymbolID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                stock_Symbol.SymbolID = (int)database.GetParameterValue(dbCommand, "@SymbolID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_SymbolException, ex);
            }
        }

        #endregion

        #region Updatestock_Symbol methods
        
        public virtual void Updatestock_Symbol(stock_Symbol stock_Symbol)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolsUpdate");            
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, stock_Symbol.SymbolID);
                database.AddInParameter(dbCommand, "@SourceID", DbType.AnsiString, stock_Symbol.SourceID);
                database.AddInParameter(dbCommand, "@Symbol", DbType.AnsiString, stock_Symbol.Symbol);
                database.AddInParameter(dbCommand, "@MarketID", DbType.Int32, stock_Symbol.MarketID);
                database.AddInParameter(dbCommand, "@IndustryID", DbType.Int32, stock_Symbol.IndustryID);
                database.AddInParameter(dbCommand, "@CompanyType", DbType.AnsiString, stock_Symbol.CompanyType);
                database.AddInParameter(dbCommand, "@SecType", DbType.Int32, stock_Symbol.SecType);
                database.AddInParameter(dbCommand, "@IsListing", DbType.Boolean, stock_Symbol.IsListing);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdatestock_SymbolException, ex);
            }
        }
        
        #endregion

        #region Deletestock_Symbol methods
        public virtual void Deletestock_Symbol(int symbolID)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolsDelete");
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, symbolID);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_SymbolException, ex);
            }
        }

        #endregion

        #region Getstock_Symbol methods
        
        public virtual stock_Symbol Getstock_Symbol(int symbolID)
        {            
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolsGet");
                
                database.AddInParameter(dbCommand, "@SymbolID", DbType.Int32, symbolID);
                
                stock_Symbol stock_Symbol =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stock_Symbol = Createstock_SymbolFromReader(reader);
                        reader.Close();
                    }
                }
                return stock_Symbol;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolException, ex);
            }
        }
        
        #endregion

        #region Getstock_SymbolList methods
        public virtual stock_SymbolCollection Getstock_SymbolList(stock_SymbolColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_SymbolsGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                stock_SymbolCollection stock_SymbolCollection = new stock_SymbolCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_Symbol stock_Symbol = Createstock_SymbolFromReader(reader);
                        stock_SymbolCollection.Add(stock_Symbol);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stock_SymbolCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_SymbolListException, ex);
            }
        }
        
        public virtual stock_SymbolCollection Getstock_SymbolList(stock_SymbolColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return Getstock_SymbolList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}