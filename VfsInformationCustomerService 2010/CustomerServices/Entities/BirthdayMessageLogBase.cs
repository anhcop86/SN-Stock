	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class BirthdayMessageLogBase
	{
		
		#region Variable Declarations
		private string				_BirthdayMessageDay = string.Empty;
		private string				_ProccessYN = string.Empty;
		#endregion
		
		#region Constructors
		public BirthdayMessageLogBase() {}
		
		public BirthdayMessageLogBase (
			string BirthdayMessageDay,
			string ProccessYN)
		
		{
			this._BirthdayMessageDay = BirthdayMessageDay;
			this._ProccessYN = ProccessYN;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BirthdayMessageDay
		{
			get { return _BirthdayMessageDay; }
			set { _BirthdayMessageDay = value; }
		}
		public string originalBirthdayMessageDay
		{
			get { return originalBirthdayMessageDay; }
			set { originalBirthdayMessageDay = value; }
		} 
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ProccessYN
		{
			get { return _ProccessYN; }
			set { _ProccessYN = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum BirthdayMessageLogColumns
	{
		BirthdayMessageDay,
		ProccessYN
	}//End enum
}