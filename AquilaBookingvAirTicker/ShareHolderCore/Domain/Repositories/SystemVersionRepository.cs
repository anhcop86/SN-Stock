using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class SystemVersionRepository: IRepository<SystemVersion>
    {
        #region SystemVersion Members

        void IRepository<SystemVersion>.Save(SystemVersion entity)
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

        void IRepository<SystemVersion>.Update(SystemVersion entity)
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

        void IRepository<SystemVersion>.Delete(SystemVersion entity)
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

        SystemVersion IRepository<SystemVersion>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<SystemVersion>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<SystemVersion>();
        }

        SystemVersion IRepository<SystemVersion>.GetById(Guid id)
        {
            return new SystemVersion();
        }

        IList<SystemVersion> IRepository<SystemVersion>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(SystemVersion));
                return criteria.List<SystemVersion>();
            }
        }

        #endregion
    }
}
