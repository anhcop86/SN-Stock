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
    public class TransactionBalanceRepository: ITransactinBalance<TransactionBalance>
    {
        public IList<TransactionBalance> GetTransactionBalances(string shareHolderId, string shareSymbol, int shareHolderGroupId, string orderBy, string orderDirection, Int32 page, Int32 pageSize, out int totalRecord)
        {
            try
            {
                using(ISession session = NHibernateHelper.OpenSession())
                {
    
                    totalRecord = 0;
                    IQuery namedQuery = session.GetNamedQuery("spTransactionBalanceFrontEnd");
                    namedQuery.SetParameter("ShareHolderId", shareHolderId);
                    namedQuery.SetParameter("ShareSymbol", shareSymbol);
                    namedQuery.SetParameter("ShareHolderGroupId", shareHolderGroupId);
                    namedQuery.SetParameter("OrderBy", orderBy);
                    namedQuery.SetParameter("OrderDirection", orderDirection);
                    namedQuery.SetParameter("Page", page);
                    namedQuery.SetParameter("PageSize", pageSize);
                    namedQuery.SetParameter("TotalRecords", totalRecord);
                    namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(TransactionBalance)));
                    return namedQuery.List<TransactionBalance>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
