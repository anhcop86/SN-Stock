/****** Object:  Stored Procedure [dbo].MessageContentSentGet    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeForMessageContentSent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeForMessageContentSent]
GO


CREATE PROCEDURE [dbo].spServiceTypeForMessageContentSent
	@ServiceTypeID int
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
	[ServiceTypeID] = @ServiceTypeID

GO
	
