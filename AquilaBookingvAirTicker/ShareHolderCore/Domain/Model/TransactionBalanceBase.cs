using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class TransactionBalanceBase
    {
        #region Khai Báo Biến

        private int _ShareHolderId = 0;
        private string _ShareHolderCode = string.Empty;

        private string _Name = string.Empty;
        private string _Gender = string.Empty;
        private DateTime _DOB = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _BirthPlace = string.Empty;
        private string _SSN = string.Empty;
        private DateTime _IssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _IssuePlace = string.Empty;
        private string _Address = string.Empty;
        private string _Address2 = string.Empty;
        private string _Phone = string.Empty;
        private string _Nationality = string.Empty;

        private string _ShareSymbol = string.Empty;
        private string _Description = string.Empty;
        private DateTime _StartDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private long _OpenningBalance = 0;
        private long _Credit = 0;
        private long _Debit = 0;
        private long _Balance = 0;
        private long _Worth = 0;
        private long _TotalQuantity = 0;
        private string _NameGroup = string.Empty;

        #endregion

        #region Xay Dựng
        public TransactionBalanceBase () {}
        public TransactionBalanceBase
            (
        int ShareHolderID,
        string ShareHolderCode,

        string Name,
        string Gender,
        DateTime DOB,
        string BirthPlace,
        string SSN,
        DateTime IssueDate,
        string IssuePlace,
        string Address,
        string Address2,
        string Phone,
        string Nationality,

        string ShareSymbol,
        string Description,
        DateTime StartDate,
        long OpenningBalance,
        long Credit,
        long Debit,
        long Balance,
            long Worth,
            long TotalQuantity,
            string NameGroup


            )
        {
            this._ShareHolderId = ShareHolderID;
            this._ShareHolderCode = ShareHolderCode;
            this._Name = Name;
            this._Gender = Gender;
            this._DOB = DOB;
            this._BirthPlace = BirthPlace;
            this._SSN = SSN;
            this._IssueDate = IssueDate;
            this._IssuePlace = IssuePlace;
            this._Address = Address;
            this._Address2 = Address2;
            this._Phone = Phone;
            this._Nationality = Nationality;
            this._ShareSymbol = ShareSymbol;
            this._Description = Description;
            this._StartDate = StartDate;
            this._OpenningBalance = OpenningBalance;
            this._Credit = Credit;
            this._Debit = Debit;
            this._Balance = Balance;
            this._Worth = Worth;
            this._TotalQuantity = TotalQuantity;
            this._NameGroup = NameGroup;

        }

        #endregion

        #region Thuộc Tính
        public int ShareHolderId
        {
            get { return _ShareHolderId; }
            set { _ShareHolderId = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ShareSymbol
        {
            get { return _ShareSymbol; }
            set { _ShareSymbol = value; }
        }
        public long OpenningBalance
        {
            get { return _OpenningBalance; }
            set { _OpenningBalance = value; }
        }
        public long Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }
        public long Debit
        {
            get { return _Debit; }
            set { _Debit = value; }
        }
        public long Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }
        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }
        public string ShareHolderCode
        {
            get { return _ShareHolderCode; }
            set { _ShareHolderCode = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
         public string Address
         {
             get { return _Address; }
             set { _Address = value; }
         }
         public string  Address2
         {
             get { return _Address2; }
             set { _Address2 = value; }
         }
          public DateTime  IssueDate
          {
              get { return _IssueDate; }
              set { _IssueDate = value; }
          }
        public string IssuePlace
        {
            get { return _IssuePlace; }
            set { _IssuePlace = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string BirthPlace
        {
            get { return _BirthPlace; }
            set { _BirthPlace = value; }
        }
        public long Worth
        {
            get { return _Worth; }
            set { _Worth = value; }
        }
        public long TotalQuantity
        {
            get { return _TotalQuantity; }
            set { _TotalQuantity = value; }
        }        

        public string NameGroup
        {
            get { return _NameGroup; }
            set { _NameGroup = value; }
        }
	
        public enum TransactionBallanceCollum
        {
            ShareHolderID,
            Name ,
            ShareSymbol,
            OpenningBalance ,
            Credit,
            Debit ,
            Balance,
            SSN,
            ShareHolderCode,
            Phone,
            BirthPlace,
            DOB,
            Gender,
            Nationality,
            Address,
            Address2,
            IssueDate,
            IssuePlace,
            StartDate,
            Description,
            Worth,
            TotalQuantity,
            NameGroup
        }
        #endregion
    }
}
