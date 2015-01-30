using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories           
{
    public class ShareHolderRepository : IRepository<ShareHolder>
    {
        #region IRepository<ShareHolder> Members

        void IRepository<ShareHolder>.Save(ShareHolder entity)
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

        void IRepository<ShareHolder>.Update(ShareHolder entity)
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

        void IRepository<ShareHolder>.Delete(ShareHolder entity)
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

        ShareHolder IRepository<ShareHolder>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ShareHolder>().Add(Restrictions.Eq("ShareHolderId", id)).UniqueResult<ShareHolder>();
        }

        ShareHolder IRepository<ShareHolder>.GetById(Guid id)
        {
            return new ShareHolder();
        }

        IList<ShareHolder> IRepository<ShareHolder>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ShareHolder));
                return criteria.List<ShareHolder>();
            }
        }

        #endregion
    }
}
