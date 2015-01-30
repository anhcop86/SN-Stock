using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace ShareHolderCore.Domain.Repositories
{
    public class CompareListDetailRepository :  ICompareListDetailRepository<CompareListDetail>,IRepository<CompareListDetail>
    {
        #region CompareListDetail 

        void IRepository<CompareListDetail>.Save(CompareListDetail entity)
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

        void IRepository<CompareListDetail>.Update(CompareListDetail entity)
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

        void IRepository<CompareListDetail>.Delete(CompareListDetail entity)
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

        CompareListDetail IRepository<CompareListDetail>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<CompareListDetail>().Add(Restrictions.Eq("CompareListDetailId", id)).UniqueResult<CompareListDetail>();
        }

        CompareListDetail IRepository<CompareListDetail>.GetById(Guid id)
        {
            return new CompareListDetail();
        }



        IList<CompareListDetail> IRepository<CompareListDetail>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(CompareListDetail));
                return criteria.List<CompareListDetail>();
            }
        }

      

        #endregion

        public IList<CompareListDetail> GetListCompareListDetailFromMember(int idMember)
        {
            StringBuilder stringQuery = new StringBuilder();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //stringQuery.Append("SELECT TOP 3 cd.[CompareListDetailId],cd.[CompareListId],cd.[HotelId] ,cd.[CreatedDate] ,cd.[CreatedBy] ");
                //stringQuery.Append(" FROM [CompareListDetail] cd, [CompareList] cl ");
                //stringQuery.Append(" WHERE cd.[CompareListId] = cl.[CompareListId]  and cl.MemberId = :idMember");


                //IQuery query = session.CreateQuery("SELECT cld.CompareListDetailId, cld.CompareListId,cld.HotelId, cld.CreatedDate, cld.CreatedBy   FROM CompareListDetail cld INNER JOIN  cld.CompareList  ");

                IQuery query = session.CreateQuery("SELECT cld FROM CompareListDetail cld, CompareList cl WHERE cld.CompareList.CompareListId = cl.CompareListId And  cl.Membership.MemberId = :idMember ");

                
                // set parameter

                query.SetInt32("idMember", idMember);

                IList<CompareListDetail> list = query.List<CompareListDetail>();

                
                

                return list;
            }
        }
    }
}
