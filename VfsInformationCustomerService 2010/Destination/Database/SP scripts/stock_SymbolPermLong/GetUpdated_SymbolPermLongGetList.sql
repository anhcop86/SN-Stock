/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongGetList    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUpdated_SymbolPermLongGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetUpdated_SymbolPermLongGetList]
GO

	
CREATE PROCEDURE [dbo].GetUpdated_SymbolPermLongGetList	
@PermDate datetime
AS

SELECT
	[stock_SymbolPermLong].[SymbolID],
	[stock_SymbolPermLong].[PermDate],
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
FROM [stock_SymbolPermLong],stock_Symbols,stock_Markets
	
WHERE 
	[stock_SymbolPermLong].[SymbolID] = stock_Symbols.[SymbolID]
AND stock_Symbols.MarketId = stock_Markets.MarketId
AND (stock_Symbols.Symbol <> 'HOSE' OR stock_Symbols.Symbol <> 'HASTC')
AND	[stock_SymbolPermLong].[PermDate] = Convert(nvarchar(10), @PermDate, 112)
ORDER BY stock_Markets.Symbol,stock_Symbols.Symbol ASC
GO

	
