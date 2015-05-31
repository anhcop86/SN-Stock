if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExportIndexForMetaStox]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spExportIndexForMetaStox
GO
	
CREATE PROCEDURE [dbo].spExportIndexForMetaStox
@PermDate datetime,
@MarketId int
AS
SELECT 	SS.Symbol,
        convert(nvarchar(10),PL.PermDate,112) as PermDate,
        PriceOpen AS PriceOpen,
        PriceHigh AS PriceHigh,
        PriceLow AS PriceLow,
        PriceClose AS PriceClose,
        Volume
FROM stock_SymbolPermLong PL, stock_Symbols SS
WHERE SS.SymbolId = PL.SymbolId
And convert(nvarchar(10),PL.PermDate,112) = convert(nvarchar(10),@PermDate,112)
and SS.MarketId = @MarketId
ORDER BY SS.Symbol