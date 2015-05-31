	
/****** Object:  Stored Procedure [dbo].stock_NewsGroupsInsert    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsInsert
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
CREATE PROCEDURE [dbo].spstock_NewsGroupsInsert
	@NewsID int,
	@NewsGroup int,
	@ID int output
AS

INSERT INTO [dbo].[stock_NewsGroups] 
(
	[NewsID],
	[NewsGroup]
) 
VALUES 
(
	@NewsID,
	@NewsGroup
)

SELECT @ID = @@IDENTITY

GO
	
