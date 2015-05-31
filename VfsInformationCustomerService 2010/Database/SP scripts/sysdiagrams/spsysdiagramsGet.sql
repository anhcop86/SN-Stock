/****** Object:  Stored Procedure [dbo].sysdiagramsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsGet
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
CREATE PROCEDURE [dbo].spsysdiagramsGet
	@diagram_id int
AS

SELECT
	[name],
	[principal_id],
	[diagram_id],
	[version],
	[definition]
FROM
	[dbo].[sysdiagrams]
WHERE
	[diagram_id] = @diagram_id

GO
	

	
