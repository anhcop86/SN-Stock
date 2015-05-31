/****** Object:  Stored Procedure [dbo].ContentParameterDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterDelete]
GO

/****** Object:  Stored Procedure [dbo].ContentParameterGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterGet]
GO

/****** Object:  Stored Procedure [dbo].ContentParameterGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterGetList]
GO

	
/****** Object:  Stored Procedure [dbo].ContentParameterInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterInsert]
GO

/****** Object:  Stored Procedure [dbo].ContentParameterUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterDelete
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterDelete
	@ContentParameterID int
AS

DELETE FROM [dbo].[ContentParameter]
WHERE
	[ContentParameterID] = @ContentParameterID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterGet
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterGet
	@ContentParameterID int
AS

SELECT
	[ContentParameterID],
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
FROM
	[dbo].[ContentParameter]
WHERE
	[ContentParameterID] = @ContentParameterID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterGetList
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentParameter (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentParameterID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentParameter ([ContentParameterID]) SELECT '
SET @sql = @sql + ' [ContentParameterID] FROM [dbo].[ContentParameter]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentParameter]

SELECT
	[dbo].[ContentParameter].[ContentParameterID],
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
FROM
	#TempContentParameter AS tblTemp JOIN [dbo].[ContentParameter] ON
	tblTemp.ContentParameterID = [dbo].[ContentParameter].ContentParameterID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempContentParameter

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterInsert
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterInsert
	@ContentParameterName nvarchar(127),
	@ContentParameterDescription nvarchar(127),
	@ContentParameterActive nvarchar(1),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentParameterID int output,
@ContentParameterValue nvarchar(255)
AS

INSERT INTO [dbo].[ContentParameter] 
(
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
) 
VALUES 
(
	@ContentParameterName,
	@ContentParameterDescription,
	@ContentParameterActive,
	@CreatedDate,
	@ModifiedDate,
	@ContentParameterValue
)

SELECT @ContentParameterID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterUpdate
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterUpdate
	@ContentParameterID int, 
	@ContentParameterName nvarchar(127), 
	@ContentParameterDescription nvarchar(127), 
	@ContentParameterActive nvarchar(1), 
	@CreatedDate datetime, 
	@ModifiedDate datetime ,
	@ContentParameterValue nvarchar(255)
AS

UPDATE [dbo].[ContentParameter] SET
	[ContentParameterName] = @ContentParameterName,
	[ContentParameterDescription] = @ContentParameterDescription,
	[ContentParameterActive] = @ContentParameterActive,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[ContentParameterValue] = @ContentParameterValue
WHERE
	[ContentParameterID] = @ContentParameterID


GO
/****** Object:  Stored Procedure [dbo].ContentTemplateDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateDelete]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateGet]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateGetList]
GO

	
/****** Object:  Stored Procedure [dbo].ContentTemplateInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateInsert]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateDelete
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateDelete
	@ContentTemplateID int
AS

DELETE FROM [dbo].[ContentTemplate]
WHERE
	[ContentTemplateID] = @ContentTemplateID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateGet
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateGet
	@ContentTemplateID int
AS

SELECT
	[ContentTemplateID],
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ContentTemplate]
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateGetList
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentTemplate (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentTemplateID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentTemplate ([ContentTemplateID]) SELECT '
SET @sql = @sql + ' [ContentTemplateID] FROM [dbo].[ContentTemplate]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentTemplate]

SELECT
	[dbo].[ContentTemplate].[ContentTemplateID],
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempContentTemplate AS tblTemp JOIN [dbo].[ContentTemplate] ON
	tblTemp.ContentTemplateID = [dbo].[ContentTemplate].ContentTemplateID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempContentTemplate

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateInsert
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateInsert
	@ServiceTypeID int,
	@Description nvarchar(400),
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@Subject nvarchar(255),
	@BodyContentType nvarchar(50),
	@BodyEncoding nvarchar(15),
	@BodyMessage nvarchar(4000),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentTemplateID int output
AS

INSERT INTO [dbo].[ContentTemplate] 
(
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@ServiceTypeID,
	@Description,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ContentTemplateID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateUpdate
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateUpdate
	@ContentTemplateID int, 
	@ServiceTypeID int, 
	@Description nvarchar(400), 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage nvarchar(4000), 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ContentTemplate] SET
	[ServiceTypeID] = @ServiceTypeID,
	[Description] = @Description,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
/****** Object:  Stored Procedure [dbo].MessageCommandDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandDelete]
GO

/****** Object:  Stored Procedure [dbo].MessageCommandGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandGet]
GO

/****** Object:  Stored Procedure [dbo].MessageCommandGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandGetList]
GO

	
/****** Object:  Stored Procedure [dbo].MessageCommandInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandInsert]
GO

/****** Object:  Stored Procedure [dbo].MessageCommandUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandDelete
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageCommandDelete
	@MessageCommandID int
AS

DELETE FROM [dbo].[MessageCommand]
WHERE
	[MessageCommandID] = @MessageCommandID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandGet
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageCommandGet
	@MessageCommandID int
AS

SELECT
	[MessageCommandID],
	[MessageContentID],
	[Status],
	[ProcessedDateTime],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageCommand]
WHERE
	[MessageCommandID] = @MessageCommandID

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
**		Date: 28/05/2009 16:59:45
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

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandInsert
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageCommandInsert
	@MessageContentID int,
	@Status int,
	@ProcessedDateTime datetime,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageCommandID int output
AS

INSERT INTO [dbo].[MessageCommand] 
(
	[MessageContentID],
	[Status],
	[ProcessedDateTime],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@MessageContentID,
	@Status,
	@ProcessedDateTime,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageCommandID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandUpdate
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
**		Date: 28/05/2009 16:59:45
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageCommandUpdate
	@MessageCommandID int, 
	@MessageContentID int, 
	@Status int, 
	@ProcessedDateTime datetime, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[MessageCommand] SET
	[MessageContentID] = @MessageContentID,
	[Status] = @Status,
	[ProcessedDateTime] = @ProcessedDateTime,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageCommandID] = @MessageCommandID

GO
/****** Object:  Stored Procedure [dbo].MessageContentDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentDelete]
GO

/****** Object:  Stored Procedure [dbo].MessageContentGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentGet]
GO

/****** Object:  Stored Procedure [dbo].MessageContentGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentGetList]
GO

	
/****** Object:  Stored Procedure [dbo].MessageContentInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentInsert]
GO

/****** Object:  Stored Procedure [dbo].MessageContentUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentDelete
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentDelete
	@MessageContentID int
AS

DELETE FROM [dbo].[MessageContent]
WHERE
	[MessageContentID] = @MessageContentID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentGet
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentGet
	@MessageContentID int
AS

SELECT
	[MessageContentID],
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
	[Status]
FROM
	[dbo].[MessageContent]
WHERE
	[MessageContentID] = @MessageContentID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentGetList
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentGetList
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
SET @sql = @sql + ' [MessageContentID] FROM [dbo].[MessageContent]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [MessageContent]

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
	[Status]
FROM
	#TempMessageContent AS tblTemp JOIN [dbo].[MessageContent] ON
	tblTemp.MessageContentID = [dbo].[MessageContent].MessageContentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempMessageContent

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentInsert
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentInsert
	@ContentTemplateID int,
	@ServiceTypeID int,
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@Subject nvarchar(255),
	@BodyContentType nvarchar(50),
	@BodyEncoding nvarchar(15),
	@BodyMessage nvarchar(4000),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageContentID int output,
	@Status int
AS

INSERT INTO [dbo].[MessageContent] 
(
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
	[Status]
) 
VALUES 
(
	@ContentTemplateID,
	@ServiceTypeID,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@CreatedDate,
	@ModifiedDate,
	@Status
)

SELECT @MessageContentID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentUpdate
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentUpdate
	@MessageContentID int, 
	@ContentTemplateID int, 
	@ServiceTypeID int, 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage nvarchar(4000), 
	@CreatedDate datetime, 
	@ModifiedDate datetime,
	@Status int
AS

UPDATE [dbo].[MessageContent] SET
	[ContentTemplateID] = @ContentTemplateID,
	[ServiceTypeID] = @ServiceTypeID,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[Status] = @Status
WHERE
	[MessageContentID] = @MessageContentID


GO
/****** Object:  Stored Procedure [dbo].MessageContentSentDelete    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentDelete]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentGet    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentGet]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentGetList    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentGetList]
GO

	
/****** Object:  Stored Procedure [dbo].MessageContentSentInsert    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentInsert]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentUpdate    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentDelete
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
**		Date: 06/06/2009 16:44:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentDelete
	@MessageContentSentID bigint
AS

DELETE FROM [dbo].[MessageContentSent]
WHERE
	[MessageContentSentID] = @MessageContentSentID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentGet
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
**		Date: 06/06/2009 16:44:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentGet
	@MessageContentSentID bigint
AS

SELECT
	[MessageContentID],
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
	[MessageContentSentID]
FROM
	[dbo].[MessageContentSent]
WHERE
	[MessageContentSentID] = @MessageContentSentID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentGetList
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
**		Date: 06/06/2009 16:44:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentGetList
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

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempMessageContentSent ([MessageContentSentID]) SELECT '
SET @sql = @sql + ' [MessageContentSentID] FROM [dbo].[MessageContentSent]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [MessageContentSent]

SELECT
	[MessageContentID],
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
	[dbo].[MessageContentSent].[MessageContentSentID]
FROM
	#TempMessageContentSent AS tblTemp JOIN [dbo].[MessageContentSent] ON
	tblTemp.MessageContentSentID = [dbo].[MessageContentSent].MessageContentSentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempMessageContentSent

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentInsert
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
**		Date: 06/06/2009 16:44:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentInsert
	@MessageContentID int,
	@ContentTemplateID int,
	@ServiceTypeID int,
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@Subject nvarchar(255),
	@BodyContentType nvarchar(50),
	@BodyEncoding nvarchar(15),
	@BodyMessage nvarchar(4000),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageContentSentID bigint output
AS

INSERT INTO [dbo].[MessageContentSent] 
(
	[MessageContentID],
	[ContentTemplateID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@MessageContentID,
	@ContentTemplateID,
	@ServiceTypeID,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageContentSentID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentUpdate
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
**		Date: 06/06/2009 16:44:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentUpdate
	@MessageContentID int, 
	@ContentTemplateID int, 
	@ServiceTypeID int, 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage nvarchar(4000), 
	@CreatedDate datetime, 
	@ModifiedDate datetime, 
	@MessageContentSentID bigint 
AS

UPDATE [dbo].[MessageContentSent] SET
	[MessageContentID] = @MessageContentID,
	[ContentTemplateID] = @ContentTemplateID,
	[ServiceTypeID] = @ServiceTypeID,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageContentSentID] = @MessageContentSentID

GO
/****** Object:  Stored Procedure [dbo].ServiceTypeDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeDelete]
GO

/****** Object:  Stored Procedure [dbo].ServiceTypeGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeGet]
GO

/****** Object:  Stored Procedure [dbo].ServiceTypeGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeGetList]
GO

	
/****** Object:  Stored Procedure [dbo].ServiceTypeInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeInsert]
GO

/****** Object:  Stored Procedure [dbo].ServiceTypeUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeDelete
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeDelete
	@ServiceTypeID int
AS

DELETE FROM [dbo].[ServiceType]
WHERE
	[ServiceTypeID] = @ServiceTypeID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeGet
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeGet
	@ServiceTypeID int
AS

SELECT
	[ServiceTypeID],
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ServiceType]
WHERE
	[ServiceTypeID] = @ServiceTypeID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeGetList
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempServiceType (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ServiceTypeID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempServiceType ([ServiceTypeID]) SELECT '
SET @sql = @sql + ' [ServiceTypeID] FROM [dbo].[ServiceType]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ServiceType]

SELECT
	[dbo].[ServiceType].[ServiceTypeID],
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempServiceType AS tblTemp JOIN [dbo].[ServiceType] ON
	tblTemp.ServiceTypeID = [dbo].[ServiceType].ServiceTypeID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempServiceType

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeInsert
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeInsert
	@ServiceTypeDescription nvarchar(50),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ServiceTypeID int output
AS

INSERT INTO [dbo].[ServiceType] 
(
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@ServiceTypeDescription,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ServiceTypeID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeUpdate
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
**		Date: 28/05/2009 16:59:46
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeUpdate
	@ServiceTypeID int, 
	@ServiceTypeDescription nvarchar(50), 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ServiceType] SET
	[ServiceTypeDescription] = @ServiceTypeDescription,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ServiceTypeID] = @ServiceTypeID

GO

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

SELECT @TotalRecords = COUNT(*) FROM [MessageContent]

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
	[Status]
FROM
	#TempMessageContent AS tblTemp JOIN [dbo].[MessageContent] ON
	tblTemp.MessageContentID = [dbo].[MessageContent].MessageContentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND Status = @Status
ORDER BY RowNumber

DROP TABLE #TempMessageContent

GO


/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementDelete]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementGet]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementGetList]
GO

	
/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementInsert    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementInsert]
GO

/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementUpdate    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementDelete
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementDelete
	@ContentTemplateAttachementID int
AS

DELETE FROM [dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementGet
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementGet
	@ContentTemplateAttachementID int
AS

SELECT
	[ContentTemplateAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementGetList
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentTemplateAttachement (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentTemplateAttachementID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentTemplateAttachement ([ContentTemplateAttachementID]) SELECT '
SET @sql = @sql + ' [ContentTemplateAttachementID] FROM [dbo].[ContentTemplateAttachement]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentTemplateAttachement]

SELECT
	[dbo].[ContentTemplateAttachement].[ContentTemplateAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempContentTemplateAttachement AS tblTemp JOIN [dbo].[ContentTemplateAttachement] ON
	tblTemp.ContentTemplateAttachementID = [dbo].[ContentTemplateAttachement].ContentTemplateAttachementID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempContentTemplateAttachement

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementInsert
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementInsert
	@AttachementDocument nvarchar(1024),
	@AttachementDescription nvarchar(500),
	@ContentTemplateID int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentTemplateAttachementID int output
AS

INSERT INTO [dbo].[ContentTemplateAttachement] 
(
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@AttachementDocument,
	@AttachementDescription,
	@ContentTemplateID,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ContentTemplateAttachementID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementUpdate
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementUpdate
	@ContentTemplateAttachementID int, 
	@AttachementDocument nvarchar(1024), 
	@AttachementDescription nvarchar(500), 
	@ContentTemplateID int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ContentTemplateAttachement] SET
	[AttachementDocument] = @AttachementDocument,
	[AttachementDescription] = @AttachementDescription,
	[ContentTemplateID] = @ContentTemplateID,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID

GO
/****** Object:  Stored Procedure [dbo].MessageContentAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementDelete]
GO

/****** Object:  Stored Procedure [dbo].MessageContentAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementGet]
GO

/****** Object:  Stored Procedure [dbo].MessageContentAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementGetList]
GO

	
/****** Object:  Stored Procedure [dbo].MessageContentAttachementInsert    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementInsert]
GO

/****** Object:  Stored Procedure [dbo].MessageContentAttachementUpdate    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementDelete
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementDelete
	@MessageContentAttachementID int
AS

DELETE FROM [dbo].[MessageContentAttachement]
WHERE
	[MessageContentAttachementID] = @MessageContentAttachementID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementGet
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementGet
	@MessageContentAttachementID int
AS

SELECT
	[MessageContentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageContentAttachement]
WHERE
	[MessageContentAttachementID] = @MessageContentAttachementID

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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementGetList
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
SET @sql = @sql + ' [MessageContentAttachementID] FROM [dbo].[MessageContentAttachement]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [MessageContentAttachement]

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
ORDER BY RowNumber

DROP TABLE #TempMessageContentAttachement

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementInsert
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementInsert
	@AttachementDocument nvarchar(1024),
	@AttachementDescription nvarchar(500),
	@MessageContentID int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageContentAttachementID int output
AS

INSERT INTO [dbo].[MessageContentAttachement] 
(
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@AttachementDocument,
	@AttachementDescription,
	@MessageContentID,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageContentAttachementID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementUpdate
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementUpdate
	@MessageContentAttachementID int, 
	@AttachementDocument nvarchar(1024), 
	@AttachementDescription nvarchar(500), 
	@MessageContentID int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[MessageContentAttachement] SET
	[AttachementDocument] = @AttachementDocument,
	[AttachementDescription] = @AttachementDescription,
	[MessageContentID] = @MessageContentID,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageContentAttachementID] = @MessageContentAttachementID

GO
/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementDelete]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementGet]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementGetList]
GO

	
/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementInsert    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementInsert]
GO

/****** Object:  Stored Procedure [dbo].MessageContentSentAttachementUpdate    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentAttachementUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentAttachementUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementDelete
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentAttachementDelete
	@MessageContentSentAttachementID int
AS

DELETE FROM [dbo].[MessageContentSentAttachement]
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementGet
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentAttachementGet
	@MessageContentSentAttachementID int
AS

SELECT
	[MessageContentSentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageContentSentAttachement]
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID

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
**		Date: 09/06/2009 16:46:08
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

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementInsert
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentAttachementInsert
	@AttachementDocument nvarchar(1024),
	@AttachementDescription nvarchar(500),
	@MessageContentID int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageContentSentAttachementID int output
AS

INSERT INTO [dbo].[MessageContentSentAttachement] 
(
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@AttachementDocument,
	@AttachementDescription,
	@MessageContentID,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageContentSentAttachementID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentAttachementUpdate
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
**		Date: 09/06/2009 16:46:08
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentAttachementUpdate
	@MessageContentSentAttachementID int, 
	@AttachementDocument nvarchar(1024), 
	@AttachementDescription nvarchar(500), 
	@MessageContentID int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[MessageContentSentAttachement] SET
	[AttachementDocument] = @AttachementDocument,
	[AttachementDescription] = @AttachementDescription,
	[MessageContentID] = @MessageContentID,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageContentSentAttachementID] = @MessageContentSentAttachementID

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[spExistServiceTypeIdForMessageContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spExistServiceTypeIdForMessageContent
GO
	
CREATE PROCEDURE spExistServiceTypeIdForMessageContent
@ServiceTypeID int 
AS

SELECT 
	MessageContentID,
	ContentTemplateID,
	ServiceTypeID,
	Sender,
	Receiver,
	Subject,
	BodyContentType,
	BodyEncoding,
	BodyMessage,
	CreatedDate,
	ModifiedDate,
	Status
FROM MessageContent
WHERE ServiceTypeID = @ServiceTypeID

GO

 /****** Object:  Stored Procedure [dbo].ServiceTypeDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[spExistServiceTypeIdForServiceType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spExistServiceTypeIdForServiceType
GO
	
CREATE PROCEDURE spExistServiceTypeIdForServiceType
	@ServiceTypeDescription nvarchar(50)
AS

SELECT 	
	ServiceTypeID,
	ServiceTypeDescription,
	CreatedDate,
	ModifiedDate
FROM ServiceType
WHERE ServiceTypeDescription = @ServiceTypeDescription

/****** Object:  Stored Procedure [dbo].ContentTemplateGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExistContentTemplateByContentTemplate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spExistContentTemplateByContentTemplate
GO

CREATE PROCEDURE [dbo].spExistContentTemplateByContentTemplate
	@Description nvarchar(400)
AS

SELECT
	[ContentTemplateID],
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ContentTemplate]
WHERE
	[Description] = @Description

GO
	
/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateByContentTemplateAttachement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spContentTemplateByContentTemplateAttachement
GO


CREATE PROCEDURE [dbo].spContentTemplateByContentTemplateAttachement
	@ContentTemplateID int
AS

SELECT
	[ContentTemplateAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
	

	
/*
ngay 23/06/2009
*/
	
 /****** Object:  Stored Procedure [dbo].MessageContentGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGetListFilterByServiceTypeID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spMessageContentGetListFilterByServiceTypeID
GO

CREATE PROCEDURE [dbo].spMessageContentGetListFilterByServiceTypeID
	@ServiceTypeID int,
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@ModifiedDate datetime,
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
					AND (CONVERT(nvarchar(10),[MessageContent].ModifiedDate,112) = '+''''+ CONVERT(nvarchar(10),@ModifiedDate,112)+''''+' OR '+''''+ CONVERT(nvarchar(10),@ModifiedDate,112)+''''+' = ''19000101'')
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
	[Status]
FROM
	#TempMessageContent AS tblTemp JOIN [dbo].[MessageContent] ON
	tblTemp.MessageContentID = [dbo].[MessageContent].MessageContentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempMessageContent

GO

	



	
  /****** Object:  Stored Procedure [dbo].MessageContentSentGetList    Script Date: 06 June 2009 ******/
 /****** Object:  Stored Procedure [dbo].MessageContentSentGetList    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentGetListFilterByServiceTypeID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spMessageContentSentGetListFilterByServiceTypeID
GO

	
	
CREATE PROCEDURE [dbo].spMessageContentSentGetListFilterByServiceTypeID
	@ServiceTypeID int,	
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@ModifiedDate datetime,
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
					AND (CONVERT(nvarchar(10),[MessageContentSent].ModifiedDate,112) = '+''''+ CONVERT(nvarchar(10),@ModifiedDate,112)+''''+' OR '+''''+ CONVERT(nvarchar(10),@ModifiedDate,112)+''''+' = ''19000101'')
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
	[MessageContentID],
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
	[dbo].[MessageContentSent].[MessageContentSentID]
FROM
	#TempMessageContentSent AS tblTemp JOIN [dbo].[MessageContentSent] ON
	tblTemp.MessageContentSentID = [dbo].[MessageContentSent].MessageContentSentID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)	
ORDER BY RowNumber

DROP TABLE #TempMessageContentSent

GO

	


	



	
