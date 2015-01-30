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
    public class ShareTypeRepository: IRepository<ShareType>
    {
        #region IRepository<Company> Members

        void IRepository<ShareType>.Save(ShareType entity)
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

        void IRepository<ShareType>.Update(ShareType entity)
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

        void IRepository<ShareType>.Delete(ShareType entity)
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

        ShareType IRepository<ShareType>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<ShareType>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<ShareType>();
        }

        ShareType IRepository<ShareType>.GetById(Guid id)
        {
            return new ShareType();
        }

        IList<ShareType> IRepository<ShareType>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ShareType));
                return criteria.List<ShareType>();
            }
        }

        #endregion
    }
}
