using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace VfsInformationFeedService
{
    public partial class InformationFeedService : ServiceBase
    {
        private IMainThread feedNewsThread;

        public InformationFeedService()
        {
            InitializeComponent();
            feedNewsThread = new FeedHoseInformation();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            this.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            feedNewsThread.Stop();
        }

        public void Start()
        {
            feedNewsThread.Start();
        }
    }
}
