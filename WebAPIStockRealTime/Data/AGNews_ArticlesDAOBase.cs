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
    public abstract class AGNews_ArticlesDAOBase
    {
        #region Common methods
        public virtual AGNews_Articles CreateShareTypeFromReader(IDataReader reader)
        {
            AGNews_Articles item = new AGNews_Articles();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ArticleId"))) item.ArticleId = (int)reader["ArticleId"];
                if (!reader.IsDBNull(reader.GetOrdinal("Company"))) item.Company = (string)reader["Company"];

                if (!reader.IsDBNull(reader.GetOrdinal("Title"))) item.Title = (string)reader["Title"];
                if (!reader.IsDBNull(reader.GetOrdinal("Lead"))) item.Lead = (string)reader["Lead"];

                if (!reader.IsDBNull(reader.GetOrdinal("Content"))) item.Content = (string)reader["Content"];
                if (!reader.IsDBNull(reader.GetOrdinal("Source"))) item.Source = (string)reader["Source"];

                if (!reader.IsDBNull(reader.GetOrdinal("ImageFile"))) item.ImageFile = (string)reader["ImageFile"];
                if (!reader.IsDBNull(reader.GetOrdinal("ImageNote"))) item.ImageNote = (string)reader["ImageNote"];

                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) item.CreatedDate = (DateTime)reader["CreatedDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedDate"))) item.UpdatedDate = (DateTime)reader["UpdatedDate"];
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
        #region GetShareTypeList methods
        public virtual List<AGNews_Articles> GetAGNews_ArticlesListByTickerList(string tickerList, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("DefaultDatabase");
                DbCommand dbCommand = database.GetStoredProcCommand("HN__AGNews_Articles_SelectByTicker");

                database.AddInParameter(dbCommand, "@TickerList", DbType.String, tickerList.ToString());                
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                List<AGNews_Articles> shareTypeCollection = new List<AGNews_Articles>();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        AGNews_Articles shareType = CreateShareTypeFromReader(reader);
                        shareTypeCollection.Add(shareType);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return shareTypeCollection;
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
