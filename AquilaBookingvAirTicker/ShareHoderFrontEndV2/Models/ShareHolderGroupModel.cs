using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class ShareHolderGroupModel
    {
          #region ProperTies
        //////////////////////////////////
        [Required]
        [Display(Name = "ShareHolderGroupId ")]
        public int ShareHolderGroupId
        {
            get;
            set;
        }
        ///////////////////////////////////
        [Required]
        [Display(Name = "Name ")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
         public string Name
         {
             get;
             set;
         }
        public IList<ShareHolderModel> ListShareHolder
        {
            get;
            set;
        }
        #endregion
    }
}