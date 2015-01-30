using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class HotelOwnerRepository : IRepository<HotelOwner>, IHotelOwnerRepository
    {
        #region HotelOwner Members

        void IRepository<HotelOwner>.Save(HotelOwner entity)
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

        void IRepository<HotelOwner>.Update(HotelOwner entity)
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

        void IRepository<HotelOwner>.Delete(HotelOwner entity)
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

        HotelOwner IRepository<HotelOwner>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelOwner>().Add(Restrictions.Eq("HotelOwnerId", id)).UniqueResult<HotelOwner>();
        }

        HotelOwner IRepository<HotelOwner>.GetById(Guid id)
        {
            return new HotelOwner();
        }

        IList<HotelOwner> IRepository<HotelOwner>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelOwner));
                return criteria.List<HotelOwner>();
            }
        }

        public HotelOwner getHotelOwnerByMemberShipId(int membershipid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelOwner>().Add(Restrictions.Eq("MemberId", membershipid)).UniqueResult<HotelOwner>();
        }

        #endregion
    }
}
