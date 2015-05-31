	
/****** Object:  Stored Procedure [dbo].StockPriceUpdate    Script Date: Tuesday, November 02, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockPriceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockPriceUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockPriceUpdate
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
CREATE PROCEDURE [dbo].spStockPriceUpdate
	@TradingDate varchar(10), 
	@StockCode varchar(10), 
	@StockNo int, 
	@StockType char(1), 
	@BoardType char(1), 
	@OpenPrice decimal(18, 1), 
	@ClosePrice decimal(18, 1), 
	@BasicPrice decimal(18, 1), 
	@CeilingPrice decimal(18, 1), 
	@FloorPrice decimal(18, 1), 
	@AveragePrice decimal(18, 1), 
	@TransactionDate smalldatetime, 
	@TotalRoom decimal(18, 0), 
	@CurrentRoom decimal(18, 0), 
	@Suspension char(1), 
	@Delisted char(1), 
	@Halted char(1), 
	@Split char(1), 
	@Benefit char(1), 
	@Meeting char(1), 
	@Notice char(1) 
AS

UPDATE [dbo].[StockPrice] SET
	[StockNo] = @StockNo,
	[StockType] = @StockType,
	[OpenPrice] = @OpenPrice,
	[ClosePrice] = @ClosePrice,
	[BasicPrice] = @BasicPrice,
	[CeilingPrice] = @CeilingPrice,
	[FloorPrice] = @FloorPrice,
	[AveragePrice] = @AveragePrice,
	[TransactionDate] = @TransactionDate,
	[TotalRoom] = @TotalRoom,
	[CurrentRoom] = @CurrentRoom,
	[Suspension] = @Suspension,
	[Delisted] = @Delisted,
	[Halted] = @Halted,
	[Split] = @Split,
	[Benefit] = @Benefit,
	[Meeting] = @Meeting,
	[Notice] = @Notice
WHERE
	[TradingDate] = @TradingDate
	AND [StockCode] = @StockCode
	AND [BoardType] = @BoardType

GO
