/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongDelete    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolPermLongDelete
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
**		Date: 06/05/2009 11:16:43
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolPermLongDelete
	@SymbolID int,
	@PermDate datetime
AS

DELETE FROM [dbo].[stock_SymbolPermLong]
WHERE
	[SymbolID] = @SymbolID
	AND Convert(nvarchar(10), [PermDate], 112) = Convert(nvarchar(10), @PermDate, 112)
GO
	
	

	
