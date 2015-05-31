/****** Object:  Stored Procedure [dbo].MessageContentSentGet    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateForMessageContentSent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateForMessageContentSent]
GO


CREATE PROCEDURE [dbo].spContentTemplateForMessageContentSent
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
	[ContentTemplateID] = @ContentTemplateID

GO