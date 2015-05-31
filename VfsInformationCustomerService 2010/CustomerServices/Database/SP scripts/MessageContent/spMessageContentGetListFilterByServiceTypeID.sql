 /****** Object:  Stored Procedure [dbo].MessageContentGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGetListFilterByServiceTypeID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spMessageContentGetListFilterByServiceTypeID
GO

CREATE PROCEDURE [dbo].spMessageContentGetListFilterByServiceTypeID
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

CREATE TABLE #TempMessageContent (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	MessageContentID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContent ([MessageContentID]) SELECT '
SET @sql = @sql + ' [MessageContentID] FROM [dbo].[MessageContent] 
					WHERE ([MessageContent].ServiceTypeID = '+ ''''+ Convert(nvarchar , @ServiceTypeID) + '''' + ' OR '+ ''''+ Convert(nvarchar , @ServiceTypeID) + '''' + ' = ''0'')
					AND (CONVERT(nvarchar(10),[MessageContent].ModifiedDate,112) between '+''''+ CONVERT(nvarchar(10),@FromModifiedDate,112)+''''+' AND '+''''+ CONVERT(nvarchar(10),@ToModifiedDate,112)+''''+')
					AND ([MessageContent].Sender LIKE ''%'+@Sender+'%'' OR '+''''+ @Sender +''''+' = ''All'')
					AND ([MessageContent].Receiver LIKE ''%'+@Receiver+'%'' OR '+''''+ @Receiver +''''+' = ''All'')
					ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
print (@sql)

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) 
FROM #TempMessageContent					

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
ORDER BY RowNumber

DROP TABLE #TempMessageContent

GO
