/****** Object:  Stored Procedure [dbo].IncomingMessageContentDelete    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentDelete
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
CREATE PROCEDURE [dbo].spIncomingMessageContentDelete
	@IncomingMessageContentID int
AS

DELETE FROM [dbo].[IncomingMessageContent]
WHERE
	[IncomingMessageContentID] = @IncomingMessageContentID
GO
	
	

	
