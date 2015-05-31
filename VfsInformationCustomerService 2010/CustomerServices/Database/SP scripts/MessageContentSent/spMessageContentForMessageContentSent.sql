/****** Object:  Stored Procedure [dbo].MessageContentSentGet    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentForMessageContentSent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentForMessageContentSent]
GO


CREATE PROCEDURE [dbo].spMessageContentForMessageContentSent
	@MessageContentID int
AS

SELECT
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
	[MessageContentSentID],
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]
FROM
	[dbo].[MessageContentSent]
WHERE
	[MessageContentID] = @MessageContentID

GO
	