using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]

    public class CountryBase
    {
        #region Primitive Properties

        public virtual short CountryId
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

        [Required]
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

        [Display(Name = "Người tạo")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedBy
        {
            get;
            set;
        }

        public virtual IList<Hotel> ListProvince
        {
            get;
            set;
        }
        #endregion
    }
}
