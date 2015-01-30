using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class FavouritDestinationRepository : IRepository<FavouritDestination>
    {
        #region FavouritDestination Members

        void IRepository<FavouritDestination>.Save(FavouritDestination entity)
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

        void IRepository<FavouritDestination>.Update(FavouritDestination entity)
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

        void IRepository<FavouritDestination>.Delete(FavouritDestination entity)
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

        FavouritDestination IRepository<FavouritDestination>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<FavouritDestination>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<FavouritDestination>();
        }

        FavouritDestination IRepository<FavouritDestination>.GetById(Guid id)
        {
            return new FavouritDestination();
        }

        IList<FavouritDestination> IRepository<FavouritDestination>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(FavouritDestination));
                return criteria.List<FavouritDestination>();
            }
        }

        #endregion
    }
}
