/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementGetList
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
CREATE PROCEDURE [dbo].spMessageContentSentAttachementGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempMessageContentSentAttachement (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageContentSentAttachementID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContentSentAttachement ([MessageContentSentAttachementID]) SELECT '
SET @sql = @sql + ' [MessageContentSentAttachementID] FROM [dbo].[MessageContentSentAttachement]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [MessageContentSentAttachement]

SELECT
	[dbo].[MessageContentSentAttachement].[MessageContentSentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempMessageContentSentAttachement AS tblTemp JOIN [dbo].[MessageContentSentAttachement] ON
	tblTemp.MessageContentSentAttachementID = [dbo].[MessageContentSentAttachement].MessageContentSentAttachementID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempMessageContentSentAttachement

GO

	
