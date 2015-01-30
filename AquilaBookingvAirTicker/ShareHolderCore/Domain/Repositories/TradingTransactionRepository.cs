using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class TradingTransactionRepository : ITradingTransactionRepository<TradingTransaction>, IRepository<TradingTransaction>
    {
        #region IRepository<membership> Members

        void IRepository<TradingTransaction>.Save(TradingTransaction entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<TradingTransaction>.Update(TradingTransaction entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<TradingTransaction>.Delete(TradingTransaction entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        TradingTransaction IRepository<TradingTransaction>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<TradingTransaction>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<TradingTransaction>();
        }

        TradingTransaction IRepository<TradingTransaction>.GetById(Guid id)
        {
            return null;
        }

        IList<TradingTransaction> IRepository<TradingTransaction>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(TradingTransaction));
                return criteria.List<TradingTransaction>();
            }
        }

        public IList<TradingTransaction> Search(Int32 shareHolderID, DateTime formDate, DateTime toDate, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("select TradingTransaction from TradingTransaction  TradingTransaction, TransactionDetail  d WHERE  (TradingTransaction.TransactionDate >=:fromDate AND TradingTransaction.TransactionDate <=:toDate  ) AND TradingTransaction.TransactionId = d.TransactionId  AND (TradingTransaction.TransactionCategory.TransactionCategoryId = :trancategory or 0 = :trancategory ) AND d.ShareSymbol =:symbol  AND ShareHolderId = :shareHolderId Order By d.TransactionType DESC " + orderBy + " " + orderDirection);

                query.SetDateTime("fromDate", formDate);
                query.SetDateTime("toDate", toDate);
                query.SetString("symbol", symbol);
                query.SetInt32("trancategory", trancategory);
                query.SetInt32("shareHolderId", shareHolderID);
                //query.SetString("orderBy", orderBy);
                //query.SetString("orderDirection", orderDirection);
                IList<TradingTransaction> transList = query.List<TradingTransaction>();
                rowCount = transList.Count;
                IEnumerable<TradingTransaction> test = transList.Skip((page - 1) * pageSize).Take(pageSize);

                return test.ToList<TradingTransaction>();
            }            
        }

        public IList<TradingTransaction> SearchFromStore(Int32 shareHolderID, DateTime formDate, DateTime toDate, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("select TradingTransaction from TradingTransaction  TradingTransaction, TransactionDetail  d WHERE  (TradingTransaction.TransactionDate >=:fromDate AND TradingTransaction.TransactionDate <=:toDate  ) AND TradingTransaction.TransactionId = d.TransactionId  AND (TradingTransaction.TransactionCategory.TransactionCategoryId = :trancategory or 0 = :trancategory ) AND d.ShareSymbol =:symbol  AND ShareHolderId = :shareHolderId Order By d.TransactionType DESC " + orderBy + " " + orderDirection);

                
                query.SetDateTime("fromDate", formDate);
                query.SetDateTime("toDate", toDate);
                query.SetString("symbol", symbol);
                query.SetInt32("trancategory", trancategory);
                query.SetInt32("shareHolderId", shareHolderID);
                //query.SetString("orderBy", orderBy);
                //query.SetString("orderDirection", orderDirection);
                IList<TradingTransaction> transList = query.List<TradingTransaction>();
                rowCount = transList.Count;
                IEnumerable<TradingTransaction> test = transList.Skip((page - 1) * pageSize).Take(pageSize);

                return test.ToList<TradingTransaction>();
            }
        }
        #endregion
    }
}
