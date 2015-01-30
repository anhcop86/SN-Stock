using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class RoomTypeRepository : IRepository<RoomType>, IRoomTypeRepository<RoomType>
    {
        #region RoomType Members

        void IRepository<RoomType>.Save(RoomType entity)
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

        void IRepository<RoomType>.Update(RoomType entity)
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

        void IRepository<RoomType>.Delete(RoomType entity)
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

        RoomType IRepository<RoomType>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<RoomType>().Add(Restrictions.Eq("RoomTypeId", id)).UniqueResult<RoomType>();
        }

        RoomType IRepository<RoomType>.GetById(Guid id)
        {
            return new RoomType();
        }

        IList<RoomType> IRepository<RoomType>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(RoomType));
                return criteria.List<RoomType>();
            }
        }

        public RoomType GetById(byte? id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<RoomType>().Add(Restrictions.Eq("RoomTypeId", id)).UniqueResult<RoomType>();
        }

        #endregion
    }
}
