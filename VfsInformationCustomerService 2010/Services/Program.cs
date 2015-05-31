using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using VfsInformationFeedService;

namespace VfsInformationFeedService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            //InformationFeedService informationFeedService = new InformationFeedService();
            //if (args.Length > 0 && args[0] == "-FeedInformation")
            //{
            //    informationFeedService.Start();
            //}
            //else
            //{
                ServicesToRun = new ServiceBase[] { new InformationFeedService() };
                ServiceBase.Run(ServicesToRun);
            //}
        }
    }
}