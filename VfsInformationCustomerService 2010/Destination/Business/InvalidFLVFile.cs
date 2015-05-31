using System;


namespace Vfs.WebCrawler.Destination.Utility
{
    public class InvalidFLVFile : Exception
    {
        public InvalidFLVFile() : base("Invalid FLV File")
        {
        }
    }
}
