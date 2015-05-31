	
/****** Object:  Stored Procedure [dbo].sysdiagramsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsInsert
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
CREATE PROCEDURE [dbo].spsysdiagramsInsert
	@name nvarchar(128),
	@principal_id int,
	@version int,
	@definition varbinary,
	@diagram_id int output
AS

INSERT INTO [dbo].[sysdiagrams] 
(
	[name],
	[principal_id],
	[version],
	[definition]
) 
VALUES 
(
	@name,
	@principal_id,
	@version,
	@definition
)

SELECT @diagram_id = @@IDENTITY

GO
	
