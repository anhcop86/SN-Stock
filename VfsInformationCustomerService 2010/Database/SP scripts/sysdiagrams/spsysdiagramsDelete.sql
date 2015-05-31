/****** Object:  Stored Procedure [dbo].sysdiagramsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsDelete
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
CREATE PROCEDURE [dbo].spsysdiagramsDelete
	@diagram_id int
AS

DELETE FROM [dbo].[sysdiagrams]
WHERE
	[diagram_id] = @diagram_id
GO
	
	

	
