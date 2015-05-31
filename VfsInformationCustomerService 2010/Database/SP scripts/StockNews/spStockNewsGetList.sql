/****** Object:  Stored Procedure [dbo].StockNewsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsGetList
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
**		Date: 10/02/2009 18:23:30
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempStockNews (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempStockNews ([NewsId]) SELECT '
SET @sql = @sql + ' [NewsId] FROM [dbo].[StockNews]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [StockNews]

SELECT
	[dbo].[StockNews].[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[LinkId],
	[OriginalUrl],
	[FeedDate]
FROM
	#TempStockNews AS tblTemp JOIN [dbo].[StockNews] ON
	tblTemp.NewsId = [dbo].[StockNews].NewsId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempStockNews

GO

	
