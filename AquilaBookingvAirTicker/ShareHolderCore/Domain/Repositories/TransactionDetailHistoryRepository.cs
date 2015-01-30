using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;

using ShareHolderCore.Domain;
using NHibernate;
using NHibernate.Criterion;
using NTransform = NHibernate.Transform;

namespace ShareHolderCore.Domain.Repositories
{
    public class TransactionDetailHistoryRepository : ITransactionDetailHistory<TransactionDetailHistory>
    {
        public IList<TransactionDetailHistory> Search(DateTime formDate, DateTime toDate,Int32 shareHolderID, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection)
        {
            rowCount = 0;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery namedQuery = session.GetNamedQuery("spSearchTransactionDetailHistoryFrontEnd");
                namedQuery.SetParameter("TransactionFromDate", formDate);
                namedQuery.SetParameter("TransactionToDate", toDate);
                namedQuery.SetParameter("ShareHolderId", shareHolderID);
                namedQuery.SetParameter("ShareSymbol", symbol);
                namedQuery.SetParameter("TransactionCategoryId", trancategory);  
                namedQuery.SetParameter("OrderBy", orderBy);  
                namedQuery.SetParameter("OrderDirection", orderDirection);
                namedQuery.SetParameter("Page", page);
                namedQuery.SetParameter("PageSize", pageSize);
                namedQuery.SetParameter("TotalRecords", 0);

                namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(TransactionDetailHistory)));
                return namedQuery.List<TransactionDetailHistory>();
                }
        }
    }
}
