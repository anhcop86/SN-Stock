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
    public class TransactionCategoryRepository: IRepository<TransactionCategory>
    {
        #region IRepository<Company> Members

        void IRepository<TransactionCategory>.Save(TransactionCategory entity)
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

        void IRepository<TransactionCategory>.Update(TransactionCategory entity)
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

        void IRepository<TransactionCategory>.Delete(TransactionCategory entity)
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

        TransactionCategory IRepository<TransactionCategory>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Company>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<TransactionCategory>();
        }

        TransactionCategory IRepository<TransactionCategory>.GetById(Guid id)
        {
            return new TransactionCategory();
        }

        IList<TransactionCategory> IRepository<TransactionCategory>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(TransactionCategory));
                return criteria.List<TransactionCategory>();
            }
        }

        #endregion
    }
}
