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
    public class CompanyRepository : IRepository<Company>
    {
        #region IRepository<Company> Members

        void IRepository<Company>.Save(Company entity)
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

        void IRepository<Company>.Update(Company entity)
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

        void IRepository<Company>.Delete(Company entity)
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

        Company IRepository<Company>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Company>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<Company>();
        }

        Company IRepository<Company>.GetById(Guid id)
        {
            return new Company();
        }

        IList<Company> IRepository<Company>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Company));
                return criteria.List<Company>();
            }
        }

        #endregion
    }
}
