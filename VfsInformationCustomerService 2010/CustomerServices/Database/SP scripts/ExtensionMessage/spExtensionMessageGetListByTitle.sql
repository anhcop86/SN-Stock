 /****** Object:  Stored Procedure [dbo].ExtensionMessageGetList    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageGetListByTitle]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageGetListByTitle]
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
CREATE PROCEDURE [dbo].spExtensionMessageGetListByTitle
	@Title varchar(50),
	@CustomerID nvarchar(10),
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
SET @sql = @sql + ' [ExtensionMessageID] FROM [dbo].[ExtensionMessage] WHERE convert(nvarchar(10),CreatedDate,112) = convert(nvarchar(10), GetDate(),112) AND ExtensionMessageID NOT IN (SELECT ExtensionMessageID FROM ExtensionMessageLog WHERE CustomerID =''' + @CustomerID + ''') AND Title=''' + @Title + ''' ORDER BY ' + ' [' + @OrderBy + N'] ' + @OrderDirection

EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) 
	FROM [ExtensionMessage] 
	WHERE convert(nvarchar(10),CreatedDate,112) = convert(nvarchar(10), GetDate(),112) 
			AND ExtensionMessageID NOT IN (SELECT ExtensionMessageID 
															FROM ExtensionMessageLog 
															WHERE CustomerID = @CustomerID) 
			AND Title=@Title

SELECT
	[dbo].[ExtensionMessage].[ExtensionMessageID],
	[Title],
	[Content],
	[CreatedDate]
FROM
	#TempExtensionMessage AS tblTemp JOIN [dbo].[ExtensionMessage] ON
	tblTemp.ExtensionMessageID = [dbo].[ExtensionMessage].ExtensionMessageID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
		AND [ExtensionMessage].ExtensionMessageID NOT IN (SELECT ExtensionMessageID 
															FROM ExtensionMessageLog 
															WHERE CustomerID = @CustomerID) 
			AND Title=@Title
ORDER BY RowNumber

DROP TABLE #TempExtensionMessage

GO

	
