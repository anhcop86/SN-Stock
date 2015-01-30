using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class CountryRepository : IRepository<Country>, ICountryRepository
    {
        #region Country Members

        void IRepository<Country>.Save(Country entity)
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

        void IRepository<Country>.Update(Country entity)
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

        void IRepository<Country>.Delete(Country entity)
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

        Country IRepository<Country>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Country>().Add(Restrictions.Eq("CountryId", id)).UniqueResult<Country>();
        }

        Country IRepository<Country>.GetById(Guid id)
        {
            return new Country();
        }

        IList<Country> IRepository<Country>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Country));
                return criteria.List<Country>();
            }
        }

        public Country getCountryById(short? it)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Country>().Add(Restrictions.Eq("CountryId", it)).UniqueResult<Country>();
        }

        #endregion
    }
}
