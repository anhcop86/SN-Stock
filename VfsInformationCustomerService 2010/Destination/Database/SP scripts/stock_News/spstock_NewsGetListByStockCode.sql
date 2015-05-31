 /****** Object:  Stored Procedure [dbo].stock_NewsGetList    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGetListByStockCode]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGetListByStockCode]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGetList
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
**		Date: 06/03/2009 17:16:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGetListByStockCode
    @StockCode nvarchar(10),
    @CustomerID nvarchar(10),
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_NewsByStockCode (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @StockCode = @StockCode + ':'
SET @sql =	N'INSERT INTO #Tempstock_NewsByStockCode ([NewsID]) SELECT '
SET @sql = @sql + ' [NewsID] FROM [dbo].[stock_News] WHERE convert(nvarchar(10),NewsDate,112) = convert(nvarchar(10), GetDate(),112) AND [stock_News].NewsID NOT IN (SELECT NewsID FROM RelatedMessagelog WHERE CustomerID =''' + @CustomerID + ''') AND left(NewsTitle,4)=''' + @StockCode + ''' ORDER BY ' +  '[' + @OrderBy + N'] ' + @OrderDirection
print @sql
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_News] WHERE NewsID NOT IN (SELECT NewsID FROM RelatedMessagelog WHERE CustomerID = @CustomerID) 
AND left(NewsTitle,4) = @StockCode

SELECT
	[dbo].[stock_News].[NewsID],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[SymbolID],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl]
FROM
	#Tempstock_NewsByStockCode AS tblTemp JOIN [dbo].[stock_News] ON
	tblTemp.NewsID = [dbo].[stock_News].NewsID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
        AND [stock_News].NewsID NOT IN (SELECT NewsID FROM RelatedMessagelog  WHERE CustomerID = @CustomerID) AND left(NewsTitle,4)= @StockCode
        AND  convert(nvarchar(10),NewsDate,112) = convert(nvarchar(10), GetDate(),112)        
ORDER BY RowNumber

DROP TABLE #Tempstock_NewsByStockCode

GO

	
