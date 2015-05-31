/****** Object:  Stored Procedure [dbo].stock_NewsGroupsDelete    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsDelete
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
**		Date: 06/03/2009 17:16:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsDelete
	@ID int
AS

DELETE FROM [dbo].[stock_NewsGroups]
WHERE
	[ID] = @ID
GO
	
	

	
