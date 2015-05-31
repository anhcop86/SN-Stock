	
/****** Object:  Stored Procedure [dbo].SourceUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceUpdate
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
CREATE PROCEDURE [dbo].spSourceUpdate
	@SourceId int, 
	@SiteName nvarchar(256), 
	@URL nvarchar(256) 
AS

UPDATE [dbo].[Source] SET
	[SiteName] = @SiteName,
	[URL] = @URL
WHERE
	[SourceId] = @SourceId

GO
