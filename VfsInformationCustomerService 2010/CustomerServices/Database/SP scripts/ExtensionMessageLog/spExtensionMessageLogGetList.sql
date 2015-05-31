/****** Object:  Stored Procedure [dbo].ExtensionMessageLogGetList    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageLogGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageLogGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageLogGetList
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
**		Date: 10/24/2011 5:23:36 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spExtensionMessageLogGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempExtensionMessageLog (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ExtensionMessageLogID bigint	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempExtensionMessageLog ([ExtensionMessageLogID]) SELECT '
SET @sql = @sql + ' [ExtensionMessageLogID] FROM [dbo].[ExtensionMessageLog]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ExtensionMessageLog]

SELECT
	[dbo].[ExtensionMessageLog].[ExtensionMessageLogID],
	[ExtensionMessageID],
	[CustomerId]
FROM
	#TempExtensionMessageLog AS tblTemp JOIN [dbo].[ExtensionMessageLog] ON
	tblTemp.ExtensionMessageLogID = [dbo].[ExtensionMessageLog].ExtensionMessageLogID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempExtensionMessageLog

GO

	
