/****** Object:  Stored Procedure [dbo].IncomingMessageContentGetList    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentGetList
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
**		Date: 1/27/2010 11:49:26 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spIncomingMessageContentGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempIncomingMessageContent (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	IncomingMessageContentID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempIncomingMessageContent ([IncomingMessageContentID]) SELECT '
SET @sql = @sql + ' [IncomingMessageContentID] FROM [dbo].[IncomingMessageContent]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [IncomingMessageContent]

SELECT
	[dbo].[IncomingMessageContent].[IncomingMessageContentID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[Status],
	[CreatedDate],
	[ModifiedDate],
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID]
FROM
	#TempIncomingMessageContent AS tblTemp JOIN [dbo].[IncomingMessageContent] ON
	tblTemp.IncomingMessageContentID = [dbo].[IncomingMessageContent].IncomingMessageContentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempIncomingMessageContent

GO

	
