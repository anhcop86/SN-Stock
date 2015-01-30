using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class ProvinceBase
    {
        #region Primitive Properties

        public virtual int ProvinceId
        {
            get;
            set;
        }   
        
        [Required]
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string EnglishName
        {
            get;
            set;
        }
        [Required]
        public virtual string CreatedDate
        {
            get;
            set;
        }
        [Required]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedBy
        {
            get;
            set;
        }

        public virtual IList<Hotel> ListHotel
        {
            get;
            set;
        }

        [Required]
        public virtual Country Country
        {
            get;
            set;
        }

        #endregion
    }
}
