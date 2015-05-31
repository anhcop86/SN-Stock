using System;
using System.Collections.Generic;
using System.Text;

namespace VfsInformationFeedService
{
    public class SendMessage
    {
        protected IList<Exception> listError;
        protected DateTime startSendEmailTime;
        protected DateTime finishSendEmailTime;
        protected int numberOfEmails;
        protected string userName;
        protected string password;

        protected int commandBlockSize;

        protected enum EmailCommandStatus
        {
            NotStart,
            SuccessAndFinish,
            FailAndFinish
        }

        #region Property
        public DateTime StartSendEmailTime
        {
            get { return startSendEmailTime; }
        }

        public DateTime FinishSendEmailTime
        {
            get { return finishSendEmailTime; }
        }

        public IList<Exception> ListError
        {
            get { return listError; }
        }

        public int NumberOfEmails
        {
            get { return numberOfEmails; }
        }

        public int CommandBlockSize
        {
            get { return commandBlockSize; }
        }

        public string UserName
        {
            set { this.userName = value; }
            get { return userName; }
        }

        public string Password
        {
            set { this.password = value; }
            get { return password; }
        }
        #endregion

        public SendMessage()
        {            
            this.Initialize();
        }

        public SendMessage(int commandBlockSize)
        {
            this.commandBlockSize = commandBlockSize;            
            this.Initialize();
        }

        public SendMessage(int commandBlockSize, string userName, string password)
        {            
            this.commandBlockSize = commandBlockSize;
            this.userName = userName;
            this.password = password;
            this.Initialize();
        }

        protected void Initialize()
        {
            listError = new List<Exception>();
            startSendEmailTime = DateTime.Now;
            finishSendEmailTime = DateTime.MaxValue;
            numberOfEmails = 0;
        }     
    }
}
