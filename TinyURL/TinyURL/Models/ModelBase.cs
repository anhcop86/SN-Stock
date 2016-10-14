using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyURL.Models
{
    public class ModelBase<TResult,TData>: IDisposable
    {
        /// <summary>
        /// 데이터
        /// </summary>
        public TData Data { get; set; }

        public virtual void GetData()
        {
            
        }

           #region [Dispose]
        /// <summary>
        /// Dispose
        /// </summary>
        void IDisposable.Dispose()
        {

        }
        #endregion
    }
}