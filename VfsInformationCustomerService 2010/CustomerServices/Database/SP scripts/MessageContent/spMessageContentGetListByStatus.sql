/****** Object:  Stored Procedure [dbo].MessageContentGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGetListByStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentGetListByStatus]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentGetListByStatus
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
**		Date: 28/05/2009 16:59:44
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentGetListByStatus
	@Status int,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempMessageContent (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageContentID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContent ([MessageContentID]) SELECT '
SET @sql = @sql + ' [MessageContentID] FROM [dbo].[MessageContent] WHERE Status =' + Convert( nvarchar(10), @Status) + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [#TempMessageContent]

SELECT
	[dbo].[MessageContent].[MessageContentID],
	[ContentTemplateID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate],
	[Status],
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]
FROM
	#TempMessageContent AS tblTemp JOIN [dbo].[MessageContent] ON
	tblTemp.MessageContentID = [dbo].[MessageContent].MessageContentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND Status = @Status
ORDER BY RowNumber

DROP TABLE #TempMessageContent

GO