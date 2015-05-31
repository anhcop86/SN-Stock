using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace VfsInformationFeedService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;

        private System.ComponentModel.Container components = null;

        public ServiceInstaller()
        {
            InitializeComponent();
        }
    }
}