using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class ImagesStoreRepository : IRepository<ImagesStore>, IImagesStoreRepository

    {
        #region ImagesStore Members

        void IRepository<ImagesStore>.Save(ImagesStore entity)
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

        void IRepository<ImagesStore>.Update(ImagesStore entity)
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

        void IRepository<ImagesStore>.Delete(ImagesStore entity)
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

        ImagesStore IRepository<ImagesStore>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ImagesStore>().Add(Restrictions.Eq("ImagesStoreId", id)).UniqueResult<ImagesStore>();
        }

        ImagesStore IRepository<ImagesStore>.GetById(Guid id)
        {
            return new ImagesStore();
        }

        IList<ImagesStore> IRepository<ImagesStore>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ImagesStore));
                return criteria.List<ImagesStore>();
            }
        }

        public ImagesStore getImagesStoreById(long imagesStoreId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ImagesStore>().Add(Restrictions.Eq("ImagesStoreId", imagesStoreId)).UniqueResult<ImagesStore>();
        }

        #endregion
    }
}
