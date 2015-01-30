using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;
using NTransform = NHibernate.Transform;

namespace ShareHolderCore.Domain.Repositories
{
    public class BookingDetailRepository : IRepository<BookingDetail>, IBookingDetailRepository<BookingDetail>
    {
        #region Booking Members

        void IRepository<BookingDetail>.Save(BookingDetail entity)
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

        void IRepository<BookingDetail>.Update(BookingDetail entity)
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

        void IRepository<BookingDetail>.Delete(BookingDetail entity)
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

        BookingDetail IRepository<BookingDetail>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<BookingDetail>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<BookingDetail>();
        }

        BookingDetail IRepository<BookingDetail>.GetById(Guid id)
        {
            return new BookingDetail();
        }

        IList<BookingDetail> IRepository<BookingDetail>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(BookingDetail));
                return criteria.List<BookingDetail>();
            }
        }

        public IList<SearchBooking> searchBooking(int hotelId, byte roomtypeId, int fromndate, int todate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<SearchBooking> resual = null;
                //IQuery query = session.CreateQuery("SELECT obj FROM BookingDetail obj WHERE (FromDate>= cast(getdate() as date) or ToDate>= cast(getdate() as date)) And((obj.FromDate between :fromndate and :todate)  or (obj.ToDate between :fromndate and :todate)  or (FromDate<= :fromndate) or (ToDate>= :todate)) And  obj.HotelId = :hotelId and obj.RoomType.RoomTypeId = :roomtypeId ");

                IQuery query = session.CreateSQLQuery("EXEC	[searchBooking] @FromDate = :fromndate, @ToDate = :todate,	@HotelId = :hotelId,	@RoomTypeId = :roomtypeId");

                query.SetInt32("hotelId", hotelId);
                query.SetByte("roomtypeId", roomtypeId);
                query.SetInt32("fromndate", fromndate);
                query.SetInt32("todate", todate);

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(SearchBooking)));
                resual = query.List<SearchBooking>();

                

                return resual;
            }
        }
        public IList<BookingDetail> getListByBookingid(long bookingid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<BookingDetail>().Add(Restrictions.Eq("Booking.BookingId", bookingid)).List<BookingDetail>();

           
           
        }

       public IList<BookingDetail> getListFilterByHotelOwner(int ownerid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<BookingDetail> resual = null;
                IQuery query = session.CreateQuery("SELECT BD  FROM BookingDetail BD, Hotel H WHERE BD.HotelId = H.HotelId AND H.HotelOwnerId = :ownerid");

                query.SetInt32("ownerid", ownerid);

                resual = query.List<BookingDetail>();

                return resual;
            }
        }

       public IList<BookingDetail> getBookingByViewCode(string viewcode)
       {
           using (ISession session = NHibernateHelper.OpenSession())
           {
               IList<BookingDetail> resual = null;
               IQuery query = session.CreateQuery("SELECT BD  FROM BookingDetail BD, Booking B WHERE BD.Booking.BookingId = B.BookingId AND B.ViewCode = :viewcode");

               query.SetString("viewcode", viewcode);

               resual = query.List<BookingDetail>();

               return resual;
           }
       }

        #endregion
    }
}
