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
    public class MembershipRepository : IMembershipRepository<Membership>
    {
        #region IRepository<membership> Members

        void IMembershipRepository<Membership>.Save(Membership entity)
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

        void IMembershipRepository<Membership>.Update(Membership entity)
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

        void IMembershipRepository<Membership>.Delete(Membership entity)
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

        Membership IMembershipRepository<Membership>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Membership>().Add(Restrictions.Eq("LoginId", id)).UniqueResult<Membership>();
        }

        Membership IMembershipRepository<Membership>.GetById(Guid id)
        {
            return null;
        }

        Membership IMembershipRepository<Membership>.GetByLoginId(string loginId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Membership>().Add(Restrictions.Eq("LoginId", loginId)).UniqueResult<Membership>();
        }

        Membership IMembershipRepository<Membership>.GetByLoginId(string loginId, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Membership));
                SimpleExpression exp1 = Expression.Eq("LoginId", loginId);
                SimpleExpression exp2 = Expression.Eq("Password", password);
                crit.Add(exp1);
                crit.Add(exp2);
                return crit.UniqueResult<Membership>();
            }
        }

        Membership IMembershipRepository<Membership>.GetByLoginId(string loginId, string password, string type)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Membership));
                SimpleExpression exp1 = Expression.Eq("LoginId", loginId);
                SimpleExpression exp2 = Expression.Eq("Password", password);
                SimpleExpression exp3 = Expression.Eq("Type", type);
                crit.Add(exp1);
                crit.Add(exp2);
                crit.Add(exp3);
                return crit.UniqueResult<Membership>();
            }
        }

        Membership IMembershipRepository<Membership>.GetByEmail(string email)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Membership>().Add(Restrictions.Eq("Email", email)).UniqueResult<Membership>();
        }

        IList<Membership> IMembershipRepository<Membership>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Membership));
                return criteria.List<Membership>();
            }
        }

        public bool ChangePassword(string loginId, string oldPassword, string newPassword)
        {
            bool result = true;
            Membership membership = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                 membership = session.CreateCriteria<Membership>().Add(Restrictions.Eq("LoginId", loginId)).UniqueResult<Membership>();
            }
            if (membership != null)
                result = false;
            if (membership.Password.Equals(oldPassword))
            {
                membership.Password = newPassword;
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(membership);
                        transaction.Commit();
                    }
                }
                result = true;
            }
            return result;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            bool result = true;
            Membership membership = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                membership = session.CreateCriteria<Membership>().Add(Restrictions.Eq("Email", email)).UniqueResult<Membership>();
            }
            if (membership != null)
                result = false;
            
                membership.Password = newPassword;
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(membership);
                        transaction.Commit();
                    }
                }
                result = true;
            
            return result;
        }

        public Membership GetByMemberId(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Membership>().Add(Restrictions.Eq("MemberId", id)).UniqueResult<Membership>();
        }
        #endregion
    }
}
