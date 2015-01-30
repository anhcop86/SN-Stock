using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace AquilaAd.Models
{
    public class FacilityModel
    {
        public virtual Int16 FacilityId { get; set; }

        [Display(Name = "Tên dịch vụ")]
        [StringLength(255, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string Name { get; set; }

        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedDate { get; set; }

        [Display(Name = "Người tạo")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedBy { get; set; }        

    }
    public enum FacilityModelColumns
    {
        FacilityId,
        Name,
        CreatedDate,
        CreatedBy
    }// end enum


}