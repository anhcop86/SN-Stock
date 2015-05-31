using System;


namespace Vfs.WebCrawler.Utility
{
    public class InvalidFLVFile : Exception
    {
        public InvalidFLVFile() : base("Invalid FLV File")
        {
        }
    }
}
