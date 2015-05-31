/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementGet
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
CREATE PROCEDURE [dbo].spMessageContentSentAttachementGet
	@MessageContentSentAttachementID int
AS

SELECT
	[MessageContentSentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageContentSentAttachement]
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID

GO
	

	
