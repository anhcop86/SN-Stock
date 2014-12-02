using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class AGNews_ArticlesBase
    {        

        public int ArticleId { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Lead { get; set; }

        public string Content { get; set; }

        public string Source { get; set; }

        public string ImageFile { get; set; }

        public string ImageNote { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
