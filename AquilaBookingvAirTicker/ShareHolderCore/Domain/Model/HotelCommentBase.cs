using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
 
    public class HotelCommentBase
    {
        public virtual long CommentId { get; set; }

        public virtual int HotelId { get; set; }

        public virtual byte Rate { get; set; }

        public virtual string comment { get; set; }

        public virtual int  MemberId { get; set; }

        public virtual DateTime CommentDate { get; set; }
    }
}
