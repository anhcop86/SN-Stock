
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Vfs.WebCrawler.Destination.Entities;

namespace Vfs.WebCrawler.Destination.Data
{    
    public abstract class RelatedMessagelogDAOBase
    {
        #region Common methods
        public virtual RelatedMessagelog CreateRelatedMessagelogFromReader(IDataReader reader)
        {
            RelatedMessagelog item = new RelatedMessagelog();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("RelatedMessagelogID"))) item.RelatedMessagelogID = (long)reader["RelatedMessagelogID"];
                if (!reader.IsDBNull(reader.GetOrdinal("NewsID"))) item.NewsID = (long)reader["NewsID"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerID"))) item.CustomerID = (string)reader["CustomerID"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateRelatedMessagelogFromReaderException, ex);
            }
            return item;
        }
        #endregion

        #region CreateRelatedMessagelog methods

        public virtual void CreateRelatedMessagelog(RelatedMessagelog relatedMessagelog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogInsert");

                database.AddInParameter(dbCommand, "@NewsID", DbType.Int64, relatedMessagelog.NewsID);
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, relatedMessagelog.CustomerID);
                database.AddOutParameter(dbCommand, "@RelatedMessagelogID", DbType.Int64, 0);

                database.ExecuteNonQuery(dbCommand);
                relatedMessagelog.RelatedMessagelogID = (long)database.GetParameterValue(dbCommand, "@RelatedMessagelogID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateRelatedMessagelogException, ex);
            }
        }

        public virtual void CreateRelatedMessagelog2(RelatedMessagelog relatedMessagelog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnectionNewWebsite");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogInsert");

                database.AddInParameter(dbCommand, "@NewsID", DbType.Int64, relatedMessagelog.NewsID);
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, relatedMessagelog.CustomerID);
                database.AddOutParameter(dbCommand, "@RelatedMessagelogID", DbType.Int64, 0);

                database.ExecuteNonQuery(dbCommand);
                relatedMessagelog.RelatedMessagelogID = (long)database.GetParameterValue(dbCommand, "@RelatedMessagelogID");
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateRelatedMessagelogException, ex);
            }
        }

        #endregion

        #region UpdateRelatedMessagelog methods

        public virtual void UpdateRelatedMessagelog(RelatedMessagelog relatedMessagelog)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogUpdate");

                database.AddInParameter(dbCommand, "@RelatedMessagelogID", DbType.Int64, relatedMessagelog.RelatedMessagelogID);
                database.AddInParameter(dbCommand, "@NewsID", DbType.Int64, relatedMessagelog.NewsID);
                database.AddInParameter(dbCommand, "@CustomerID", DbType.AnsiString, relatedMessagelog.CustomerID);

                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateRelatedMessagelogException, ex);
            }
        }

        #endregion

        #region DeleteRelatedMessagelog methods
        public virtual void DeleteRelatedMessagelog(long relatedMessagelogID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogDelete");

                database.AddInParameter(dbCommand, "@RelatedMessagelogID", DbType.Int64, relatedMessagelogID);

                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteRelatedMessagelogException, ex);
            }
        }

        #endregion

        #region GetRelatedMessagelog methods

        public virtual RelatedMessagelog GetRelatedMessagelog(long relatedMessagelogID)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogGet");

                database.AddInParameter(dbCommand, "@RelatedMessagelogID", DbType.Int64, relatedMessagelogID);

                RelatedMessagelog relatedMessagelog = null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        relatedMessagelog = CreateRelatedMessagelogFromReader(reader);
                        reader.Close();
                    }
                }
                return relatedMessagelog;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetRelatedMessagelogException, ex);
            }
        }

        #endregion

        #region GetRelatedMessagelogList methods
        public virtual RelatedMessagelogCollection GetRelatedMessagelogList(RelatedMessagelogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DestinationConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spRelatedMessagelogGetList");

                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                RelatedMessagelogCollection relatedMessagelogCollection = new RelatedMessagelogCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        RelatedMessagelog relatedMessagelog = CreateRelatedMessagelogFromReader(reader);
                        relatedMessagelogCollection.Add(relatedMessagelog);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return relatedMessagelogCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetRelatedMessagelogListException, ex);
            }
        }

        public virtual RelatedMessagelogCollection GetRelatedMessagelogList(RelatedMessagelogColumns orderBy, string orderDirection)
        {
            int totalRecords = 0;
            return GetRelatedMessagelogList(orderBy, orderDirection, 0, 0, out totalRecords);
        }

        #endregion
    }
}