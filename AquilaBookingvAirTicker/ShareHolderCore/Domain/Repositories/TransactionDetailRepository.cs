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
    public class TransactionDetailRepository: IRepository<TransactionDetail>
    {
        #region IRepository<Company> Members

        void IRepository<TransactionDetail>.Save(TransactionDetail entity)
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

        void IRepository<TransactionDetail>.Update(TransactionDetail entity)
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

        void IRepository<TransactionDetail>.Delete(TransactionDetail entity)
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

        TransactionDetail IRepository<TransactionDetail>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<TransactionDetail>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<TransactionDetail>();
        }

        TransactionDetail IRepository<TransactionDetail>.GetById(Guid id)
        {
            return new TransactionDetail();
        }

        IList<TransactionDetail> IRepository<TransactionDetail>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(TransactionDetail));
                return criteria.List<TransactionDetail>();
            }
        }

        #endregion
    }
}
