using System;
using System.Collections.Generic;
using System.Text;


namespace VfsCustomerService.Utility
{
    public class InvalidImageTypeFile :Exception
    {
        public InvalidImageTypeFile()
            : base("Invalid image file") 
        {
        
        }
    }
}
