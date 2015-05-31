/****** Object:  Stored Procedure [dbo].StockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsDelete
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
**		Date: 10/02/2009 18:23:30
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsDelete
	@NewsId bigint
AS

DELETE FROM [dbo].[StockNews]
WHERE
	[NewsId] = @NewsId
GO
	
	

	
