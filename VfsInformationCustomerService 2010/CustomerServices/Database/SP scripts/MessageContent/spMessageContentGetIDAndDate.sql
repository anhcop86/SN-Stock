/****** Object:  Stored Procedure [dbo].MessageContentGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGetIDAndDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spMessageContentGetIDAndDate
GO	
	
CREATE PROCEDURE [dbo].spMessageContentGetIDAndDate
	@MessageContentID int,
	@ModifiedDate datetime
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
	[MessageContentID] = @MessageContentID
And convert(nvarchar(10),[ModifiedDate],112) =  convert(nvarchar(10),@ModifiedDate,112)

GO	
 