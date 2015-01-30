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
    public class HotelFacilityRepository : IRepository<HotelFacility>, IHotelFacilityRepository
    {
        #region IRepository<Company> Members

        void IRepository<HotelFacility>.Save(HotelFacility entity)
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

        void IRepository<HotelFacility>.Update(HotelFacility entity)
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

        void IRepository<HotelFacility>.Delete(HotelFacility entity)
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

        HotelFacility IRepository<HotelFacility>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelFacility>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<HotelFacility>();
        }

        HotelFacility IRepository<HotelFacility>.GetById(Guid id)
        {
            return new HotelFacility();
        }

        IList<HotelFacility> IRepository<HotelFacility>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelFacility));
                return criteria.List<HotelFacility>();
            }
        }

        public int CountFacility(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                int resual = 0;
                IQuery query = session.CreateSQLQuery("SELECT Count(*) from HotelFacility Where HotelId = :hotelId ");

                query.SetInt32("hotelId", hotelId);

                //query.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(int)));
                resual = (int)query.UniqueResult();

                return resual;
            }
        }

        public void InsertFacility(int hotelId, short facilityId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

               
                string createdate = DateTime.Now.ToString("yyyyMMdd");
                string createby = "Aquila";
                IQuery query = session.CreateSQLQuery("INSERT INTO [HotelFacility] VALUES (" + hotelId + "," + facilityId + ",'" + createdate + "','" + createby + "')");
                              
                query.ExecuteUpdate();

                
            }
        }
        public void DeleteFacility(int hotelId, short facilityId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IQuery query = session.CreateSQLQuery("DELETE FROM [HotelFacility] WHERE HotelId = " + hotelId + " AND FacilityId = " + facilityId );
                             
                query.ExecuteUpdate();
                
            }
        }

       
        #endregion
    }
}
