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
    public class AvailabilityRepository : IRepository<Availability>, IAvailabilityRepository<Availability>
    {
        #region IRepository<Company> Members

        void IRepository<Availability>.Save(Availability entity)
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

        void IRepository<Availability>.Update(Availability entity)
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

        void IRepository<Availability>.Delete(Availability entity)
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

        Availability IRepository<Availability>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Availability>().Add(Restrictions.Eq("AvailabilityId", id)).UniqueResult<Availability>();
        }

        Availability IRepository<Availability>.GetById(Guid id)
        {
            return new Availability();
        }

        IList<Availability> IRepository<Availability>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Availability)).AddOrder(Order.Desc("FromDate")); ;
                return criteria.List<Availability>();
            }
        }
        public decimal getPrice(int idhotel)
        {
            String currentDate = DateTime.Now.ToString("yyyyMMdd");
            using (ISession session = NHibernateHelper.OpenSession())
            {

                decimal resual = Decimal.Zero;
                IQuery query = session.CreateQuery("SELECT MIN(obj.Price) FROM Availability obj WHERE  obj.Hotel.HotelId = :idhotel AND (obj.FromDate<=:currentDate AND obj.ToDate>=:currentDate)");

                query.SetInt32("idhotel", idhotel);
                query.SetAnsiString("currentDate", currentDate);
           
                if (query.UniqueResult() != null)
                    resual = (decimal)query.UniqueResult();
                else
                    resual = Decimal.Zero;
                
                 
                return resual;
            }
        }
        
        public decimal getPrice(int idhotel, int roomType)
        {
            String currentDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0'); 
            using (ISession session = NHibernateHelper.OpenSession())
            {

                decimal resual = Decimal.Zero;
                IQuery query = session.CreateQuery("SELECT MIN(obj.Price) FROM Availability obj WHERE obj.Hotel.HotelId = :idhotel AND obj.Hotel.RoomTypeId=:roomtype AND obj.FromDate>=:fdDate AND obj.ToDate<=:tDate");

                query.SetInt32("idhotel", idhotel);
                query.SetInt32("roomtype", roomType);
                query.SetAnsiString("fDate", currentDate);
                query.SetAnsiString("tDate", currentDate);

                if (query.UniqueResult() != null)
                    resual = (decimal)query.UniqueResult();
                else
                    resual = Decimal.Zero;


                return resual;
            }
        }
        public IList<MinHotelPrice> getListPrice(int idhotel)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<MinHotelPrice> resual = new List<MinHotelPrice>();

                IQuery query = session.CreateSQLQuery("SELECT DISTINCT  HotelId, RoomTypeId ,  MIN([Price]) AS Price  FROM Availability  WHERE HotelId = :idhotel  GROUP BY HotelId, RoomTypeId");
                query.SetInt32("idhotel", idhotel);

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(MinHotelPrice)));

                resual = query.List<MinHotelPrice>();


                return resual;
            }
        }
        public IList<Availability> getAvailabilityfromHotelid(int idhotel)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Availability> resual = null;
                IQuery query = session.CreateQuery("SELECT obj   FROM Availability obj WHERE obj.Hotel.HotelId = :idhotel");

                query.SetInt32("idhotel", idhotel);

                resual = query.List<Availability>();

                return resual;
            }
        }

        public Availability getAvailabilityById(Int64 idAvailability)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Availability>().Add(Restrictions.Eq("AvailabilityId", idAvailability)).UniqueResult<Availability>();
        }
        public IList<HotelIdAndRoomtypeId> searchHotel(string name, int fromndate, int todate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<HotelIdAndRoomtypeId> resual = null;
                //IQuery query = session.CreateQuery("SELECT obj FROM Availability obj, Hotel ht WHERE (FromDate>= cast(getdate() as date) or ToDate>= cast(getdate() as date)) And ((FromDate between :fromndate and :todate)  or (ToDate between :fromndate and :todate) or (FromDate<= :fromndate) or (ToDate>= :todate)) And ht.HotelId = obj.Hotel.HotelId And dbo.fuChuyenCoDauThanhKhongDau(ht.Location) like '%' + :name +'%'");

                IQuery query = session.CreateSQLQuery("EXEC	[searchHotelId]	@FromDate = :fromndate,@ToDate = :todate,@Location = :name");

                query.SetString("name", name);
                query.SetInt32("fromndate", fromndate);
                query.SetInt32("todate", todate);

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(HotelIdAndRoomtypeId)));
                resual = query.List<HotelIdAndRoomtypeId>();

                return resual;
            }
        }

        public IList<HotelIdAndRoomtypeId> searchHotel(int hotelid, int fromndate, int todate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<HotelIdAndRoomtypeId> resual = null;
                //IQuery query = session.CreateQuery("SELECT obj FROM Availability obj, Hotel ht WHERE (FromDate>= cast(getdate() as date) or ToDate>= cast(getdate() as date)) And ((FromDate between :fromndate and :todate)  or (ToDate between :fromndate and :todate) or (FromDate<= :fromndate) or (ToDate>= :todate)) And ht.HotelId = obj.Hotel.HotelId And obj.Hotel.HotelId = :hotelid ");

                IQuery query = session.CreateSQLQuery("EXEC [searchWithHotelId] @FromDate = :fromndate,@ToDate = :todate,@HotelId = :hotelid");

                query.SetInt32("hotelid", hotelid);
                query.SetInt32("fromndate", fromndate);
                query.SetInt32("todate", todate);

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(HotelIdAndRoomtypeId)));
                resual = query.List<HotelIdAndRoomtypeId>();


                return resual;
            }
        }

        public IList<Availability> getAvailabilityFilterFromHotelOwner(int ownerid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Availability> resual = null;
                IQuery query = session.CreateQuery("SELECT A  FROM Availability A, Hotel H WHERE A.Hotel.HotelId = H.HotelId AND H.HotelOwnerId = :ownerid order by A.FromDate DESC ");

                query.SetInt32("ownerid", ownerid);

                resual = query.List<Availability>();

                return resual;
            }
        }

        public IList<Availability> getAvailabilitywithPaging(int pageNumber, int pageSize)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Availability>().SetFirstResult(pageNumber).SetMaxResults(pageSize).AddOrder(Order.Desc("CreatedDate")).List<Availability>();
        }
        public int countAllAvailability()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                int resual = 0;
                IQuery query = session.CreateSQLQuery("SELECT  Count (*) from Availability");

                resual = (int)query.UniqueResult();
                return resual;
            }
        }

        public decimal GetMinPriceOfHotelHotDeal(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                decimal Minprice = 0;
                string dateTimeNow = DateTime.Now.ToString("yyyyMMdd");

                IQuery query = session.CreateQuery("SELECT MIN(Av.Price)  FROM  Availability Av WHERE (Av.FromDate >= :dateTimeNow or Av.ToDate >= :dateTimeNow) AND  Av.IsHotDeal = 'Y' AND Av.Hotel.HotelId = :hotelId");
                query.SetInt32("hotelId", hotelId);
                query.SetString("dateTimeNow", dateTimeNow);                
                Minprice = (decimal)query.UniqueResult();

                return Minprice;
            }
        }

        public decimal GetMaxPriceOfHotelHotDeal(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                decimal Maxprice = 0;
                string dateTimeNow = DateTime.Now.ToString("yyyyMMdd");

                IQuery query = session.CreateQuery("SELECT MAX(Av.Price)  FROM  Availability Av WHERE (Av.ToDate <=:dateTimeNow)  AND Av.Hotel.HotelId = :hotelId");
                query.SetString("dateTimeNow", dateTimeNow);
                query.SetInt32("hotelId", hotelId);
                
                if (query.UniqueResult() != null)
                    Maxprice = (decimal)query.UniqueResult();

                return Maxprice;
            }
        }
        #endregion
    }
}
