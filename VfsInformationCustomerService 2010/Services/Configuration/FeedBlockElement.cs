using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace VfsInformationFeedService.Configuration
{
    public class FeedBlockElement: ConfigurationElement
    {
        [ConfigurationProperty("numberOfItem", DefaultValue = 10)]
        public int NumberOfItem 
        {
            get
            {
                return (int)this["numberOfItem"];
            }
        }
    }
}
