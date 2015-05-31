if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongUpdateDataForeign]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongUpdateDataForeign]
GO

CREATE PROCEDURE [dbo].[spstock_SymbolPermLongUpdateDataForeign]
	@PermDate datetime
AS

UPDATE [dbo].[stock_SymbolPermLong] SET
	
	[BuyForeignQuantity] = 0,
	[BuyForeignValue] = 0,	
	[SellForeignQuantity] = 0,
	[SellForeignValue] = 0
WHERE
	Convert(nvarchar(10), [PermDate], 112) = Convert(nvarchar(10), @PermDate, 112)
GO
