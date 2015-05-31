/****** Object:  Stored Procedure [dbo].ContentTemplateGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateGetList
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
**		Date: 28/05/2009 16:59:43
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentTemplate (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentTemplateID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentTemplate ([ContentTemplateID]) SELECT '
SET @sql = @sql + ' [ContentTemplateID] FROM [dbo].[ContentTemplate]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentTemplate]

SELECT
	[dbo].[ContentTemplate].[ContentTemplateID],
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempContentTemplate AS tblTemp JOIN [dbo].[ContentTemplate] ON
	tblTemp.ContentTemplateID = [dbo].[ContentTemplate].ContentTemplateID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempContentTemplate

GO

	
