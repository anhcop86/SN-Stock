/****** Object:  Stored Procedure [dbo].MessageContentGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateForMessageContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateForMessageContent]
GO

	

CREATE PROCEDURE [dbo].spContentTemplateForMessageContent
	@ContentTemplateID int
AS

SELECT TOP 1
	[MessageContentID],
	[ContentTemplateID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate],
	[Status],
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]
FROM
	[dbo].[MessageContent]
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
	
