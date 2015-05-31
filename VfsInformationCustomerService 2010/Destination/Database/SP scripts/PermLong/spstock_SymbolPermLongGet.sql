/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongGet    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolPermLongGet
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
CREATE PROCEDURE [dbo].spstock_SymbolPermLongGet
	@SymbolID int,
	@PermDate datetime
AS

SELECT
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
FROM
	[dbo].[stock_SymbolPermLong]
WHERE
	[SymbolID] = @SymbolID
	AND Convert(nvarchar(10), [PermDate], 112) = Convert(nvarchar(10), @PermDate, 112)

GO
	

	
