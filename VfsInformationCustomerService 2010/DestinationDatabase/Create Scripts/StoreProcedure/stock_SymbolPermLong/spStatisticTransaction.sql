If exists (select * from dbo.sysobjects where id = object_id(N'[spStatisticTransaction]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [spStatisticTransaction]
GO

CREATE PROCEDURE [spStatisticTransaction]
@PermDate datetime,
@Market nvarchar(10)
AS

Select  stock_Symbols.Symbol,
		BuyCount,
		BuyQuantity,
		SellCount,
		SellQuantity,
		(BuyQuantity - SellQuantity) AS "Change",
		(BuyQuantity/(CASE WHEN BuyCount = 0 THEN 1 ELSE BuyCount END)) AS "DVDMTrungBinhTrenLenh",
		(SellQuantity/(CASE WHEN SellCount = 0 THEN 1 ELSE SellCount END)) AS "DVDBTrungBinhTrenLenh",
		Volume,
		(TotalValue / 1000) AS "TotalValue"

From stock_Markets, dbo.stock_Symbols, dbo.stock_SymbolPermLong
Where stock_Symbols.SymbolID = stock_SymbolPermLong.SymbolID
	AND stock_Markets.MarketID = stock_Symbols.MarketID
	AND Convert(nvarchar(10),PermDate, 112) = Convert(nvarchar(10), @PermDate, 112)    
	AND (rtrim(stock_Markets.Symbol) = rtrim(@Market) or @Market = 'All')
	
Order by stock_Symbols.Symbol

GO

