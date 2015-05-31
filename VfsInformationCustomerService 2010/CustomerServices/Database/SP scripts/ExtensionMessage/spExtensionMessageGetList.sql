/****** Object:  Stored Procedure [dbo].ExtensionMessageGetList    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageGetList
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
CREATE PROCEDURE [dbo].spExtensionMessageGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempExtensionMessage (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ExtensionMessageID bigint	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempExtensionMessage ([ExtensionMessageID]) SELECT '
SET @sql = @sql + ' [ExtensionMessageID] FROM [dbo].[ExtensionMessage]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ExtensionMessage]

SELECT
	[dbo].[ExtensionMessage].[ExtensionMessageID],
	[Title],
	[Content],
	[CreatedDate]
FROM
	#TempExtensionMessage AS tblTemp JOIN [dbo].[ExtensionMessage] ON
	tblTemp.ExtensionMessageID = [dbo].[ExtensionMessage].ExtensionMessageID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempExtensionMessage

GO

	
