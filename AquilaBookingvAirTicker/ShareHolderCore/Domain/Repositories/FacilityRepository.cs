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
    public class FacilityRepository : IRepository<Facility>, IFacilityRepository
    {
        #region IRepository<Company> Members

        void IRepository<Facility>.Save(Facility entity)
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

        void IRepository<Facility>.Update(Facility entity)
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

        void IRepository<Facility>.Delete(Facility entity)
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

        Facility IRepository<Facility>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Facility>().Add(Restrictions.Eq("FacilityId", id)).UniqueResult<Facility>();
        }

        Facility IRepository<Facility>.GetById(Guid id)
        {
            return new Facility();
        }

        IList<Facility> IRepository<Facility>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Facility));
                return criteria.List<Facility>();
            }
        }
        public string CheckFacility(int hotelId, int facilityid)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                string resual = String.Empty;
                IQuery query = session.CreateQuery("SELECT obj FROM HotelFacility obj WHERE  obj.HotelId = :hotelId And obj.FacilityId = :facilityid");

                query.SetInt32("hotelId", hotelId);
                query.SetInt32("facilityid", facilityid);

                if (query.UniqueResult() != null)
                    resual = "Y";
                else
                    resual = "N";


                return resual;
            }
        }
        public Facility GetById(Int16 id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Facility>().Add(Restrictions.Eq("FacilityId", id)).UniqueResult<Facility>();
        }

        #endregion
    }
}
