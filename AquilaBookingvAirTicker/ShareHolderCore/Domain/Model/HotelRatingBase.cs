using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    public class HotelRatingBase
    {
        public virtual long RatingId { get; set; }

        public virtual int HotelId { get; set; }

        public virtual byte Type { get; set; }
        public virtual byte Rate { get; set; }
        
    }
}
