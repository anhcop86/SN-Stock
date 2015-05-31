/****** Object:  Stored Procedure [dbo].SourceGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceGetList
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
**		Date: 10/02/2009 18:23:28
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempSource (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	SourceId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempSource ([SourceId]) SELECT '
SET @sql = @sql + ' [SourceId] FROM [dbo].[Source]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Source]

SELECT
	[dbo].[Source].[SourceId],
	[SiteName],
	[URL]
FROM
	#TempSource AS tblTemp JOIN [dbo].[Source] ON
	tblTemp.SourceId = [dbo].[Source].SourceId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempSource

GO

	
