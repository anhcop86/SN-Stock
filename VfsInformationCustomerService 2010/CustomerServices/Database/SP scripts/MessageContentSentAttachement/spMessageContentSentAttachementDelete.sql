/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementDelete
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
CREATE PROCEDURE [dbo].spMessageContentSentAttachementDelete
	@MessageContentSentAttachementID int
AS

DELETE FROM [dbo].[MessageContentSentAttachement]
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID
GO
	
	

	
