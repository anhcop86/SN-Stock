	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class ContentItemBase
	{
		
		#region Variable Declarations
		private int				_ContentItemId = 0;
		private string				_Title = string.Empty;
		private string				_Content = string.Empty;
		private string				_LastUpdate = string.Empty;
		private Int16				_VerticalPosition = 0;
		private Int32				_UserID =0;
        private string _Display = string.Empty;
        private string _SiteHome = string.Empty; 
        private string _StartPub = string.Empty; 
        private string _EndPub = string.Empty;   
        private int _TemplateID = 0;   
        private string _ObjectCode = string.Empty;      
        private string _MetaTagKeyWord = string.Empty; 
        private string _MetaTagDescription = string.Empty;    
        private string _Type = string.Empty;     
        private string _Picture = string.Empty;    
        private string _NewHeadLine = string.Empty;    
        private string _NewDate = string.Empty;     
        private string _EventLocation = string.Empty;     
        private string _EventTime = string.Empty;    
            
       // protected IList<Bank> _listBank;
		//protected IList listShareHolder;

		#endregion
		
		#region Constructors
		public ContentItemBase() {  }

        public ContentItemBase(
            int _ContentItemId,
            string _Title,
			string _Content,
            string _LastUpdate,
            string _MetaTagKeyWord,
            string _MetaTagDescription)
		
		{
            this._ContentItemId = _ContentItemId;
            this._Title = _Title;
            this._Content = _Content;
            this._LastUpdate = _LastUpdate;
            this._MetaTagDescription = _MetaTagKeyWord;
            this._MetaTagDescription = _MetaTagDescription;
			
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
        public virtual int ContentItemId
		{
            get { return _ContentItemId; }
            set { _ContentItemId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Tieu de")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(10, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string Title
         {
             get { return _Title; }
             set { _Title = value; }
         }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
        [Display(Name = "Địa chỉ")]
        public virtual string Content
		{
            get { return _Content; }
            set { _Content = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Số điện thoại")]
        public virtual string LastUpdate
		{
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Website")]
        public virtual Int16 VerticalPosition
		{
            get { return _VerticalPosition; }
            set { _VerticalPosition = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Số fax")]
        public virtual int UserID
		{
            get { return _UserID; }
            set { _UserID = value; }
		}

        public virtual string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }

        public virtual string SiteHome
        {
            get { return _SiteHome; }
            set { _SiteHome = value; }
        }

        public virtual string StartPub
        {
            get { return _StartPub; }
            set { _StartPub = value; }
        }

        public virtual string EndPub
        {
            get { return _EndPub; }
            set { _EndPub = value; }
        }

        public virtual int TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        public virtual string ObjectCode
        {
            get { return _ObjectCode; }
            set { _ObjectCode = value; }
        }

        public virtual string MetaTagKeyWord
        {
            get { return _MetaTagKeyWord; }
            set { _MetaTagKeyWord = value; }
        }

        public virtual string MetaTagDescription
        {
            get { return _MetaTagDescription; }
            set { _MetaTagDescription = value; }
        }

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public virtual string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        public virtual string NewHeadLine
        {
            get { return _NewHeadLine; }
            set { _NewHeadLine = value; }
        }

        public virtual string NewDate
        {
            get { return _NewDate; }
            set { _NewDate = value; }
        }

        public virtual string EventLocation
        {
            get { return _EventLocation; }
            set { _EventLocation = value; }
        }

        public virtual string EventTime
        {
            get { return _EventTime; }
            set { _EventTime = value; }
        }

        public virtual ContentCategory ContentCategory
        {
            get;
            set;
        }

        public virtual Language Language
        {
            get;
            set;
        }
       /* public virtual IList<Bank> ListBank
        {
            get { return _listBank ;}
            set { _listBank = value; }
        }
             */
		/// <summary>
		/// 
		/// </summary>		
		//public virtual IList ListShareHolder
		//{
		//    get
		//    {
		//        if (listShareHolder == null)
		//        {
		//            listShareHolder = new ArrayList();
		//        }
		//        return listShareHolder;
		//    }
		//    set { listShareHolder = value; }
		//} 
		#endregion
	}//End Class
	
	
}