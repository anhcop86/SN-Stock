/****** Object:  Stored Procedure [dbo].MessageCommandGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandGetList
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
CREATE PROCEDURE [dbo].spMessageCommandGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempMessageCommand (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageCommandID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageCommand ([MessageCommandID]) SELECT '
SET @sql = @sql + ' [MessageCommandID] FROM [dbo].[MessageCommand]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [MessageCommand]

SELECT
	[dbo].[MessageCommand].[MessageCommandID],
	[MessageContentID],
	[Status],
	[ProcessedDateTime],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempMessageCommand AS tblTemp JOIN [dbo].[MessageCommand] ON
	tblTemp.MessageCommandID = [dbo].[MessageCommand].MessageCommandID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempMessageCommand

GO

	
