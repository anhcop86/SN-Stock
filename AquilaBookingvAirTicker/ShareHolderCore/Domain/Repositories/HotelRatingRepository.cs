using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using ShareHolderCore.Domain.Model;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class HotelRatingRepository : IRepository<HotelRating>, IHotelRatingRepository
    {
        void IRepository<HotelRating>.Save(HotelRating entity)
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

        void IRepository<HotelRating>.Update(HotelRating entity)
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

        void IRepository<HotelRating>.Delete(HotelRating entity)
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

        HotelRating IRepository<HotelRating>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelRating>().Add(Restrictions.Eq("RatingId", id)).UniqueResult<HotelRating>();
        }

        HotelRating IRepository<HotelRating>.GetById(Guid id)
        {
            return new HotelRating();
        }

        IList<HotelRating> IRepository<HotelRating>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelRating));
                return criteria.List<HotelRating>();
            }
        }

        public decimal getAverageofRatingWithHotel(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery(" SELECT CASE WHEN  (AVG(CAST(rate AS FLOAT)) IS NULL) THEN 0 ELSE  AVG(CAST(rate AS FLOAT)) END from HotelRating where HotelId = :hotelId");
                query.SetInt32("hotelId", hotelId);

                var resual = decimal.Parse(query.UniqueResult().ToString());
                return (decimal)resual;
            }
        }
       
    }
}
