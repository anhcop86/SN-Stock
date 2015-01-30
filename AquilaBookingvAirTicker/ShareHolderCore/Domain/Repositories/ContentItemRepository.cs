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
    public class ContentItemRepository : IRepository<ContentItem>
    {
        #region IRepository<ContentItemRepository> Members
        void IRepository<ContentItem>.Save(ContentItem entity)
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

        void IRepository<ContentItem>.Update(ContentItem entity)
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

        void IRepository<ContentItem>.Delete(ContentItem entity)
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

        ContentItem IRepository<ContentItem>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ContentItem>().Add(Restrictions.Eq("ShareHolderId", id)).UniqueResult<ContentItem>();
        }

        ContentItem IRepository<ContentItem>.GetById(Guid id)
        {
            return new ContentItem();
        }

        IList<ContentItem> IRepository<ContentItem>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ContentItem));
                return criteria.List<ContentItem>();
            }
        }
        #endregion
    }
}
