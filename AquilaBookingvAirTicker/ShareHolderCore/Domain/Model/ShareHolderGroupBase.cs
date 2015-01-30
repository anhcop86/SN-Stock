using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class ShareHolderGroupBase
    {
        #region Variable Decralation
        private int _ShareHolderGroupId = 0;
        private string _Name = string.Empty;
        private IList<ShareHolder> _listShareHolder;
        #endregion
        ///////////////////////////////////
        #region Constructor
        public ShareHolderGroupBase() { }

        public ShareHolderGroupBase(
                int ShareHolderGroupId,
                string Name)
        {
            this._ShareHolderGroupId = ShareHolderGroupId;
            this._Name = Name;
        }
        #endregion
        //////////////////////////////////
        #region ProperTies
        //////////////////////////////////
        public virtual int ShareHolderGroupId
        {
            get { return _ShareHolderGroupId; }
            set { _ShareHolderGroupId = value; }
        }
        ///////////////////////////////////
        [Display(Name = "Tên nhóm cổ đông")]
        public virtual string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
        public virtual IList<ShareHolder> ListShareHolder
        {
            get { return _listShareHolder; }
            set { _listShareHolder = value; }
        }
        #endregion

    }//end class
    public enum shareholderGroupColumns
    {
        ShareHolderGroupId,
        Name
    }//end enum
}
