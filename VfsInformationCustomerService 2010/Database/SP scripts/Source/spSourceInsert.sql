	
/****** Object:  Stored Procedure [dbo].SourceInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spSourceInsert
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
CREATE PROCEDURE [dbo].spSourceInsert
	@SiteName nvarchar(256),
	@URL nvarchar(256),
	@SourceId int output
AS

INSERT INTO [dbo].[Source] 
(
	[SiteName],
	[URL]
) 
VALUES 
(
	@SiteName,
	@URL
)

SELECT @SourceId = @@IDENTITY

GO
	
