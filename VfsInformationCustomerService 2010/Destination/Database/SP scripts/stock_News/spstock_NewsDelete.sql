/****** Object:  Stored Procedure [dbo].stock_NewsDelete    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsDelete
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
CREATE PROCEDURE [dbo].spstock_NewsDelete
	@NewsID int
AS

DELETE FROM [dbo].[stock_News]
WHERE
	[NewsID] = @NewsID
GO
	
	

	
