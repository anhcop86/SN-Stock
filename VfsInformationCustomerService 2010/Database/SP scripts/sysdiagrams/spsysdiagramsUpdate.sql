	
/****** Object:  Stored Procedure [dbo].sysdiagramsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsUpdate
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
CREATE PROCEDURE [dbo].spsysdiagramsUpdate
	@name nvarchar(128), 
	@principal_id int, 
	@diagram_id int, 
	@version int, 
	@definition varbinary 
AS

UPDATE [dbo].[sysdiagrams] SET
	[name] = @name,
	[principal_id] = @principal_id,
	[version] = @version,
	[definition] = @definition
WHERE
	[diagram_id] = @diagram_id

GO
