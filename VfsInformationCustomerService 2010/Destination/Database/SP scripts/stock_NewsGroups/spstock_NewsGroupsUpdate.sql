	
/****** Object:  Stored Procedure [dbo].stock_NewsGroupsUpdate    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsUpdate
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
CREATE PROCEDURE [dbo].spstock_NewsGroupsUpdate
	@ID int, 
	@NewsID int, 
	@NewsGroup int 
AS

UPDATE [dbo].[stock_NewsGroups] SET
	[NewsID] = @NewsID,
	[NewsGroup] = @NewsGroup
WHERE
	[ID] = @ID

GO
