/****** Object:  Stored Procedure [dbo].stock_SymbolsDelete    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsDelete
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
**		Date: 08/03/2009 17:05:14
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsDelete
	@SymbolID int
AS

DELETE FROM [dbo].[stock_Symbols]
WHERE
	[SymbolID] = @SymbolID
GO
	
	

	
