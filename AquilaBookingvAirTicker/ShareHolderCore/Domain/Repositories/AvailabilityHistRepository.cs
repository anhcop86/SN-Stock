using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class AvailabilityHistRepository : IRepository<AvailabilityHist>
    {
        #region IRepository<AvailabilityHistHist> Members

        void IRepository<AvailabilityHist>.Save(AvailabilityHist entity)
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

        void IRepository<AvailabilityHist>.Update(AvailabilityHist entity)
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

        void IRepository<AvailabilityHist>.Delete(AvailabilityHist entity)
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

        AvailabilityHist IRepository<AvailabilityHist>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<AvailabilityHist>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<AvailabilityHist>();
        }

        AvailabilityHist IRepository<AvailabilityHist>.GetById(Guid id)
        {
            return new AvailabilityHist();
        }

        IList<AvailabilityHist> IRepository<AvailabilityHist>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(AvailabilityHist));
                return criteria.List<AvailabilityHist>();
            }
        }

        #endregion
    }
}
