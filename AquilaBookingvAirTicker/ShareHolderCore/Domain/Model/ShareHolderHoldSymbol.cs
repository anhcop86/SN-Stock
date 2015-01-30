using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  ShareHolderCore.Domain.Model
{
	public class ShareHolderHoldSymbol
	{
		protected int _ShareHolderId = 0;
		protected string _ShareSymbol = string.Empty;
    protected string _id = string.Empty;
    protected string _shareHolderCode = string.Empty;
    protected string _shareHolderType = string.Empty;

    public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

		public virtual int ShareHolderId
		{
			get { return _ShareHolderId; }
			set { _ShareHolderId = value; }
		}

    public virtual string ShareSymbol
        {
            get { return _ShareSymbol; }
            set { _ShareSymbol = value; }
        }

    public virtual string ShareHolderCode
    {
        get { return _shareHolderCode; }
        set { _shareHolderCode = value; }
    }

    public virtual string ShareHolderType
    {
        get { return _shareHolderType; }
        set { _shareHolderType = value; }
    }   
       
	}
}
