/****** Object:  Stored Procedure [dbo].LinkGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkGet
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
**		Date: 10/02/2009 18:23:29
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkGet
	@LinkId int
AS

SELECT
	[LinkId],
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
FROM
	[dbo].[Link]
WHERE
	[LinkId] = @LinkId

GO
	

	
