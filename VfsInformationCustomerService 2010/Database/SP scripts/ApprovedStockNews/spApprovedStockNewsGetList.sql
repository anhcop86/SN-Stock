/****** Object:  Stored Procedure [dbo].ApprovedStockNewsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsGetList
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
**		Date: 10/02/2009 18:23:31
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempApprovedStockNews (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempApprovedStockNews ([NewsId]) SELECT '
SET @sql = @sql + ' [NewsId] FROM [dbo].[ApprovedStockNews]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ApprovedStockNews]

SELECT
	[dbo].[ApprovedStockNews].[NewsId],
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
	[Comment],
	[LinkId],
	[OriginalUrl],
	[ApprovedDate]
FROM
	#TempApprovedStockNews AS tblTemp JOIN [dbo].[ApprovedStockNews] ON
	tblTemp.NewsId = [dbo].[ApprovedStockNews].NewsId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempApprovedStockNews

GO

	
