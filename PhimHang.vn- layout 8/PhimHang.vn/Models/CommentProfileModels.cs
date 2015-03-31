using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class CommentProfileModels
    {
        public string Message { get; set; }

        public string PostedByName { get; set; }

        public string PostedByAvatar { get; set; }

        public DateTime PostedDate { get; set; }

        public long PostId { get; set; }
    }
}