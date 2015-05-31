using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace VfsInformationFeedService.Configuration
{
    public class VfsInformationFeedServiceConfiguration
    {
        private static ServiceSection serviceSection;

        public static ServiceSection FeedServiceSection
        {
            get
            {
                if (serviceSection == null)
                {
                    Initialize();
                }
                return serviceSection;
            }
        }

        private static void Initialize()
        {
            try
            {
                serviceSection = (ServiceSection)ConfigurationManager.GetSection("vfs.informationFeedService");
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
            }
        }
    }
}
