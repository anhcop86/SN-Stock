if exists (select * from dbo.sysobjects where id = object_id(N'[spExistServiceTypeIdForMessageContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spExistServiceTypeIdForMessageContent
GO
	
CREATE PROCEDURE spExistServiceTypeIdForMessageContent
@ServiceTypeID int 
AS

SELECT 
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
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]
FROM MessageContent
WHERE ServiceTypeID = @ServiceTypeID

GO