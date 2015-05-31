using System;
using System.Collections.Generic;
using System.Text;


namespace Vfs.WebCrawler.Destination.Utility
{
    public class InvalidImageTypeFile :Exception
    {
        public InvalidImageTypeFile()
            : base("Invalid image file") 
        {
        
        }
    }
}
