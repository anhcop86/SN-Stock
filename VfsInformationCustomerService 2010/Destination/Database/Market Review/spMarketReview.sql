 If exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMarketReview]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMarketReview]
GO

CREATE PROCEDURE [dbo].[spMarketReview]
@PermDate datetime,
@Market nvarchar(10)
AS

Create Table #SelectedDate(CompareDate nvarchar(10))
INSERT INTO #SelectedDate 
		SELECT DISTINCT TOP 30 Convert(nvarchar(10), D1.PermDate, 112) PermDate
		FROM stock_SymbolPermLong D1
		WHERE Convert(nvarchar(10), D1.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112)
		ORDER BY Convert(nvarchar(10), D1.PermDate, 112)  DESC

Select  stock_Symbols.Symbol,
		PriceClose,		 		 
		(Select MAX(PriceClose)
			From dbo.stock_SymbolPermLong B
			Where Convert(nvarchar(10), B.PermDate, 112) IN (SELECT CompareDate FROM #SelectedDate)
				AND stock_Symbols.SymbolID = B.SymbolID) AS PriceHigh,
		
		(Select MIN(PriceClose)
			From dbo.stock_SymbolPermLong B
			Where Convert(nvarchar(10), B.PermDate, 112) IN (SELECT CompareDate FROM #SelectedDate)
				AND stock_Symbols.SymbolID = B.SymbolID) AS PriceLow,

		(PriceClose/(Select (PriceClose)
			From dbo.stock_SymbolPermLong B
			Where Convert(nvarchar(10), B.PermDate, 112) IN (SELECT MIN(CompareDate) FROM #SelectedDate)
				AND stock_Symbols.SymbolID = B.SymbolID)) -1  AS ChenhLechGia,
				
		(Select SUM(Volume)
			From dbo.stock_SymbolPermLong B
			Where Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)
				AND stock_Symbols.SymbolID = B.SymbolID)/30 AS "KLGDTBTrenPhien",

		(Select (SUM(ISNULL(TotalValue,0))/1000000)
			From dbo.stock_SymbolPermLong B
			Where Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)
				AND stock_Symbols.SymbolID = B.SymbolID)/30 AS "GTGDTrungBinhTrenPhien",

		(Select SUM(ISNULL(BuyQuantity,0)/1000)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "KLDM30Phien",

		(Select SUM(ISNULL(SellQuantity,0)/1000)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)) AS "KLDB30Phien",

		(Select (SUM(ISNULL(BuyQuantity,0) - ISNULL(SellQuantity,0))/1000)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)) AS "KLDMTruKLDBBaMuoiPhien",

		(Select SUM(ISNULL(BuyQuantity,0))
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)) / (Select (CASE WHEN SUM(ISNULL(BuyCount,0)) = 0 THEN 1 ELSE SUM(ISNULL(BuyCount,0)) END)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))
			AS "DVDMTrungBinhTrenLenh",


		(Select SUM(ISNULL(SellQuantity,0))
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC)) / (Select (CASE WHEN SUM(ISNULL(SellCount,0)) = 0 THEN 1 ELSE SUM(ISNULL(SellCount,0)) END )
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))
			 AS "DVDBTrungBinhTrenLenh",


		(Select SUM(ISNULL(BuyForeignQuantity,0))
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "NDTNNKLMuaBaMuoiPhien",
				
		(Select (SUM(ISNULL(BuyForeignValue,0))/1000000)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "NDTNNGTMuaBaMuoiPhien",

		(Select SUM(ISNULL(SellForeignQuantity,0))
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "NDTNNKLBanBaMuoiPhien",

		(Select (SUM(ISNULL(SellForeignValue,0))/1000000)
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID 
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "NDTNNGTBanBaMuoiPhien",

		(Select SUM(ISNULL(BuyForeignQuantity,0) - ISNULL(SellForeignQuantity,0))
			From dbo.stock_SymbolPermLong B
			Where stock_Symbols.SymbolID = B.SymbolID
				AND Convert(nvarchar(10), B.PermDate, 112) IN (Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate	FROM stock_SymbolPermLong D WHERE Convert(nvarchar(10), D.PermDate, 112) <= Convert(nvarchar(10), @PermDate, 112) ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC))  AS "NDTNNKLMuaTruBanBaMuoiPhien"

From stock_Markets, dbo.stock_Symbols, dbo.stock_SymbolPermLong
Where stock_Symbols.SymbolID = stock_SymbolPermLong.SymbolID
	AND Convert(nvarchar(10),PermDate, 112) = Convert(nvarchar(10), @PermDate, 112)
    AND stock_Markets.MarketID = stock_Symbols.MarketID
	AND (rtrim(stock_Markets.Symbol) = rtrim(@Market) or @Market = 'All')
	
Order by stock_Symbols.Symbol

DROP TABLE #SelectedDate

GO
