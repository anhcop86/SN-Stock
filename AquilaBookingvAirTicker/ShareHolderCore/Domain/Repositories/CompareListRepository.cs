using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class CompareListRepository : IRepository<CompareList>, ICompareListRepository
    {

        #region CompareList Members

        void IRepository<CompareList>.Save(CompareList entity)
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

        void IRepository<CompareList>.Update(CompareList entity)
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

        void IRepository<CompareList>.Delete(CompareList entity)
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

        CompareList IRepository<CompareList>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<CompareList>().Add(Restrictions.Eq("CompanyId", id)).UniqueResult<CompareList>();
        }

        CompareList IRepository<CompareList>.GetById(Guid id)
        {
            return new CompareList();
        }

        IList<CompareList> IRepository<CompareList>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(CompareList));
                return criteria.List<CompareList>();
            }
        }

        public CompareList getCompareListfromMemberId(int memberid)
        {
            StringBuilder stringQuery = new StringBuilder();
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IQuery query = session.CreateQuery("SELECT obj  FROM CompareList obj Where  obj.Membership.MemberId = :memberid ");

                query.SetInt32("memberid", memberid);

                CompareList CompareList = query.UniqueResult<CompareList>();


                return CompareList;
            }
        }
        #endregion
    }
}
