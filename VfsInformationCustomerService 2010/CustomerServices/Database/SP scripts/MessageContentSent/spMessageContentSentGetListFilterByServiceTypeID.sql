 /****** Object:  Stored Procedure [dbo].MessageContentSentGetList    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentGetListFilterByServiceTypeID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spMessageContentSentGetListFilterByServiceTypeID
GO

	
	
CREATE PROCEDURE [dbo].spMessageContentSentGetListFilterByServiceTypeID
	@ServiceTypeID int,	
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@FromModifiedDate datetime,
	@ToModifiedDate datetime,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempMessageContentSent (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageContentSentID bigint	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int


-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContentSent ([MessageContentSentID]) SELECT '
SET @sql = @sql + ' [MessageContentSentID] FROM [dbo].[MessageContentSent] 
					WHERE ([MessageContentSent].[ServiceTypeID] = '+''''+ CONVERT(nvarchar,@ServiceTypeID) +''''+' OR '+''''+ CONVERT(nvarchar,@ServiceTypeID) +''''+' = ''0'')
					AND (CONVERT(nvarchar(10),[MessageContentSent].ModifiedDate,112) between '+''''+ CONVERT(nvarchar(10),@FromModifiedDate,112)+''''+' AND '+''''+ CONVERT(nvarchar(10),@ToModifiedDate,112)+''''+')
					AND ([MessageContentSent].Sender LIKE ''%'+@Sender+'%'' OR '+''''+ @Sender +''''+' = ''All'')
					AND ([MessageContentSent].Receiver LIKE ''%'+@Receiver+'%'' OR '+''''+ @Receiver +''''+' = ''All'')
					ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
 
EXEC (@sql)
PRINT (@sql)
SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) 
FROM #TempMessageContentSent

SELECT
	[dbo].[MessageContentSent].[MessageContentSentID],
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
	ServiceID,
	CommandCode,
	Request,
	MoID,
	ChargeYN,
	TotalMessages,
	[MessageContentID]
FROM
	#TempMessageContentSent AS tblTemp JOIN [dbo].[MessageContentSent] ON
	tblTemp.MessageContentSentID = [dbo].[MessageContentSent].MessageContentSentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)	
ORDER BY RowNumber

DROP TABLE #TempMessageContentSent

GO

	
