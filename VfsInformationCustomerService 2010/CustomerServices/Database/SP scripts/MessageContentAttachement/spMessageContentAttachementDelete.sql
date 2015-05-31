/****** Object:  Stored Procedure [dbo].MessageContentAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementDelete
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
CREATE PROCEDURE [dbo].spMessageContentAttachementDelete
	@MessageContentAttachementID int
AS

DELETE FROM [dbo].[MessageContentAttachement]
WHERE
	[MessageContentAttachementID] = @MessageContentAttachementID
GO
	
	

	
