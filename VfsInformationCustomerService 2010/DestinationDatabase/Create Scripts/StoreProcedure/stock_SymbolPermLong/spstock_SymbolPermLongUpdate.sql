	
/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongUpdate    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolPermLongUpdate
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
CREATE PROCEDURE [dbo].spstock_SymbolPermLongUpdate
	@SymbolID int, 
	@PermDate datetime, 
	@PriceOpen float, 
	@PriceClose float, 
	@PriceHigh float, 
	@PriceLow float, 
	@PriceAverage float, 
	@PricePreviousClose float, 
	@Volume float, 
	@TotalTrade float, 
	@TotalValue float, 
	@AdjRatio float, 
	@LastUpdated datetime, 
	@CurrentForeignRoom float, 
	@BuyCount float, 
	@BuyQuantity float, 
	@SellCount float, 
	@SellQuantity float, 
	@BuyForeignCount float, 
	@BuyForeignQuantity float, 
	@BuyForeignValue float, 
	@SellForeignCount float, 
	@SellForeignQuantity float, 
	@SellForeignValue float 
AS

UPDATE [dbo].[stock_SymbolPermLong] SET
	[PriceOpen] = @PriceOpen,
	[PriceClose] = @PriceClose,
	[PriceHigh] = @PriceHigh,
	[PriceLow] = @PriceLow,
	[PriceAverage] = @PriceAverage,
	[PricePreviousClose] = @PricePreviousClose,
	[Volume] = @Volume,
	[TotalTrade] = @TotalTrade,
	[TotalValue] = @TotalValue,
	[AdjRatio] = @AdjRatio,
	[LastUpdated] = @LastUpdated,
	[CurrentForeignRoom] = @CurrentForeignRoom,
	[BuyCount] = @BuyCount,
	[BuyQuantity] = @BuyQuantity,
	[SellCount] = @SellCount,
	[SellQuantity] = @SellQuantity,
	[BuyForeignCount] = @BuyForeignCount,
	[BuyForeignQuantity] = @BuyForeignQuantity,
	[BuyForeignValue] = @BuyForeignValue,
	[SellForeignCount] = @SellForeignCount,
	[SellForeignQuantity] = @SellForeignQuantity,
	[SellForeignValue] = @SellForeignValue
WHERE
	[SymbolID] = @SymbolID
	AND Convert(nvarchar(10), [PermDate], 112) = Convert(nvarchar(10), @PermDate, 112)

GO
