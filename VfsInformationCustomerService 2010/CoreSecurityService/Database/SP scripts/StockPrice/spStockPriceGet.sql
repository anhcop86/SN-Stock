/****** Object:  Stored Procedure [dbo].StockPriceGet    Script Date: Tuesday, November 02, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockPriceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockPriceGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockPriceGet
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
CREATE PROCEDURE [dbo].spStockPriceGet
	@TradingDate varchar(10),
	@StockCode varchar(10),
	@BoardType char(1)
AS

SELECT
	[TradingDate],
	[StockCode],
	[StockNo],
	[StockType],
	[BoardType],
	[OpenPrice],
	[ClosePrice],
	[BasicPrice],
	[CeilingPrice],
	[FloorPrice],
	[AveragePrice],
	[TransactionDate],
	[TotalRoom],
	[CurrentRoom],
	[Suspension],
	[Delisted],
	[Halted],
	[Split],
	[Benefit],
	[Meeting],
	[Notice]
FROM
	[dbo].[StockPrice]
WHERE
	[TradingDate] = @TradingDate
	AND [StockCode] = @StockCode
	AND [BoardType] = @BoardType

GO
	

	
