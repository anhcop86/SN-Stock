using System;


namespace VfsCustomerService.Utility
{
    public class InvalidFLVFile : Exception
    {
        public InvalidFLVFile() : base("Invalid FLV File")
        {
        }
    }
}
