using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class CurrencyTypeRepository : IRepository<CurrencyType>, ICurrencyTypeRepository
    {
        #region CurrencyType Members

        void IRepository<CurrencyType>.Save(CurrencyType entity)
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

        void IRepository<CurrencyType>.Update(CurrencyType entity)
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

        void IRepository<CurrencyType>.Delete(CurrencyType entity)
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

        CurrencyType IRepository<CurrencyType>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<CurrencyType>().Add(Restrictions.Eq("CurrencyTypeId", id)).UniqueResult<CurrencyType>();
        }

        CurrencyType IRepository<CurrencyType>.GetById(Guid id)
        {
            return new CurrencyType();
        }

        IList<CurrencyType> IRepository<CurrencyType>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(CurrencyType));
                return criteria.List<CurrencyType>();
            }
        }

        public CurrencyType GetById(byte id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<CurrencyType>().Add(Restrictions.Eq("CurrencyTypeId", id)).UniqueResult<CurrencyType>();
        }
        #endregion
    }
}
