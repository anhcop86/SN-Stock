/****** Object:  Stored Procedure [dbo].stock_SymbolPermLongGetList    Script Date: 06 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolPermLongGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolPermLongGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolPermLongGetList
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
CREATE PROCEDURE [dbo].spstock_SymbolPermLongGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_SymbolPermLong (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	SymbolID int,	
	PermDate datetime	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_SymbolPermLong ([SymbolID],[PermDate]) SELECT '
SET @sql = @sql + ' [SymbolID],[PermDate] FROM [dbo].[stock_SymbolPermLong]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_SymbolPermLong]

SELECT
	[dbo].[stock_SymbolPermLong].[SymbolID],
	[dbo].[stock_SymbolPermLong].[PermDate],
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
	#Tempstock_SymbolPermLong AS tblTemp JOIN [dbo].[stock_SymbolPermLong] ON
	tblTemp.SymbolID = [dbo].[stock_SymbolPermLong].SymbolID  AND 	
	tblTemp.PermDate = [dbo].[stock_SymbolPermLong].PermDate 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempstock_SymbolPermLong

GO

	
