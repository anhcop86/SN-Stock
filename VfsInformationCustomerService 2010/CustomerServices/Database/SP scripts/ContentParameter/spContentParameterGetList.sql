/****** Object:  Stored Procedure [dbo].ContentParameterGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterGetList
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
**		Date: 28/05/2009 16:59:42
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentParameter (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentParameterID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentParameter ([ContentParameterID]) SELECT '
SET @sql = @sql + ' [ContentParameterID] FROM [dbo].[ContentParameter]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentParameter]

SELECT
	[dbo].[ContentParameter].[ContentParameterID],
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
FROM
	#TempContentParameter AS tblTemp JOIN [dbo].[ContentParameter] ON
	tblTemp.ContentParameterID = [dbo].[ContentParameter].ContentParameterID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempContentParameter

GO

	
