using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain;
using NHibernate;
using NHibernate.Criterion;
using NTransform = NHibernate.Transform;


namespace ShareHolderCore.Domain.Repositories
{
    public class SubShareHoldersRepository : ISubShareHolders<ShareHolderHoldSymbol>
    {
        public IList<ShareHolderHoldSymbol> GetSubShareHolders(Int32 shareHolderId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery namedQuery = session.GetNamedQuery("spGetSubShareHolders");
                namedQuery.SetParameter("ShareHolderId", shareHolderId);

                namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(ShareHolderHoldSymbol)));
                return namedQuery.List<ShareHolderHoldSymbol>();
            }
        }

        public IList<ShareHolderHoldSymbol> GetSubShareHolders(Int32 shareHolderId, string addSelectAllOptionValue, string selectAllOptionText)
        {
          IList<ShareHolderHoldSymbol> returnValue = GetSubShareHolders(shareHolderId);
          ShareHolderHoldSymbol sh = new ShareHolderHoldSymbol();
          sh.Id = addSelectAllOptionValue;
          sh.ShareHolderCode= selectAllOptionText;
          returnValue.Insert(0, sh);
          return returnValue;  
        }
    }
}
