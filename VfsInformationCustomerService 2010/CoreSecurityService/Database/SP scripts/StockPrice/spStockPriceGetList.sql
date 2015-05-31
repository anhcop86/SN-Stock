/****** Object:  Stored Procedure [dbo].StockPriceGetList    Script Date: Tuesday, November 02, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockPriceGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockPriceGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockPriceGetList
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
**		Date: 11/2/2010 2:57:46 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockPriceGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempStockPrice (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	TradingDate varchar(10),	
	StockCode varchar(10),	
	BoardType char(1)	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempStockPrice ([TradingDate],[StockCode],[BoardType]) SELECT '
SET @sql = @sql + ' [TradingDate],[StockCode],[BoardType] FROM [dbo].[StockPrice]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [StockPrice]

SELECT
	[dbo].[StockPrice].[TradingDate],
	[dbo].[StockPrice].[StockCode],
	[StockNo],
	[StockType],
	[dbo].[StockPrice].[BoardType],
	[OpenPrice],
	[ClosePrice],
	[BasicPrice],
	[CeilingPrice],
	[FloorPrice],
	[AveragePrice],
	[TransactionDate],
	[TotalRoom],
	[CurrentRoom],
	[Suspension],
	[Delisted],
	[Halted],
	[Split],
	[Benefit],
	[Meeting],
	[Notice]
FROM
	#TempStockPrice AS tblTemp JOIN [dbo].[StockPrice] ON
	tblTemp.TradingDate = [dbo].[StockPrice].TradingDate  AND 	
	tblTemp.StockCode = [dbo].[StockPrice].StockCode  AND 	
	tblTemp.BoardType = [dbo].[StockPrice].BoardType 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempStockPrice

GO

	
