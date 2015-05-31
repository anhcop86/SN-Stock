using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace VfsInformationFeedService.Configuration
{
    public class ServiceSection : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("schedulde")]
        public ScheduleElement FeedInterval
        {
            get
            {
                return (ScheduleElement)this["schedulde"];
            }
        }

        [ConfigurationProperty("feedBlock")]
        public FeedBlockElement FeedBlock
        {
            get
            {
                return (FeedBlockElement)this["feedBlock"];
            }
        }       
    }
}
