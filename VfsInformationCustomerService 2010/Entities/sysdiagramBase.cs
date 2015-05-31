	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Entities
{
	[Serializable]
	public class sysdiagramBase
	{
		
		#region Variable Declarations
		private string				_name = string.Empty;
		private int				_principal_id = 0;
		private int				_diagram_id = 0;
		private int				_version = 0;
		private byte[]				_definition = new byte[] { 0 };
		#endregion
		
		#region Constructors
		public sysdiagramBase() {}
		
		public sysdiagramBase (
			string name,
			int principal_id,
			int diagram_id,
			int version,
			byte[] definition)
		
		{
			this._name = name;
			this._principal_id = principal_id;
			this._diagram_id = diagram_id;
			this._version = version;
			this._definition = definition;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string name
		{
			get { return _name; }
			set { _name = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int principal_id
		{
			get { return _principal_id; }
			set { _principal_id = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int diagram_id
		{
			get { return _diagram_id; }
			set { _diagram_id = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int version
		{
			get { return _version; }
			set { _version = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varbinary</value>
		public byte[] definition
		{
			get { return _definition; }
			set { _definition = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum sysdiagramColumns
	{
		name,
		principal_id,
		diagram_id,
		version,
		definition
	}//End enum
}