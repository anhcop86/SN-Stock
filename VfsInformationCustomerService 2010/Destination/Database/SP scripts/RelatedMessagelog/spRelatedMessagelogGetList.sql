/****** Object:  Stored Procedure [dbo].RelatedMessagelogGetList    Script Date: Wednesday, July 07, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRelatedMessagelogGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRelatedMessagelogGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spRelatedMessagelogGetList
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
**		Date: 7/7/2010 11:43:19 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRelatedMessagelogGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempRelatedMessagelog (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	RelatedMessagelogID bigint	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempRelatedMessagelog ([RelatedMessagelogID]) SELECT '
SET @sql = @sql + ' [RelatedMessagelogID] FROM [dbo].[RelatedMessagelog]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [RelatedMessagelog]

SELECT
	[dbo].[RelatedMessagelog].[RelatedMessagelogID],
	[NewsID],
	[CustomerID]
FROM
	#TempRelatedMessagelog AS tblTemp JOIN [dbo].[RelatedMessagelog] ON
	tblTemp.RelatedMessagelogID = [dbo].[RelatedMessagelog].RelatedMessagelogID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempRelatedMessagelog

GO

	
