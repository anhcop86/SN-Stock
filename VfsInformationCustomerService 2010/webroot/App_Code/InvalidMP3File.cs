using System;


namespace Vfs.WebCrawler.Utility
{
    public class InvalidMP3File : Exception
    {
        public InvalidMP3File()
            : base("Invalid MP3 File")
        {
        }
    }
}
