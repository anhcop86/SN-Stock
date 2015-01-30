using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class HotelFacilityBase
    {
        public virtual Int32 HotelFacilityId { get; set; }
        public virtual Int32 HotelId { get; set; }


        public virtual Int16 FacilityId { get; set; }

        [Display(Name = "Tên dịch vụ")]        
        [StringLength(255, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string Name { get; set; }

        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedDate { get; set; }

        [Display(Name = "Ngày sửa")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedBy { get; set; }

        public virtual Facility Facility { get; set; }


        public virtual Hotel Hotel
        {
            get;
            set;
        }
    }
    public enum HotelFacilityColumns
    {
        FacilityId,
        Name,
        CreatedDate,
        CreatedBy
    }// end enum
}
