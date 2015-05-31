/****** Object:  Stored Procedure [dbo].BirthdayMessageLogGetList    Script Date: Thursday, April 08, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBirthdayMessageLogGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBirthdayMessageLogGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spBirthdayMessageLogGetList
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
**		Date: 4/8/2010 6:15:22 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spBirthdayMessageLogGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempBirthdayMessageLog (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	BirthdayMessageDay nvarchar(8)	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempBirthdayMessageLog ([BirthdayMessageDay]) SELECT '
SET @sql = @sql + ' [BirthdayMessageDay] FROM [dbo].[BirthdayMessageLog]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [BirthdayMessageLog]

SELECT
	[dbo].[BirthdayMessageLog].[BirthdayMessageDay],
	[ProccessYN]
FROM
	#TempBirthdayMessageLog AS tblTemp JOIN [dbo].[BirthdayMessageLog] ON
	tblTemp.BirthdayMessageDay = [dbo].[BirthdayMessageLog].BirthdayMessageDay 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempBirthdayMessageLog

GO

	
