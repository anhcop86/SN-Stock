/****** Object:  Stored Procedure [dbo].SourceDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceDelete
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
**		Date: 10/02/2009 18:23:28
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceDelete
	@SourceId int
AS

DELETE FROM [dbo].[Source]
WHERE
	[SourceId] = @SourceId
GO
	
	

	
