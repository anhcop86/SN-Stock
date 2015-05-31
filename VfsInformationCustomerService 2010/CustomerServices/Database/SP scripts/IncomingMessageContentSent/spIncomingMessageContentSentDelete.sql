/****** Object:  Stored Procedure [dbo].IncomingMessageContentSentDelete    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentSentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentSentDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentSentDelete
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
**		Date: 1/27/2010 11:49:26 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spIncomingMessageContentSentDelete
	@IncomingMessageContentSentID bigint
AS

DELETE FROM [dbo].[IncomingMessageContentSent]
WHERE
	[IncomingMessageContentSentID] = @IncomingMessageContentSentID
GO
	
	

	
