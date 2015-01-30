using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{

    public class HotelCommentRepository : IRepository<HotelComment>, IHotelCommentRepository<HotelComment>
    {
        void IRepository<HotelComment>.Save(HotelComment entity)
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

        void IRepository<HotelComment>.Update(HotelComment entity)
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

        void IRepository<HotelComment>.Delete(HotelComment entity)
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

        HotelComment IRepository<HotelComment>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<HotelComment>().Add(Restrictions.Eq("CommentId", id)).UniqueResult<HotelComment>();
        }

        HotelComment IRepository<HotelComment>.GetById(Guid id)
        {
            return new HotelComment();
        }

        IList<HotelComment> IRepository<HotelComment>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(HotelComment));
                return criteria.List<HotelComment>();
            }
        }

        public IList<HotelComment> getListHotelCommentFromHotelId(int hotelId)
        {
           
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(HotelComment));
                SimpleExpression exp1 = Expression.Eq("HotelId", hotelId);               
                crit.Add(exp1);
                crit.AddOrder(Order.Desc("CommentDate"));
                
                return crit.List<HotelComment>();
            }
        }

        public void deleteCommnet(long commentid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery(" DELETE HotelComment WHERE CommentId = :commentid");
                query.SetInt64("commentid", commentid);

                query.ExecuteUpdate();
            }
        }

        public int countComment(int hotelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int result = 0;
                IQuery query = session.CreateSQLQuery("SELECT Count(*) from HotelComment where HotelId = :hotelId");
                query.SetInt32("hotelId", hotelId);

                result = (int) query.UniqueResult();

                return result;
            }

        }
    }
}
