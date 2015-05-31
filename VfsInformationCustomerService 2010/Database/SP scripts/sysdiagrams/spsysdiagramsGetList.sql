/****** Object:  Stored Procedure [dbo].sysdiagramsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsGetList
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
CREATE PROCEDURE [dbo].spsysdiagramsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempsysdiagrams (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	diagram_id int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempsysdiagrams ([diagram_id]) SELECT '
SET @sql = @sql + ' [diagram_id] FROM [dbo].[sysdiagrams]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [sysdiagrams]

SELECT
	[name],
	[principal_id],
	[dbo].[sysdiagrams].[diagram_id],
	[version],
	[definition]
FROM
	#Tempsysdiagrams AS tblTemp JOIN [dbo].[sysdiagrams] ON
	tblTemp.diagram_id = [dbo].[sysdiagrams].diagram_id 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempsysdiagrams

GO

	
