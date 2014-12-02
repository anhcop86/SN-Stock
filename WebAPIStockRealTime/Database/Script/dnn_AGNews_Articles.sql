if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HN__AGNews_Articles_SelectByTicker]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[HN__AGNews_Articles_SelectByTicker]
GO


CREATE PROCEDURE [dbo].[HN__AGNews_Articles_SelectByTicker]

		@TickerList varchar(2000),
		@Page int,
		@PageSize int,
		@TotalRecords int output

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempAGNews_Articles (
		RowNumber INT IDENTITY (1, 1) NOT NULL,
		ArticleId int
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
--SET @TickerList = 'TV3'+''',''' + 'KCE'
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempAGNews_Articles (ArticleId) SELECT '
SET @sql = @sql + ' ArticleId FROM dbo.dnn_AGNews_Articles WHERE (Company IS NOT NULL AND Company <> '''' AND LEN(Company) <=6)  AND  Company IN (''' + @TickerList + ''')
					Order By UpdatedDate DESC  '
					
print(@sql)
EXEC (@sql)

SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM #TempAGNews_Articles



SELECT  
		AA.ArticleId,
		AA.Company,
		AA.Title,
		AA.Lead,
		AA.Content,
		AA.[Source],
		AA.ImageFile,
		AA.ImageNote,
		AA.CreatedDate,
		AA.UpdatedDate
		
FROM  

#TempAGNews_Articles AS tblTemp JOIN [dbo].dnn_AGNews_Articles AS AA ON
	tblTemp.ArticleId = AA.ArticleId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempAGNews_Articles




GO


