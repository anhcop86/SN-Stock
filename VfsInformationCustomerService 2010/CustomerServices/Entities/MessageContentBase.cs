	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class MessageContentBase
	{
		
		#region Variable Declarations
		private int				_MessageContentID = 0;
		private int				_ContentTemplateID = 0;
		private int				_ServiceTypeID = 0;
		private string				_Sender = string.Empty;
		private string				_Receiver = string.Empty;
		private string				_Subject = string.Empty;
		private string				_BodyContentType = string.Empty;
		private string				_BodyEncoding = string.Empty;
		private string				_BodyMessage = string.Empty;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
        private int _Status = 0;
        private string _ServiceID = string.Empty;
        private string _CommandCode = string.Empty;
        private string _Request = string.Empty;
        private string _MoID = string.Empty;
        private string _ChargeYN = string.Empty;
        private short _TotalMessages = 0;
		#endregion
		
		#region Constructors
		public MessageContentBase() {}
		
		public MessageContentBase (
			int MessageContentID,
			int ContentTemplateID,
			int ServiceTypeID,
			string Sender,
			string Receiver,
			string Subject,
			string BodyContentType,
			string BodyEncoding,
			string BodyMessage,
			DateTime CreatedDate,
			DateTime ModifiedDate,
            int Status,
            string ServiceID,
            string CommandCode,
            string Request,
            string MoID,
            string ChargeYN,
            short TotalMessages)
		
		{
			this._MessageContentID = MessageContentID;
			this._ContentTemplateID = ContentTemplateID;
			this._ServiceTypeID = ServiceTypeID;
			this._Sender = Sender;
			this._Receiver = Receiver;
			this._Subject = Subject;
			this._BodyContentType = BodyContentType;
			this._BodyEncoding = BodyEncoding;
			this._BodyMessage = BodyMessage;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
            this._Status = Status;
            this._ServiceID = ServiceID;
            this._CommandCode = CommandCode;
            this._Request = Request;
            this._MoID = MoID;
            this._ChargeYN = ChargeYN;
            this._TotalMessages = TotalMessages;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int MessageContentID
		{
			get { return _MessageContentID; }
			set { _MessageContentID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ContentTemplateID
		{
			get { return _ContentTemplateID; }
			set { _ContentTemplateID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ServiceTypeID
		{
			get { return _ServiceTypeID; }
			set { _ServiceTypeID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Sender
		{
			get { return _Sender; }
			set { _Sender = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Receiver
		{
			get { return _Receiver; }
			set { _Receiver = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyContentType
		{
			get { return _BodyContentType; }
			set { _BodyContentType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyEncoding
		{
			get { return _BodyEncoding; }
			set { _BodyEncoding = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyMessage
		{
			get { return _BodyMessage; }
			set { _BodyMessage = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime ModifiedDate
		{
			get { return _ModifiedDate; }
			set { _ModifiedDate = value; }
		}

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }

        public string CommandCode
        {
            get { return _CommandCode; }
            set { _CommandCode = value; }
        }
        public string Request
        {
            get { return _Request; }
            set { _Request = value; }
        }
        public string MoID
        {
            get { return _MoID; }
            set { _MoID = value; }
        }
        public string ChargeYN
        {
            get { return _ChargeYN; }
            set { _ChargeYN = value; }
        }
        public short TotalMessages
        {
            get { return _TotalMessages; }
            set { _TotalMessages = value; }
        }
		#endregion
	}//End Class
	
	public enum MessageContentColumns
	{
		MessageContentID,
		ContentTemplateID,
		ServiceTypeID,
		Sender,
		Receiver,
		Subject,
		BodyContentType,
		BodyEncoding,
		BodyMessage,
		CreatedDate,
		ModifiedDate,
        Status,
        ServiceID,
        CommandCode,
        Request,
        MoID,
        ChargeYN,
        TotalMessages
	}//End enum
}