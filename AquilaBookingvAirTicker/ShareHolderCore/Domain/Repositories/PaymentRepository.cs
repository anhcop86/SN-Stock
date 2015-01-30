using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class PaymentRepository : IRepository<Payment>, IPaymentRepository<Payment>
    {
        #region Payment Members

        void IRepository<Payment>.Save(Payment entity)
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

        void IRepository<Payment>.Update(Payment entity)
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

        void IRepository<Payment>.Delete(Payment entity)
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

        Payment IRepository<Payment>.GetById(int id)
        {
            return new Payment(); // su dung ham ben duoi, GetById(long id)
        }

        Payment IRepository<Payment>.GetById(Guid id)
        {
            return new Payment();
        }

        IList<Payment> IRepository<Payment>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Payment)).AddOrder(Order.Desc("PaymentDate"));
                return criteria.List<Payment>();
            }
        }

        public Payment GetById(long id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Payment>().Add(Restrictions.Eq("PaymentId", id)).UniqueResult<Payment>();
        }

        public Payment GetByBookingCode(string bookingCodeid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Payment>().Add(Restrictions.Eq("BookingCode", bookingCodeid)).UniqueResult<Payment>();
        }


        public int countAllPayment()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                int resual = 0;
                IQuery query = session.CreateSQLQuery("SELECT  Count (*) from Payment");

                resual = (int)query.UniqueResult();
                return resual;
            }
        }

        public IList<Payment> getPaymentwithPaging(int pageNumber, int pageSize)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Payment>().SetFirstResult(pageNumber).SetMaxResults(pageSize).AddOrder(Order.Desc("PaymentDate")).List<Payment>();
        }

        public IList<Payment> getPaymentFilterFromHotelOwner(int ownerid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Payment> resual = null;
                IList<Booking> listBooking = new List<Booking>();
                // list booking
                IQuery query = session.CreateQuery("SELECT B  FROM Booking B, BookingDetail BD, Hotel H WHERE BD.Booking.BookingId = B.BookingId AND BD.HotelId = H.HotelId AND H.HotelOwnerId = :ownerid");
                query.SetInt32("ownerid", ownerid);
                listBooking = query.List<Booking>();
                // get all view code for filter
                StringBuilder viewcodeString = new StringBuilder();
                foreach (var item in listBooking)
                {
                    viewcodeString.Append("'" + item.ViewCode + "',");
                }

                // get payment 
                IQuery query1 = null;
                if (!string.IsNullOrEmpty(viewcodeString.ToString()))
                {
                    query1 = session.CreateQuery("SELECT P FROM Payment P WHERE P.BookingCode IN (" + viewcodeString.Remove(viewcodeString.Length - 1, 1) + ")");
                    resual = query1.List<Payment>();
                }
                else
                {
                    resual = new List<Payment>();
                }
                

                return resual;
            }
        }

        #endregion

    }
   
}
