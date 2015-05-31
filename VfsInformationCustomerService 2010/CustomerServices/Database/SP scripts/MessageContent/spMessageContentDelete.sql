/****** Object:  Stored Procedure [dbo].MessageContentDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentDelete
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
**		Date: 28/05/2009 16:59:43
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentDelete
	@MessageContentID int
AS

DELETE FROM [dbo].[MessageContent]
WHERE
	[MessageContentID] = @MessageContentID
GO
	
	

	
