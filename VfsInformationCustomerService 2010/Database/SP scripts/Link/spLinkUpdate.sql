	
/****** Object:  Stored Procedure [dbo].LinkUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkUpdate
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
CREATE PROCEDURE [dbo].spLinkUpdate
	@LinkId int, 
	@SourceId int, 
	@Link nvarchar(256), 
	@LinkShortDescription nvarchar(256), 
	@LinkDescription nvarchar(256) 
AS

UPDATE [dbo].[Link] SET
	[SourceId] = @SourceId,
	[Link] = @Link,
	[LinkShortDescription] = @LinkShortDescription,
	[LinkDescription] = @LinkDescription
WHERE
	[LinkId] = @LinkId

GO
