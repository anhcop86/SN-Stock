using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class CompareListDetailBase
    {
        #region Primitive Properties
        public CompareListDetailBase()
        {
           
        }

        public virtual long CompareListDetailId
        {
            get;
            set;
        }

        //public virtual Nullable<long> CompareListId
        //{
        //    get;
        //    set;            
        //}

      
        public virtual string CreatedDate
        {
            get;
            set;
        }

        public virtual string CreatedBy
        {
            get;
            set;
        }

        public virtual CompareList CompareList
        {
            get;
            set;
        }

        public virtual Hotel Hotel
        {
            get;
            set;                
        }
        
        #endregion
    }
}
