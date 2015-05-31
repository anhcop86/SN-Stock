/****** Object:  Stored Procedure [dbo].StockPriceDelete    Script Date: Tuesday, November 02, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockPriceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockPriceDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockPriceDelete
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
**		Date: 11/2/2010 2:57:46 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockPriceDelete
	@TradingDate varchar(10),
	@StockCode varchar(10),
	@BoardType char(1)
AS

DELETE FROM [dbo].[StockPrice]
WHERE
	[TradingDate] = @TradingDate
	AND [StockCode] = @StockCode
	AND [BoardType] = @BoardType
GO
	
	

	
