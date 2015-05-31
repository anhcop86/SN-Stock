
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class stock_NewsGroupDAOBase
    {
        #region Common methods
        public virtual stock_NewsGroup Createstock_NewsGroupFromReader(IDataReader reader)
        {
            stock_NewsGroup item = new stock_NewsGroup();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ID"))) item.ID = (int)reader["ID"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsID"))) item.NewsID = (int)reader["NewsID"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsGroup"))) item.NewsGroup = (int)reader["NewsGroup"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_NewsGroupFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region Createstock_NewsGroup methods
            
        public virtual void Createstock_NewsGroup(stock_NewsGroup stock_NewsGroup)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsInsert");
                
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int32, stock_NewsGroup.NewsID);
                database.AddInParameter(dbCommand, "@NewsGroup", DbType.Int32, stock_NewsGroup.NewsGroup);
                database.AddOutParameter(dbCommand, "@ID", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                stock_NewsGroup.ID = (int)database.GetParameterValue(dbCommand, "@ID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreatestock_NewsGroupException, ex);
            }
        }

        #endregion

        #region Updatestock_NewsGroup methods
        
        public virtual void Updatestock_NewsGroup(stock_NewsGroup stock_NewsGroup)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsUpdate");            
                
                database.AddInParameter(dbCommand, "@ID", DbType.Int32, stock_NewsGroup.ID);
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int32, stock_NewsGroup.NewsID);
                database.AddInParameter(dbCommand, "@NewsGroup", DbType.Int32, stock_NewsGroup.NewsGroup);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdatestock_NewsGroupException, ex);
            }
        }
        
        #endregion

        #region Deletestock_NewsGroup methods
        public virtual void Deletestock_NewsGroup(int id)
        {
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsDelete");
                
                database.AddInParameter(dbCommand, "@ID", DbType.Int32, id);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeletestock_NewsGroupException, ex);
            }
        }

        #endregion

        #region Getstock_NewsGroup methods
        
        public virtual stock_NewsGroup Getstock_NewsGroup(int id)
        {            
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsGet");
                
                database.AddInParameter(dbCommand, "@ID", DbType.Int32, id);
                
                stock_NewsGroup stock_NewsGroup =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        stock_NewsGroup = Createstock_NewsGroupFromReader(reader);
                        reader.Close();
                    }
                }
                return stock_NewsGroup;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewsGroupException, ex);
            }
        }
        
        #endregion

        #region Getstock_NewsGroupList methods
        public virtual stock_NewsGroupCollection Getstock_NewsGroupList(stock_NewsGroupColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                //Database database = DatabaseFactory.CreateDatabase();
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spstock_NewsGroupsGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                stock_NewsGroupCollection stock_NewsGroupCollection = new stock_NewsGroupCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        stock_NewsGroup stock_NewsGroup = Createstock_NewsGroupFromReader(reader);
                        stock_NewsGroupCollection.Add(stock_NewsGroup);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return stock_NewsGroupCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetstock_NewsGroupListException, ex);
            }
        }
        
        public virtual stock_NewsGroupCollection Getstock_NewsGroupList(stock_NewsGroupColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return Getstock_NewsGroupList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}