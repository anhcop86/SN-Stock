using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class BookingRepository : IRepository<Booking>, IBookingRepository
    {
        #region Booking Members

        void IRepository<Booking>.Save(Booking entity)
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

        void IRepository<Booking>.Update(Booking entity)
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

        void IRepository<Booking>.Delete(Booking entity)
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

        Booking IRepository<Booking>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Booking>().Add(Restrictions.Eq("BookingId", id)).UniqueResult<Booking>();
        }

        Booking IRepository<Booking>.GetById(Guid id)
        {
            return new Booking();
        }

        IList<Booking> IRepository<Booking>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Booking)).AddOrder(Order.Desc("BookingDate"));
                return criteria.List<Booking>();
            }
        }



        #endregion
        #region ext
        public Booking getBookingFromViewCode(string viewCode)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                Booking resual = null;
                IQuery query = session.CreateQuery("SELECT obj FROM Booking obj WHERE ViewCode = :viewCode ");

                query.SetString("viewCode", viewCode);

                resual = query.UniqueResult<Booking>();

                return resual;
            }
        }

        public Booking getBookingId(long bookingId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Booking>().Add(Restrictions.Eq("BookingId", bookingId)).UniqueResult<Booking>();
        }

        public IList<Booking> getBookingFromOwnerId(int ownerId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Booking> resual = null;
                IQuery query = session.CreateQuery("SELECT B  FROM Booking B, BookingDetail BD, Hotel H WHERE B.BookingId = BD.Booking.BookingId  AND BD.HotelId = H.HotelId AND H.HotelOwnerId = :ownerId");

                query.SetInt32("ownerId", ownerId);

                resual = query.List<Booking>();

                return resual;
            }
        }

        public IList<Booking> getBookingwithPaging(int pageNumber, int pageSize)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Booking>().SetFirstResult(pageNumber).SetMaxResults(pageSize).List<Booking>();
        }
        public int countAllBooking()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                int resual = 0;
                IQuery query = session.CreateSQLQuery("SELECT Count (*) from Booking");

                resual = (int)query.UniqueResult();
                return resual;
            }
        }

        public IList<Booking> getBookingFromMemberId(int memberid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Booking>().Add(Restrictions.Eq("Membership.MemberId", memberid)).AddOrder(Order.Desc("BookingDate")).List<Booking>();
        }
        #endregion
    }
}
