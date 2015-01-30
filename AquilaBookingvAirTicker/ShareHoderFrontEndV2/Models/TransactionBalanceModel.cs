using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class TransactionBalanceModel
    {
         #region Thuộc Tính
        public int ShareHolderId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string ShareSymbol
        {
            get;
            set;
        }
        public long OpenningBalance
        {
            get;
            set;
        }
        public long Credit
        {
            get;
            set;
        }
        public long Debit
        {
            get;
            set;
        }
        public long Balance
        {
            get;
            set;
        }
        public string SSN
        {
            get;
            set;
        }
        public string ShareHolderCode
        {
            get;
            set;
        }
        public DateTime DOB
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string Nationality
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Address2
        {
            get;
            set;
        }
        public DateTime IssueDate
        {
            get;
            set;
        }
        public string IssuePlace
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string BirthPlace
        {
            get;
            set;
        }
        public long Worth
        {
            get;
            set;
        }
        public long TotalQuantity
        {
            get;
            set;
        }

        public string NameGroup
        {
            get;
            set;
        }
    #endregion
    }
}