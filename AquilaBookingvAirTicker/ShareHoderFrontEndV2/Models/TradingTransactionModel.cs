using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ShareHolderCore.Domain.Model;

namespace ShareHoderFrontEndV2.Models
{
    public class TradingTransactionModel: TradingTransaction
    {
        #region Properties
        public string ShareHolderId { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }
     
        #endregion
    }
}