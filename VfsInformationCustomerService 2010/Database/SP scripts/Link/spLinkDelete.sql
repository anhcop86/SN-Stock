/****** Object:  Stored Procedure [dbo].LinkDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkDelete
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
CREATE PROCEDURE [dbo].spLinkDelete
	@LinkId int
AS

DELETE FROM [dbo].[Link]
WHERE
	[LinkId] = @LinkId
GO
	
	

	
