using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain;
using NHibernate;
using NHibernate.Criterion;
using NTransform = NHibernate.Transform;
namespace ShareHolderCore.Domain.Repositories
{
    public class ShareHolderSummaryRepository: IShareHolderSummary<TransactionDetailOfShareHolder>
    {
         public IList<TransactionDetailOfShareHolder> GetCurrentBalance(DateTime toDate, string shareHolderCode, string shareSymbol, string orderBy, string orderDirection, Int32 page, Int32 pageSize)
         {
             using (ISession session = NHibernateHelper.OpenSession())             
            {
                IQuery namedQuery = session.GetNamedQuery("spTransactionDetailOfShareHolder");
                namedQuery.SetParameter("TransactionToDate", toDate);
                namedQuery.SetParameter("ShareHolderCode", shareHolderCode);
                namedQuery.SetParameter("ShareSymbol", shareSymbol);
                namedQuery.SetParameter("OrderBy", orderBy);
                namedQuery.SetParameter("OrderDirection", orderDirection);
                namedQuery.SetParameter("Page", page);
                namedQuery.SetParameter("PageSize", 100);
                namedQuery.SetParameter("TotalRecords", pageSize);
                namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(TransactionDetailOfShareHolder)));
                return namedQuery.List<TransactionDetailOfShareHolder>();
            }
            
        }
    }
}
