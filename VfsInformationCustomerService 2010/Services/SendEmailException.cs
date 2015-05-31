using System;
using System.Collections.Generic;
using System.Text;

using VfsCustomerService.Entities;

namespace VfsInformationFeedService
{
    [global::System.Serializable]
    public class SendEmailException : ApplicationException
    {
        private MessageContent emailException;

        public SendEmailException() { }
        public SendEmailException(MessageContent email, Exception innerException)
            : base("", innerException)
        {
            emailException = email;
        }
        
        public SendEmailException(string message) : base(message) { }        

        protected SendEmailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }


        public MessageContent EmailException
        {
            get { return emailException; }
        }
    }
}
