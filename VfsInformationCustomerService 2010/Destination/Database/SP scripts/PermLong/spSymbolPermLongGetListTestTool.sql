/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongGetList    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSymbolPermLongGetListTestTool]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSymbolPermLongGetListTestTool]
GO
	
CREATE PROCEDURE [dbo].spSymbolPermLongGetListTestTool
@PermDateFrom datetime,
@PermDateto datetime,
@countAVG int

AS
		CREATE TABLE #tablevar(
				SymbolID int
				,AVGVolume float)
INSERT INTO #tablevar EXEC [spGetVolumeAVG] @countAVG
SELECT
		stock_Symbols.Symbol AS SymbolTo, 
		(CASE WHEN stock_Symbols.MarketID = 6 THEN stock_SymbolPermLong.PriceClose ELSE stock_SymbolPermLong.PriceAverage END) AS PriceCloseTo,
		PriceHigh AS PriceHighTo ,		
		(SELECT V.Symbol FROM View_TESTTOOL V WHERE V.SymbolId = stock_Symbols.SymbolId AND V.PermDate = convert(nvarchar(10),@PermDateFrom,112)) AS SymbolFrom,
		(SELECT V.PriceLow FROM View_TESTTOOL V WHERE V.SymbolId = stock_Symbols.SymbolId AND V.PermDate = convert(nvarchar(10),@PermDateFrom,112))AS PriceLowFrom,
		(SELECT V.PriceClose FROM View_TESTTOOL V WHERE V.SymbolId = stock_Symbols.SymbolId AND V.PermDate = convert(nvarchar(10),@PermDateFrom,112)) AS PriceCloseFrom
		, (SELECT AVGVolume FROM #tablevar
			  WHERE #tablevar.[SymbolID] = stock_Symbols.SymbolId) AS	AVGVolume
FROM stock_SymbolPermLong, stock_Symbols
WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId 
AND (stock_Symbols.Symbol <> 'HOSE' AND stock_Symbols.Symbol <> 'HASTC') 
AND convert(nvarchar(10),stock_SymbolPermLong.PermDate,112) = convert(nvarchar(10),@PermDateto,112) order by stock_Symbols.Symbol

DROP TABLE #tablevar
GO

	
 