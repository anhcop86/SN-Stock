using System;
using System.Collections.Generic;
using System.Text;

namespace VfsCustomerInformationServices
{
    public interface IInformationFeedHoseSession
    {
        void FeedNewsList();
        void FeedNewsItem(string listUrl, string shortUrl, int linkId);
    }
}
