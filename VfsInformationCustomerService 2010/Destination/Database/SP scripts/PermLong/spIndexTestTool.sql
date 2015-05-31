/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongGetList    Script Date: 06 May 2009 ******/
IF exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIndexTestTool]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIndexTestTool]
GO
	
CREATE PROCEDURE [dbo].spIndexTestTool
@PermDate datetime

AS
SELECT stock_Symbols.Symbol AS Symbol, stock_SymbolPermLong.PriceClose AS IndexSymbol
FROM stock_SymbolPermLong, stock_Symbols 
WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId AND stock_Symbols.Symbol IN ('HOSE' , 'HASTC')
 And convert(nvarchar(10),PermDate,112) = convert(nvarchar(10),@PermDate,112) order by stock_Symbols.Symbol DESC

GO