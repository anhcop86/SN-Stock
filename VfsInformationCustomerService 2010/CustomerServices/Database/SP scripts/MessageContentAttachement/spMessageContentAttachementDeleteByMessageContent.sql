 /****** Object:  Stored Procedure [dbo].MessageContentAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].spMessageContentAttachementDeleteByMessageContent') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementDeleteByMessageContent]
GO


CREATE PROCEDURE [dbo].spMessageContentAttachementDeleteByMessageContent
	@MessageContentID int
AS

DELETE FROM [dbo].[MessageContentAttachement]
WHERE
	[MessageContentID] = @MessageContentID
GO
	
	