using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class SystemParameterRepository: IRepository<SystemParameter>
    {
        #region SystemParameter Members

        void IRepository<SystemParameter>.Save(SystemParameter entity)
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

        void IRepository<SystemParameter>.Update(SystemParameter entity)
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

        void IRepository<SystemParameter>.Delete(SystemParameter entity)
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

        SystemParameter IRepository<SystemParameter>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<SystemParameter>().Add(Restrictions.Eq("ParameterId", id)).UniqueResult<SystemParameter>();
        }

        SystemParameter IRepository<SystemParameter>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<SystemParameter>().Add(Restrictions.Eq("ParameterId", id)).UniqueResult<SystemParameter>();
        }

        IList<SystemParameter> IRepository<SystemParameter>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(SystemParameter));
                return criteria.List<SystemParameter>();
            }
        }

        #endregion
    }
}
