/****** Object:  Stored Procedure [dbo].LinkGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkGetListBySource]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkGetListBySource]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkGetList
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
**		Date: 10/02/2009 18:23:29
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkGetListBySource
	@SourceId int,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempLinkBySource (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	LinkId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempLinkBySource ([LinkId]) SELECT '
SET @sql = @sql + ' [LinkId] FROM [dbo].[Link] WHERE SourceId = ' + convert(varchar(10), @SourceId) + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Link]

SELECT
	[dbo].[Link].[LinkId],
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
FROM
	#TempLinkBySource AS tblTemp JOIN [dbo].[Link] ON
	tblTemp.LinkId = [dbo].[Link].LinkId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND [SourceId] = @SourceId
ORDER BY RowNumber

DROP TABLE #TempLinkBySource

GO

	
 