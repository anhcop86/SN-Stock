using System;
using System.Collections.Generic;
using System.Text;

namespace VfsInformationFeedService
{
    [global::System.Serializable]
    public class FeedInformationException : ApplicationException
    {
        private FeedHoseInformation feedHoseInformationException;

        public FeedInformationException() { }
        public FeedInformationException(FeedHoseInformation feedHoseInformation, Exception innerException)
            : base("", innerException)
        {
            feedHoseInformationException = feedHoseInformation;
        }

        public FeedInformationException(string message) : base(message) { }

        protected FeedInformationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }


        public FeedHoseInformation FeedHoseInformationException
        {
            get { return feedHoseInformationException; }
        }
    }

}
