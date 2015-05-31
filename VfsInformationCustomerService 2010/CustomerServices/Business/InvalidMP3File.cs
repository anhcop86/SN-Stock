using System;

namespace VfsCustomerService.Utility
{
    public class InvalidMP3File : Exception
    {
        public InvalidMP3File()
            : base("Invalid MP3 File")
        {
        }
    }
}
