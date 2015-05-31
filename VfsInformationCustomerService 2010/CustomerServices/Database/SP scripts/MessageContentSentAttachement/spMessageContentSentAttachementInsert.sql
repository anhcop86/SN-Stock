	
/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementInsert    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementInsert
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
CREATE PROCEDURE [dbo].spMessageContentSentAttachementInsert
	@AttachementDocument nvarchar(1024),
	@AttachementDescription nvarchar(500),
	@MessageContentID int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageContentSentAttachementID int output
AS

INSERT INTO [dbo].[MessageContentSentAttachement] 
(
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@AttachementDocument,
	@AttachementDescription,
	@MessageContentID,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageContentSentAttachementID = @@IDENTITY

GO
	
