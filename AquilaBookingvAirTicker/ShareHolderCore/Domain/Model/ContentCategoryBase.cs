	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class ContentCategoryBase
	{
		
		#region Variable Declarations
        protected int _ContentCategoryId = 0;
		protected string _Name = string.Empty;
        protected string _Description = string.Empty;
        protected int _VerticalPosition =0;
        protected string _Type = string.Empty;
        protected string _URL = string.Empty;
        protected string _Display = string.Empty;
        protected int _ParentID = 0;
        protected int _LevelNumber = 0;
        protected string _DisplayInHomePage = string.Empty;
        protected string _FirstDisplay;
        protected int _DisplayOrder = 0;
        private Language language;
        protected IList<ContentItem> _listContentItem;
        //protected IList listShareHolder;

		#endregion
		
		#region Constructors
		public ContentCategoryBase() { }

        public ContentCategoryBase(
            int ContentCategoryId,
			string Name,
			string Description,
            int VerticalPosition,
            string Type,
			string URL)
		
		{
            this._ContentCategoryId = ContentCategoryId;
			this._Name = Name;
            this.Description = Description;
            this._VerticalPosition = VerticalPosition;
			this._Type = Type;
			this._URL = URL;
			
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
        public virtual int ContentCategoryId
		{
            get { return _ContentCategoryId; }
            set { _ContentCategoryId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Tên nhóm tin")]
        [Required(ErrorMessageResourceName = "ContentCategoryName", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(10, ErrorMessageResourceName = "ContentCategoryName", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 200)]
         public virtual string Name
         {
             get { return _Name; }
             set { _Name = value; }
         }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
        [Display(Name = "Mô tả")]
        public virtual string Description
		{
            get { return _Description; }
            set { _Description = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Vị trí")]
        public virtual int VerticalPosition
		{
            get { return _VerticalPosition; }
            set { _VerticalPosition = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Loại tin")]
        public virtual string Type
		{
            get { return _Type; }
            set { _Type = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Link URL")]
        public virtual string URL
		{
            get { return _URL; }
            set { _URL = value; }
		}

        [Display(Name = "Text hiển thị")]
        public virtual string Display
		{
            get { return _Display; }
            set { _Display = value; }
		}

        [Display(Name = "Mã ID cha")]
        public virtual int ParentID
		{
            get { return _ParentID; }
            set { _ParentID = value; }
		}

        [Display(Name = "Cấp độ nhóm tin")]
        public virtual int LevelNumber
        {
            get { return _LevelNumber; }
            set { _LevelNumber = value; }
        }

        [Display(Name = "Hiển thị trên trang home")]
        public virtual string DisplayInHomePage
        {
            get { return _DisplayInHomePage; }
            set { _DisplayInHomePage = value; }
        }

        [Display(Name = "Hiển thị đầu tiên")]
        public virtual string FirstDisplay
        {
            get { return _FirstDisplay; }
            set { _FirstDisplay = value; }
        }

        [Display(Name = "thứ tự hiển thị")]
        public virtual int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }

        public virtual Language Language
        {
            get { return language; }
            set { this.language = value; }
        }

        public virtual IList<ContentItem> ListContentItem
        {
            get { return this._listContentItem ;}
            set { _listContentItem = value; }
        }
             
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
	
	public enum ContentCategoryColumns
	{
        ContentCategoryId,
		Name,
		Description,
        VerticalPosition,
        Type,
        URL
	}//End enum
}