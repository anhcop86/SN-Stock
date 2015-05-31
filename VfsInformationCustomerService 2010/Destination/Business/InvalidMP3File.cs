using System;

namespace Vfs.WebCrawler.Destination.Utility
{
    public class InvalidMP3File : Exception
    {
        public InvalidMP3File()
            : base("Invalid MP3 File")
        {
        }
    }
}
