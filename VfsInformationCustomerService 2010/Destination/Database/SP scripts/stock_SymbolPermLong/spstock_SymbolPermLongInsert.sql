	
/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongInsert    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolPermLongInsert
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
CREATE PROCEDURE [dbo].spstock_SymbolPermLongInsert
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

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [dbo].[stock_SymbolPermLong] (
	[SymbolID],
	[PermDate],
	[PriceOpen],
	[PriceClose],
	[PriceHigh],
	[PriceLow],
	[PriceAverage],
	[PricePreviousClose],
	[Volume],
	[TotalTrade],
	[TotalValue],
	[AdjRatio],
	[LastUpdated],
	[CurrentForeignRoom],
	[BuyCount],
	[BuyQuantity],
	[SellCount],
	[SellQuantity],
	[BuyForeignCount],
	[BuyForeignQuantity],
	[BuyForeignValue],
	[SellForeignCount],
	[SellForeignQuantity],
	[SellForeignValue]
) VALUES (
	@SymbolID,
	@PermDate,
	@PriceOpen,
	@PriceClose,
	@PriceHigh,
	@PriceLow,
	@PriceAverage,
	@PricePreviousClose,
	@Volume,
	@TotalTrade,
	@TotalValue,
	@AdjRatio,
	@LastUpdated,
	@CurrentForeignRoom,
	@BuyCount,
	@BuyQuantity,
	@SellCount,
	@SellQuantity,
	@BuyForeignCount,
	@BuyForeignQuantity,
	@BuyForeignValue,
	@SellForeignCount,
	@SellForeignQuantity,
	@SellForeignValue
)

GO
	
