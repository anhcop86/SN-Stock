IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'View_TESTTOOL')
	BEGIN
		DROP  View View_TESTTOOL
	END
GO

CREATE View View_TESTTOOL AS

SELECT     a.SymbolID, CONVERT(nvarchar(10), a.PermDate, 112) AS PermDate, b.Symbol, a.PriceLow, 
                      (CASE WHEN b.MarketID = 6 THEN a.PriceClose ELSE a.PriceAverage END) AS PriceClose
FROM         dbo.stock_SymbolPermLong AS a ,dbo.stock_Symbols AS b
                       WHERE a.SymbolID = b.SymbolID
AND     (b.Symbol <> 'HOSE') AND (b.Symbol <> 'HASTC')


GO
 