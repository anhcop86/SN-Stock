using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace VfsInformationFeedService.Configuration
{
    public class ScheduleElement : ConfigurationElement
    {
        [ConfigurationProperty("IntervalMinutes", DefaultValue = 1)]
        public int IntervalMinutes
        {
            get
            {
                return (int)this["IntervalMinutes"];
            }
        }
    }
}
