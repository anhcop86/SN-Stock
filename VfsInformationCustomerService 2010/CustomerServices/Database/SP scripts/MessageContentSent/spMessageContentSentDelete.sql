/****** Object:  Stored Procedure [dbo].MessageContentSentDelete    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentDelete
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
**		Date: 06/06/2009 16:44:23
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentDelete
	@MessageContentSentID bigint
AS

DELETE FROM [dbo].[MessageContentSent]
WHERE
	[MessageContentSentID] = @MessageContentSentID
GO
	
	

	
