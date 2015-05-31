	
/****** Object:  Stored Procedure [dbo].LinkInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spLinkInsert
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
CREATE PROCEDURE [dbo].spLinkInsert
	@SourceId int,
	@Link nvarchar(256),
	@LinkShortDescription nvarchar(256),
	@LinkDescription nvarchar(256),
	@LinkId int output
AS

INSERT INTO [dbo].[Link] 
(
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
) 
VALUES 
(
	@SourceId,
	@Link,
	@LinkShortDescription,
	@LinkDescription
)

SELECT @LinkId = @@IDENTITY

GO
	
