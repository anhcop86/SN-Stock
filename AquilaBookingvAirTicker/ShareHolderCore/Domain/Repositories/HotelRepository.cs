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
    public class HotelRepository : IRepository<Hotel>, IHotelRepository<Hotel>, IHotelViewlastestRepository<Hotel>
    {
        #region Hotel Members

        void IRepository<Hotel>.Save(Hotel entity)
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

        void IRepository<Hotel>.Update(Hotel entity)
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

        void IRepository<Hotel>.Delete(Hotel entity)
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

        Hotel IRepository<Hotel>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Hotel>().Add(Restrictions.Eq("HotelId", id)).UniqueResult<Hotel>();
        }

        Hotel IRepository<Hotel>.GetById(Guid id)
        {
            return new Hotel();
        }

        IList<Hotel> IRepository<Hotel>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Hotel));
                return criteria.List<Hotel>();
            }
        }

        public IList<Hotel> getHolteMostview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateQuery("SELECT obj   FROM Hotel obj WHERE  obj.MostView = 'Y'");


                resual = query.SetMaxResults(5).List<Hotel>();

                return resual;
            }
        }
        public IList<Hotel> getListHotelMostview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateQuery("SELECT obj   FROM Hotel obj, HotelViewlastest hvl WHERE hvl.HotelId = obj.HotelId  order by hvl.IdLastest DESC");


                resual = query.SetMaxResults(3).List<Hotel>();

                return resual;
            }
        }

        public IList<Hotel> searchHolte(string searchValue)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateSQLQuery("SELECT *   FROM Hotel WHERE Freetext(Location , '"+searchValue+"')");

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(Hotel)));

                


                resual = query.List<Hotel>();

                return resual;
            }
        }
        /*
        public IList<Hotel> searchHolte(string searchValue)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateQuery("SELECT obj   FROM Hotel obj WHERE dbo.fuChuyenCoDauThanhKhongDau(obj.Location) like '%' + :searchValue +'%'");

                query.SetString("searchValue", searchValue);

                resual = query.List<Hotel>();

                return resual;
            }
        }
        */
        public IList<Hotel> getHotDealHotel()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                string dateTimeNow = DateTime.Now.ToString("yyyyMMdd");
                IQuery query = session.CreateQuery("SELECT obj FROM Hotel obj, Availability Av WHERE (Av.FromDate >= :dateTimeNow or Av.ToDate >= :dateTimeNow) AND obj.HotelId = Av.Hotel.HotelId And Av.IsHotDeal = 'Y'");
                query.SetString("dateTimeNow", dateTimeNow);


                resual = query.List<Hotel>().Distinct().ToList<Hotel>();

                return resual;
            }
        }
        public IList<Hotel> getHotDealHotel(string searchValue)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query;
                string dateTimeNow = DateTime.Now.ToString("yyyyMMdd");
                if (string.IsNullOrEmpty(searchValue))
                {
                    query = session.CreateQuery("SELECT obj   FROM Hotel obj, Availability Av WHERE (Av.FromDate >= :dateTimeNow or Av.ToDate >= :dateTimeNow) AND obj.HotelId = Av.Hotel.HotelId And Av.IsHotDeal = 'Y'");
                    query.SetString("dateTimeNow", dateTimeNow);
                }
                else
                {
                    query = session.CreateQuery("SELECT obj   FROM Hotel obj, Availability Av WHERE  (Av.FromDate >= :dateTimeNow or Av.ToDate >= :dateTimeNow) AND obj.HotelId = Av.Hotel.HotelId And Av.IsHotDeal = 'Y' And dbo.fuChuyenCoDauThanhKhongDau(obj.Location) like '%' + :searchValue +'%' ");
                    query.SetString("searchValue", searchValue);
                    query.SetString("dateTimeNow", dateTimeNow);
                }

                resual = query.List<Hotel>().Distinct().ToList<Hotel>();

                return resual;
            }
        }

        public LocationTag getHotelLocationTag(string searchValue)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                LocationTag resual = null;
                //IQuery query = session.CreateSQLQuery("SELECT TOP 1 Location as LocationTagName , CHARINDEX('" + searchValue + "',dbo.fuChuyenCoDauThanhKhongDau(Location),0) as LocationIndex  FROM [Hotel]  WHERE dbo.fuChuyenCoDauThanhKhongDau(Location) like '%" + searchValue + "%' and CHARINDEX('" + searchValue + "',dbo.fuChuyenCoDauThanhKhongDau(Location),0) > 0 Order by LocationIndex ASC");

                IQuery query = session.CreateSQLQuery("	SELECT TOP 1 Location as LocationTagName, len(Location) as LocationIndex  FROM [Hotel]  WHERE Freetext(Location , '" + searchValue + "')  Order by LocationIndex ASC");

                //query.SetString("searchValue", searchValue);

                query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(LocationTag)));
                resual = query.UniqueResult<LocationTag>();
                
                return resual;
            }
        }

        public Hotel gethotelbyHotelOwer(int hotelowerid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Hotel>().Add(Restrictions.Eq("HotelOwnerId", hotelowerid)).UniqueResult<Hotel>();
        }

        public IList<Hotel> getHotelFilterByOwner(int OwnerId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateQuery("SELECT obj  FROM Hotel obj WHERE obj.HotelOwnerId = :OwnerId OR obj.HotelOwnerId IS NULL");

                query.SetInt32("OwnerId", OwnerId);

                resual = query.List<Hotel>();

                return resual;
            }
        }

        public IList<Hotel> getHotelFilterByOwnerId(int OwnerId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<Hotel> resual = null;
                IQuery query = session.CreateQuery("SELECT obj  FROM Hotel obj WHERE obj.HotelOwnerId = :OwnerId");

                query.SetInt32("OwnerId", OwnerId);
                
                resual = query.List<Hotel>();

                return resual;
            }
        }

        public IList<Hotel> getListHotelwithPaging(int pageNumber, int pageSize)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Hotel>().SetFirstResult(pageNumber).SetMaxResults(pageSize).List<Hotel>();

        }

        public int countAllHotel()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                int resual = 0;
                IQuery query = session.CreateSQLQuery(" SELECT  Count (*) from Hotel");

                resual = (int)query.UniqueResult();
                return resual;
            }
        }
        #endregion

    }
}
