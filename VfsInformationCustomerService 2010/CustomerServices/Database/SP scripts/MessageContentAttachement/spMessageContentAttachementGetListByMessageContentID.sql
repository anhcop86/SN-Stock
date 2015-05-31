 /****** Object:  Stored Procedure [dbo].MessageContentAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementGetListByMessageContentID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementGetListByMessageContentID]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementGetList
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
**		Date: 09/06/2009 16:46:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementGetListByMessageContentID
	@MessageContentID int,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempMessageContentAttachement (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageContentAttachementID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContentAttachement ([MessageContentAttachementID]) SELECT '
SET @sql = @sql + ' [MessageContentAttachementID] FROM [dbo].[MessageContentAttachement] WHERE MessageContentID = '  + Convert( nvarchar(10), @MessageContentID) + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM #TempMessageContentAttachement

SELECT
	[dbo].[MessageContentAttachement].[MessageContentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempMessageContentAttachement AS tblTemp JOIN [dbo].[MessageContentAttachement] ON
	tblTemp.MessageContentAttachementID = [dbo].[MessageContentAttachement].MessageContentAttachementID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND MessageContentID = @MessageContentID
ORDER BY RowNumber

DROP TABLE #TempMessageContentAttachement

GO

	
