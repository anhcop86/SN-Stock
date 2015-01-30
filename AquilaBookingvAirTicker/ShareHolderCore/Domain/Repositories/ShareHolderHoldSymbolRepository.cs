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
    public class ShareHolderHoldSymbolRepository : IShareHolderHoldSymbol<ShareHolderHoldSymbol>
    {
        public IList<ShareHolderHoldSymbol> GetShareHolderHoldSymbol(string userId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery namedQuery = session.GetNamedQuery("spShareHolderShareSymbolGetByUserId");
                namedQuery.SetParameter("UserId", userId);

                namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(ShareHolderHoldSymbol)));
                return namedQuery.List<ShareHolderHoldSymbol>();
            }
        }

        public ShareHolderHoldSymbol GetShareHolderHoldSymbolByShareSymbol(string userId, string shareSymbol)
        {
            ShareHolderHoldSymbol  result =  new ShareHolderHoldSymbol();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery namedQuery = session.GetNamedQuery("spShareHolderShareSymbolGetByUserId");
                namedQuery.SetParameter("UserId", userId);

                namedQuery.SetResultTransformer(new NTransform.AliasToBeanResultTransformer(typeof(ShareHolderHoldSymbol)));
                IList<ShareHolderHoldSymbol> test = namedQuery.List<ShareHolderHoldSymbol>();
                foreach (ShareHolderHoldSymbol sh in test)
                {
                    if (sh.ShareHolderId.ToString() == userId && sh.ShareSymbol == shareSymbol)
                        result = sh;
                }
                return result;
            }
        }
    }
}
