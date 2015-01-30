using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class HotelViewlastestRepository : IRepository<HotelViewlastest>
    {
        #region HotelViewlastest Members

        void IRepository<HotelViewlastest>.Save(HotelViewlastest entity)
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

        void IRepository<HotelViewlastest>.Update(HotelViewlastest entity)
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

        void IRepository<HotelViewlastest>.Delete(HotelViewlastest entity)
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

        HotelViewlastest IRepository<HotelViewlastest>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelViewlastest>().Add(Restrictions.Eq("HotelId", id)).UniqueResult<HotelViewlastest>();
        }

        HotelViewlastest IRepository<HotelViewlastest>.GetById(Guid id)
        {
            return new HotelViewlastest();
        }

        IList<HotelViewlastest> IRepository<HotelViewlastest>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelViewlastest));
                return criteria.List<HotelViewlastest>();
            }
        }

        #endregion
    }
}
