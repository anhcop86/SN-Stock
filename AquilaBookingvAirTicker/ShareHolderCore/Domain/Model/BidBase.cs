using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class BidBase
    {

        #region Variable Declarations
        private Guid _ID = Guid.Empty;
        private DateTime _BidDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private int _ShareHolderId = 0;
        private string _ShareSymbol = string.Empty;
        private long _Quantity = 0;
        private decimal _Price = 0;
        private string _Status = string.Empty;
        private DateTime _ExpiredDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private DateTime _CreateDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private IList<Bid> _listBid;
        #endregion

        #region Constructors
        public BidBase() { }

        public BidBase(
            Guid ID,
            DateTime BidDate,
            int ShareHolderId,
            string ShareSymbol,
            long Quantity,
            decimal Price,
            string Status,
            DateTime ExpiredDate,
            DateTime CreateDate)
        {
            this._ID = ID;
            this._BidDate = BidDate;
            this._ShareHolderId = ShareHolderId;
            this._ShareSymbol = ShareSymbol;
            this._Quantity = Quantity;
            this._Price = Price;
            this._Status = Status;
            this._ExpiredDate = ExpiredDate;
            this._CreateDate = CreateDate;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is uniqueidentifier</value>
        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
       

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Required(ErrorMessageResourceName = "dataTime", ErrorMessageResourceType = typeof(ValidationMessage))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày")]
        public virtual DateTime BidDate
        {
            get { return _BidDate; }
            set { _BidDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        [Required(ErrorMessageResourceName = "shareHolderId", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Mã cổ đông")]
        public virtual int ShareHolderId
        {
            get { return _ShareHolderId; }
            set { _ShareHolderId = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        
        /// <value>This type is nvarchar</value>
        [Required(ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(255, ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        [Display(Name = "Mã cổ phiếu")]
        public virtual string ShareSymbol
        {
            get { return _ShareSymbol; }
            set { _ShareSymbol = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is bigint</value>


        
        [Display(Name = "Khối lượng")]
        public virtual long Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is decimal</value>
        /// 
        [Display(Name = "Giá")]
        public virtual decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        
        [Required(ErrorMessageResourceName = "status", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(1, ErrorMessageResourceName = "status", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 1)]
        [Display(Name = "Tình trạng")]
        public virtual string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        /// 
        [Display(Name = "Ngày hết hạn")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        /// 
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual IList<Bid> ListBid
        {
            get { return _listBid; }
            set { _listBid = value; }
        }
        #endregion


    }
    public enum BidColumns
    {
        ID,
        BidDate,
        ShareHolderId,
        ShareSymbol,
        Quantity,
        Price,
        Status,
        ExpiredDate,
        CreateDate
    }//End enum
}
