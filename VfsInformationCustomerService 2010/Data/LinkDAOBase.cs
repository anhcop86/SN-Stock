
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Vfs.WebCrawler.Data
{    
    public abstract class LinkDAOBase
    {
        #region Common methods
        public virtual Link CreateLinkFromReader(IDataReader reader)
        {
            Link item = new Link();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("LinkId"))) item.LinkId = (int)reader["LinkId"];
                if (!reader.IsDBNull(reader.GetOrdinal("SourceId"))) item.SourceId = (int)reader["SourceId"];
                if (!reader.IsDBNull(reader.GetOrdinal("Link"))) item.Link = (string)reader["Link"];
                if (!reader.IsDBNull(reader.GetOrdinal("LinkShortDescription"))) item.LinkShortDescription = (string)reader["LinkShortDescription"];
                if (!reader.IsDBNull(reader.GetOrdinal("LinkDescription"))) item.LinkDescription = (string)reader["LinkDescription"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateLinkFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateLink methods
            
        public virtual void CreateLink(Link link)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkInsert");
                
                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, link.SourceId);
                database.AddInParameter(dbCommand, "@Link", DbType.String, link.Link);
                database.AddInParameter(dbCommand, "@LinkShortDescription", DbType.String, link.LinkShortDescription);
                database.AddInParameter(dbCommand, "@LinkDescription", DbType.String, link.LinkDescription);
                database.AddOutParameter(dbCommand, "@LinkId", DbType.Int32, 0);
                
                database.ExecuteNonQuery(dbCommand);
                link.LinkId = (int)database.GetParameterValue(dbCommand, "@LinkId");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateLinkException, ex);
            }
        }

        #endregion

        #region UpdateLink methods
        
        public virtual void UpdateLink(Link link)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkUpdate");            
                
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, link.LinkId);
                database.AddInParameter(dbCommand, "@SourceId", DbType.Int32, link.SourceId);
                database.AddInParameter(dbCommand, "@Link", DbType.String, link.Link);
                database.AddInParameter(dbCommand, "@LinkShortDescription", DbType.String, link.LinkShortDescription);
                database.AddInParameter(dbCommand, "@LinkDescription", DbType.String, link.LinkDescription);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateLinkException, ex);
            }
        }
        
        #endregion

        #region DeleteLink methods
        public virtual void DeleteLink(int linkId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkDelete");
                
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, linkId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteLinkException, ex);
            }
        }

        #endregion

        #region GetLink methods
        
        public virtual Link GetLink(int linkId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkGet");
                
                database.AddInParameter(dbCommand, "@LinkId", DbType.Int32, linkId);
                
                Link link =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        link = CreateLinkFromReader(reader);
                        reader.Close();
                    }
                }
                return link;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetLinkException, ex);
            }
        }
        
        #endregion

        #region GetLinkList methods
        public virtual LinkCollection GetLinkList(LinkColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = database.GetStoredProcCommand("spLinkGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                LinkCollection linkCollection = new LinkCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Link link = CreateLinkFromReader(reader);
                        linkCollection.Add(link);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return linkCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetLinkListException, ex);
            }
        }
        
        public virtual LinkCollection GetLinkList(LinkColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetLinkList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion
    }
}