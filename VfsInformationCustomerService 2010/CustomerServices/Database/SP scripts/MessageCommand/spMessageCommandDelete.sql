/****** Object:  Stored Procedure [dbo].MessageCommandDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandDelete
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
CREATE PROCEDURE [dbo].spMessageCommandDelete
	@MessageCommandID int
AS

DELETE FROM [dbo].[MessageCommand]
WHERE
	[MessageCommandID] = @MessageCommandID
GO
	
	

	
