using NHibernate;
using StockApiReatime.Nhibernate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApiReatime.Nhibernate.Repositoies
{
    public class IPProxyRepository : IRepository<IPProxy>
    {
        #region IRepository<Company> Members

        void IRepository<IPProxy>.Save(IPProxy entity)
        {
            using (ISession session = NHIbernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<IPProxy>.Update(IPProxy entity)
        {
            using (ISession session = NHIbernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<IPProxy>.Delete(IPProxy entity)
        {
            using (ISession session = NHIbernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }


        IPProxy IRepository<IPProxy>.GetById(object id)
        {
            return new IPProxy();
        }

        IList<IPProxy> IRepository<IPProxy>.GetAll()
        {
            using (ISession session = NHIbernateSession.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(IPProxy));
                return criteria.List<IPProxy>();
            }
        }

        #endregion
    }
}