 /****** Object:  Stored Procedure [dbo].MessageContentAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentForMessageContentAttachement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentForMessageContentAttachement]
GO

	
CREATE PROCEDURE [dbo].spMessageContentForMessageContentAttachement
	@MessageContentID int
AS

SELECT
	[MessageContentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageContentAttachement]
WHERE
	[MessageContentID] = @MessageContentID
GO
	

	
