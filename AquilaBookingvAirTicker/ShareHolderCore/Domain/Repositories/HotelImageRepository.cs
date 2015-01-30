using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class HotelImageRepository : IRepository<HotelImage>, IHotelImageRepository<HotelImage>
    {
        #region HotelImage Members

        void IRepository<HotelImage>.Save(HotelImage entity)
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

        void IRepository<HotelImage>.Update(HotelImage entity)
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

        void IRepository<HotelImage>.Delete(HotelImage entity)
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

        HotelImage IRepository<HotelImage>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelImage>().Add(Restrictions.Eq("HotelImageID", id)).UniqueResult<HotelImage>();
        }

        HotelImage IRepository<HotelImage>.GetById(Guid id)
        {
            return new HotelImage();
        }

        IList<HotelImage> IRepository<HotelImage>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelImage));
                return criteria.List<HotelImage>();
            }
        }

        public HotelImage HotelImageofMain(int hotelId)
        {
            
            using (ISession session = NHibernateHelper.OpenSession())
            {

                HotelImage resual = null;
                IQuery query = session.CreateQuery("SELECT hti  FROM HotelImage hti WHERE  hti.Hotel.HotelId = :hotelId And hti.RoomTypeId = NULL Order By hti.SortOrder ");

                query.SetInt32("hotelId", hotelId);
                resual = query.SetMaxResults(1).UniqueResult<HotelImage>(); 

                return resual;
            }
        }

        public IList<HotelImage> HotelImageofAllMain(int hotelId)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<HotelImage> resual = null;
                IQuery query = session.CreateQuery("SELECT hti  FROM HotelImage hti WHERE  hti.Hotel.HotelId = :hotelId And hti.RoomTypeId = NULL Order By hti.SortOrder ");

                query.SetInt32("hotelId", hotelId);
                resual = query.List<HotelImage>();

                return resual;
            }
        }

        public IList<HotelImage> ImageOfHotelRoom(int hotelId)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<HotelImage> resual = null;
                IQuery query = session.CreateQuery("SELECT hti   FROM HotelImage hti WHERE  hti.Hotel.HotelId = :hotelId And hti.RoomTypeId <> NULL Order By hti.SortOrder ");

                query.SetInt32("hotelId", hotelId);
                resual = query.SetMaxResults(4).List<HotelImage>();

                return resual;
            }
        }
        public IList<HotelImage> ImageOfHotelRoom(int hotelId,byte roomtypeId)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<HotelImage> resual = null;
                IQuery query = session.CreateQuery("SELECT hti   FROM HotelImage hti WHERE  hti.Hotel.HotelId = :hotelId And hti.RoomTypeId = :roomtypeId Order By hti.SortOrder ");

                query.SetInt32("hotelId", hotelId);
                query.SetByte("roomtypeId", roomtypeId);
                resual = query.List<HotelImage>();

                return resual;
            }
        }

        public IList<HotelImage> GetAllImageOfHotel(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<HotelImage> resual = null;
                IQuery query = session.CreateQuery("SELECT hti   FROM HotelImage hti WHERE  hti.Hotel.HotelId = :hotelId Order By hti.SortOrder ");

                query.SetInt32("hotelId", hotelId);
                resual = query.List<HotelImage>();

                return resual;
            }
        }
        public HotelImage getHotelImageByImagesStoreId(long imagesStoreId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelImage>().Add(Restrictions.Eq("ImagesStore.ImagesStoreId", imagesStoreId)).UniqueResult<HotelImage>();
        }
        #endregion
    }
}
