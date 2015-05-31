	
/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementUpdate    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementUpdate
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: CodeSmith
**		Date: 09/06/2009 16:46:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentAttachementUpdate
	@MessageContentSentAttachementID int, 
	@AttachementDocument nvarchar(1024), 
	@AttachementDescription nvarchar(500), 
	@MessageContentID int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[MessageContentSentAttachement] SET
	[AttachementDocument] = @AttachementDocument,
	[AttachementDescription] = @AttachementDescription,
	[MessageContentID] = @MessageContentID,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID

GO
