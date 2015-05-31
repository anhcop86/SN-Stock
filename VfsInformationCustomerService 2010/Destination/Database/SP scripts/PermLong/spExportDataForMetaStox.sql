if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExportDataForMetaStox]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExportDataForMetaStox]
GO
	
CREATE PROCEDURE [dbo].spExportDataForMetaStox
@PermDate datetime,
@MarketId int
AS
SELECT 	SS.Symbol,
        convert(nvarchar(10),PL.PermDate,112) as PermDate,
        PriceOpen/1000 AS PriceOpen,
        PriceHigh/1000 AS PriceHigh,
        PriceLow/1000 AS PriceLow,
        PriceClose/1000 AS PriceClose,
        Volume
FROM stock_SymbolPermLong PL, stock_Symbols SS, stock_markets SM
WHERE SS.SymbolId = PL.SymbolId
and SM.marketid = SS.marketid
And convert(nvarchar(10),PL.PermDate,112) =convert(nvarchar(10),@PermDate,112)
and SM.marketid  = @MarketId
ORDER BY SS.Symbol