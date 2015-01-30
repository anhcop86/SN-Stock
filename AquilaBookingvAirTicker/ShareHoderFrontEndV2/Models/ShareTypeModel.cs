﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class ShareTypeModel
    {
        #region Properties
        [Required]
        [Display(Name = "ShareTypeId")]
        public int ShareTypeId { get; set; }


        [Display(Name = "Description")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Description { get; set; }

        public IList<TransactionDetailModel> ListTransactionDetail { get; set; }
        #endregion
    }
}