/****** Object:  Stored Procedure [dbo].SourceGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceGet
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
CREATE PROCEDURE [dbo].spSourceGet
	@SourceId int
AS

SELECT
	[SourceId],
	[SiteName],
	[URL]
FROM
	[dbo].[Source]
WHERE
	[SourceId] = @SourceId

GO
	

	
