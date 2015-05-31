	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class RelatedMessagelogBase
	{
		
		#region Variable Declarations
		private long				_RelatedMessagelogID = 0;
		private long				_NewsID = 0;
		#endregion
		
		#region Constructors
		public RelatedMessagelogBase() {}
		
		public RelatedMessagelogBase (
			long RelatedMessagelogID,
			long NewsID,
            string CustomerID)
		
		{
			this._RelatedMessagelogID = RelatedMessagelogID;
			this._NewsID = NewsID;
            this._CustomerID = CustomerID;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
		public long RelatedMessagelogID
		{
			get { return _RelatedMessagelogID; }
			set { _RelatedMessagelogID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
		public long NewsID
		{
			get { return _NewsID; }
			set { _NewsID = value; }
		}
	
		
		#endregion
        
        private string _CustomerID;

        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
	
	}//End Class
	
	public enum RelatedMessagelogColumns
	{
		RelatedMessagelogID,
		NewsID,
        CustomerID
	}//End enum
}