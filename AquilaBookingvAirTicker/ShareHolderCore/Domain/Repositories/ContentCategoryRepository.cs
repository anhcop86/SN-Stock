using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain;

namespace ShareHolderCore.Domain.Repositories
{
    public class ContentCategoryRepository : IRepository<ContentCategory>
    {
        #region IRepository<ContentItemRepository> Members
        void IRepository<ContentCategory>.Save(ContentCategory entity)
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

        void IRepository<ContentCategory>.Update(ContentCategory entity)
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

        void IRepository<ContentCategory>.Delete(ContentCategory entity)
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

        ContentCategory IRepository<ContentCategory>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ContentCategory>().Add(Restrictions.Eq("ShareHolderId", id)).UniqueResult<ContentCategory>();
        }

        ContentCategory IRepository<ContentCategory>.GetById(Guid id)
        {
            return new ContentCategory();
        }

        IList<ContentCategory> IRepository<ContentCategory>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ContentCategory));
                return criteria.List<ContentCategory>();
            }
        }
        #endregion
    }
}
