/****** Object:  Stored Procedure [dbo].sysdiagramsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsDelete]
GO

/****** Object:  Stored Procedure [dbo].sysdiagramsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsGet]
GO

/****** Object:  Stored Procedure [dbo].sysdiagramsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].sysdiagramsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsInsert]
GO

/****** Object:  Stored Procedure [dbo].sysdiagramsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spsysdiagramsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spsysdiagramsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsDelete
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spsysdiagramsDelete
	@diagram_id int
AS

DELETE FROM [dbo].[sysdiagrams]
WHERE
	[diagram_id] = @diagram_id
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsGet
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spsysdiagramsGet
	@diagram_id int
AS

SELECT
	[name],
	[principal_id],
	[diagram_id],
	[version],
	[definition]
FROM
	[dbo].[sysdiagrams]
WHERE
	[diagram_id] = @diagram_id

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsGetList
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spsysdiagramsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempsysdiagrams (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	diagram_id int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempsysdiagrams ([diagram_id]) SELECT '
SET @sql = @sql + ' [diagram_id] FROM [dbo].[sysdiagrams]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [sysdiagrams]

SELECT
	[name],
	[principal_id],
	[dbo].[sysdiagrams].[diagram_id],
	[version],
	[definition]
FROM
	#Tempsysdiagrams AS tblTemp JOIN [dbo].[sysdiagrams] ON
	tblTemp.diagram_id = [dbo].[sysdiagrams].diagram_id 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempsysdiagrams

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsInsert
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spsysdiagramsInsert
	@name nvarchar(128),
	@principal_id int,
	@version int,
	@definition varbinary,
	@diagram_id int output
AS

INSERT INTO [dbo].[sysdiagrams] 
(
	[name],
	[principal_id],
	[version],
	[definition]
) 
VALUES 
(
	@name,
	@principal_id,
	@version,
	@definition
)

SELECT @diagram_id = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spsysdiagramsUpdate
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spsysdiagramsUpdate
	@name nvarchar(128), 
	@principal_id int, 
	@diagram_id int, 
	@version int, 
	@definition varbinary 
AS

UPDATE [dbo].[sysdiagrams] SET
	[name] = @name,
	[principal_id] = @principal_id,
	[version] = @version,
	[definition] = @definition
WHERE
	[diagram_id] = @diagram_id

GO
/****** Object:  Stored Procedure [dbo].SourceDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceDelete]
GO

/****** Object:  Stored Procedure [dbo].SourceGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceGet]
GO

/****** Object:  Stored Procedure [dbo].SourceGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceGetList]
GO

	
/****** Object:  Stored Procedure [dbo].SourceInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceInsert]
GO

/****** Object:  Stored Procedure [dbo].SourceUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSourceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spSourceUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spSourceDelete
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceDelete
	@SourceId int
AS

DELETE FROM [dbo].[Source]
WHERE
	[SourceId] = @SourceId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceGet
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceGet
	@SourceId int
AS

SELECT
	[SourceId],
	[SiteName],
	[URL]
FROM
	[dbo].[Source]
WHERE
	[SourceId] = @SourceId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceGetList
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempSource (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	SourceId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempSource ([SourceId]) SELECT '
SET @sql = @sql + ' [SourceId] FROM [dbo].[Source]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Source]

SELECT
	[dbo].[Source].[SourceId],
	[SiteName],
	[URL]
FROM
	#TempSource AS tblTemp JOIN [dbo].[Source] ON
	tblTemp.SourceId = [dbo].[Source].SourceId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempSource

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spSourceInsert
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceInsert
	@SiteName nvarchar(256),
	@URL nvarchar(256),
	@SourceId int output
AS

INSERT INTO [dbo].[Source] 
(
	[SiteName],
	[URL]
) 
VALUES 
(
	@SiteName,
	@URL
)

SELECT @SourceId = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spSourceUpdate
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spSourceUpdate
	@SourceId int, 
	@SiteName nvarchar(256), 
	@URL nvarchar(256) 
AS

UPDATE [dbo].[Source] SET
	[SiteName] = @SiteName,
	[URL] = @URL
WHERE
	[SourceId] = @SourceId

GO
/****** Object:  Stored Procedure [dbo].LinkDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkDelete]
GO

/****** Object:  Stored Procedure [dbo].LinkGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkGet]
GO

/****** Object:  Stored Procedure [dbo].LinkGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkGetList]
GO

	
/****** Object:  Stored Procedure [dbo].LinkInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkInsert]
GO

/****** Object:  Stored Procedure [dbo].LinkUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLinkUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLinkUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spLinkDelete
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkDelete
	@LinkId int
AS

DELETE FROM [dbo].[Link]
WHERE
	[LinkId] = @LinkId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkGet
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkGet
	@LinkId int
AS

SELECT
	[LinkId],
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
FROM
	[dbo].[Link]
WHERE
	[LinkId] = @LinkId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkGetList
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempLink (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	LinkId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempLink ([LinkId]) SELECT '
SET @sql = @sql + ' [LinkId] FROM [dbo].[Link]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Link]

SELECT
	[dbo].[Link].[LinkId],
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
FROM
	#TempLink AS tblTemp JOIN [dbo].[Link] ON
	tblTemp.LinkId = [dbo].[Link].LinkId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempLink

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spLinkInsert
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkInsert
	@SourceId int,
	@Link nvarchar(256),
	@LinkShortDescription nvarchar(256),
	@LinkDescription nvarchar(256),
	@LinkId int output
AS

INSERT INTO [dbo].[Link] 
(
	[SourceId],
	[Link],
	[LinkShortDescription],
	[LinkDescription]
) 
VALUES 
(
	@SourceId,
	@Link,
	@LinkShortDescription,
	@LinkDescription
)

SELECT @LinkId = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spLinkUpdate
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spLinkUpdate
	@LinkId int, 
	@SourceId int, 
	@Link nvarchar(256), 
	@LinkShortDescription nvarchar(256), 
	@LinkDescription nvarchar(256) 
AS

UPDATE [dbo].[Link] SET
	[SourceId] = @SourceId,
	[Link] = @Link,
	[LinkShortDescription] = @LinkShortDescription,
	[LinkDescription] = @LinkDescription
WHERE
	[LinkId] = @LinkId

GO
/****** Object:  Stored Procedure [dbo].StockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsDelete]
GO

/****** Object:  Stored Procedure [dbo].StockNewsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsGet]
GO

/****** Object:  Stored Procedure [dbo].StockNewsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].StockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsInsert]
GO

/****** Object:  Stored Procedure [dbo].StockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsDelete
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsDelete
	@NewsId int
AS

DELETE FROM [dbo].[StockNews]
WHERE
	[NewsId] = @NewsId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsGet
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsGet
	@NewsId int
AS

SELECT
	[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[LinkId],
	[OriginalUrl],
	[FeedDate]
FROM
	[dbo].[StockNews]
WHERE
	[NewsId] = @NewsId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsGetList
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempStockNews (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempStockNews ([NewsId]) SELECT '
SET @sql = @sql + ' [NewsId] FROM [dbo].[StockNews]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [StockNews]

SELECT
	[dbo].[StockNews].[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[LinkId],
	[OriginalUrl],
	[FeedDate]
FROM
	#TempStockNews AS tblTemp JOIN [dbo].[StockNews] ON
	tblTemp.NewsId = [dbo].[StockNews].NewsId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempStockNews

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsInsert
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsInsert
	@NewsTitle nvarchar(256),
	@NewsDescription nvarchar(1024),
	@NewsContent ntext,
	@NewsDate datetime,
	@NewsSource nvarchar(256),
	@ShareSymbol nvarchar(10),
	@UseUrl bit,
	@NewsUrl nvarchar(256),
	@LanguageID int,
	@IsApproved int,
	@ImageUrl nvarchar(256),
	@LinkId int,
	@OriginalUrl nvarchar(256),
	@FeedDate datetime,
	@NewsId int output
AS

INSERT INTO [dbo].[StockNews] 
(
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[LinkId],
	[OriginalUrl],
	[FeedDate]
) 
VALUES 
(
	@NewsTitle,
	@NewsDescription,
	@NewsContent,
	@NewsDate,
	@NewsSource,
	@ShareSymbol,
	@UseUrl,
	@NewsUrl,
	@LanguageID,
	@IsApproved,
	@ImageUrl,
	@LinkId,
	@OriginalUrl,
	@FeedDate
)

SELECT @NewsId = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spStockNewsUpdate
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
**		Date: 10/02/2009 18:23:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsUpdate
	@NewsId int, 
	@NewsTitle nvarchar(256), 
	@NewsDescription nvarchar(1024), 
	@NewsContent ntext, 
	@NewsDate datetime, 
	@NewsSource nvarchar(256), 
	@ShareSymbol nvarchar(10), 
	@UseUrl bit, 
	@NewsUrl nvarchar(256), 
	@LanguageID int, 
	@IsApproved int, 
	@ImageUrl nvarchar(256), 
	@LinkId int, 
	@OriginalUrl nvarchar(256), 
	@FeedDate datetime 
AS

UPDATE [dbo].[StockNews] SET
	[NewsTitle] = @NewsTitle,
	[NewsDescription] = @NewsDescription,
	[NewsContent] = @NewsContent,
	[NewsDate] = @NewsDate,
	[NewsSource] = @NewsSource,
	[ShareSymbol] = @ShareSymbol,
	[UseUrl] = @UseUrl,
	[NewsUrl] = @NewsUrl,
	[LanguageID] = @LanguageID,
	[IsApproved] = @IsApproved,
	[ImageUrl] = @ImageUrl,
	[LinkId] = @LinkId,
	[OriginalUrl] = @OriginalUrl,
	[FeedDate] = @FeedDate
WHERE
	[NewsId] = @NewsId

GO
/****** Object:  Stored Procedure [dbo].ApprovedStockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsDelete]
GO

/****** Object:  Stored Procedure [dbo].ApprovedStockNewsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsGet]
GO

/****** Object:  Stored Procedure [dbo].ApprovedStockNewsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].ApprovedStockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsInsert]
GO

/****** Object:  Stored Procedure [dbo].ApprovedStockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsDelete
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsDelete
	@NewsId int
AS

DELETE FROM [dbo].[ApprovedStockNews]
WHERE
	[NewsId] = @NewsId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsGet
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsGet
	@NewsId int
AS

SELECT
	[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[Comment],
	[LinkId],
	[OriginalUrl],
	[ApprovedDate]
FROM
	[dbo].[ApprovedStockNews]
WHERE
	[NewsId] = @NewsId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsGetList
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempApprovedStockNews (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempApprovedStockNews ([NewsId]) SELECT '
SET @sql = @sql + ' [NewsId] FROM [dbo].[ApprovedStockNews]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ApprovedStockNews]

SELECT
	[dbo].[ApprovedStockNews].[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[Comment],
	[LinkId],
	[OriginalUrl],
	[ApprovedDate]
FROM
	#TempApprovedStockNews AS tblTemp JOIN [dbo].[ApprovedStockNews] ON
	tblTemp.NewsId = [dbo].[ApprovedStockNews].NewsId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempApprovedStockNews

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsInsert
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsInsert
	@NewsTitle nvarchar(256),
	@NewsDescription nvarchar(1024),
	@NewsContent ntext,
	@NewsDate datetime,
	@NewsSource nvarchar(256),
	@ShareSymbol nvarchar(10),
	@UseUrl bit,
	@NewsUrl nvarchar(256),
	@LanguageID int,
	@IsApproved int,
	@ImageUrl nvarchar(256),
	@Comment nvarchar(256),
	@LinkId int,
	@OriginalUrl nvarchar(256),
	@ApprovedDate datetime,
	@NewsId int output
AS

INSERT INTO [dbo].[ApprovedStockNews] 
(
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[Comment],
	[LinkId],
	[OriginalUrl],
	[ApprovedDate]
) 
VALUES 
(
	@NewsTitle,
	@NewsDescription,
	@NewsContent,
	@NewsDate,
	@NewsSource,
	@ShareSymbol,
	@UseUrl,
	@NewsUrl,
	@LanguageID,
	@IsApproved,
	@ImageUrl,
	@Comment,
	@LinkId,
	@OriginalUrl,
	@ApprovedDate
)

SELECT @NewsId = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsUpdate
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsUpdate
	@NewsId int, 
	@NewsTitle nvarchar(256), 
	@NewsDescription nvarchar(1024), 
	@NewsContent ntext, 
	@NewsDate datetime, 
	@NewsSource nvarchar(256), 
	@ShareSymbol nvarchar(10), 
	@UseUrl bit, 
	@NewsUrl nvarchar(256), 
	@LanguageID int, 
	@IsApproved int, 
	@ImageUrl nvarchar(256), 
	@Comment nvarchar(256), 
	@LinkId int, 
	@OriginalUrl nvarchar(256), 
	@ApprovedDate datetime 
AS

UPDATE [dbo].[ApprovedStockNews] SET
	[NewsTitle] = @NewsTitle,
	[NewsDescription] = @NewsDescription,
	[NewsContent] = @NewsContent,
	[NewsDate] = @NewsDate,
	[NewsSource] = @NewsSource,
	[ShareSymbol] = @ShareSymbol,
	[UseUrl] = @UseUrl,
	[NewsUrl] = @NewsUrl,
	[LanguageID] = @LanguageID,
	[IsApproved] = @IsApproved,
	[ImageUrl] = @ImageUrl,
	[Comment] = @Comment,
	[LinkId] = @LinkId,
	[OriginalUrl] = @OriginalUrl,
	[ApprovedDate] = @ApprovedDate
WHERE
	[NewsId] = @NewsId

GO
/****** Object:  Stored Procedure [dbo].RejectedStockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsDelete]
GO

/****** Object:  Stored Procedure [dbo].RejectedStockNewsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsGet]
GO

/****** Object:  Stored Procedure [dbo].RejectedStockNewsGetList    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].RejectedStockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsInsert]
GO

/****** Object:  Stored Procedure [dbo].RejectedStockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsDelete
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsDelete
	@NewsId int
AS

DELETE FROM [dbo].[RejectedStockNews]
WHERE
	[NewsId] = @NewsId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsGet
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsGet
	@NewsId int
AS

SELECT
	[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[RejectedReason],
	[LinkId],
	[OriginalUrl],
	[RejectedDate]
FROM
	[dbo].[RejectedStockNews]
WHERE
	[NewsId] = @NewsId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsGetList
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempRejectedStockNews (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsId int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempRejectedStockNews ([NewsId]) SELECT '
SET @sql = @sql + ' [NewsId] FROM [dbo].[RejectedStockNews]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [RejectedStockNews]

SELECT
	[dbo].[RejectedStockNews].[NewsId],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[RejectedReason],
	[LinkId],
	[OriginalUrl],
	[RejectedDate]
FROM
	#TempRejectedStockNews AS tblTemp JOIN [dbo].[RejectedStockNews] ON
	tblTemp.NewsId = [dbo].[RejectedStockNews].NewsId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempRejectedStockNews

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsInsert
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsInsert
	@NewsTitle nvarchar(256),
	@NewsDescription nvarchar(1024),
	@NewsContent ntext,
	@NewsDate datetime,
	@NewsSource nvarchar(256),
	@ShareSymbol nvarchar(10),
	@UseUrl bit,
	@NewsUrl nvarchar(256),
	@LanguageID int,
	@IsApproved int,
	@ImageUrl nvarchar(256),
	@RejectedReason nvarchar(256),
	@LinkId int,
	@OriginalUrl nvarchar(256),
	@RejectedDate datetime,
	@NewsId int output
AS

INSERT INTO [dbo].[RejectedStockNews] 
(
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[ShareSymbol],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl],
	[RejectedReason],
	[LinkId],
	[OriginalUrl],
	[RejectedDate]
) 
VALUES 
(
	@NewsTitle,
	@NewsDescription,
	@NewsContent,
	@NewsDate,
	@NewsSource,
	@ShareSymbol,
	@UseUrl,
	@NewsUrl,
	@LanguageID,
	@IsApproved,
	@ImageUrl,
	@RejectedReason,
	@LinkId,
	@OriginalUrl,
	@RejectedDate
)

SELECT @NewsId = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsUpdate
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
**		Date: 10/02/2009 18:23:35
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsUpdate
	@NewsId int, 
	@NewsTitle nvarchar(256), 
	@NewsDescription nvarchar(1024), 
	@NewsContent ntext, 
	@NewsDate datetime, 
	@NewsSource nvarchar(256), 
	@ShareSymbol nvarchar(10), 
	@UseUrl bit, 
	@NewsUrl nvarchar(256), 
	@LanguageID int, 
	@IsApproved int, 
	@ImageUrl nvarchar(256), 
	@RejectedReason nvarchar(256), 
	@LinkId int, 
	@OriginalUrl nvarchar(256), 
	@RejectedDate datetime 
AS

UPDATE [dbo].[RejectedStockNews] SET
	[NewsTitle] = @NewsTitle,
	[NewsDescription] = @NewsDescription,
	[NewsContent] = @NewsContent,
	[NewsDate] = @NewsDate,
	[NewsSource] = @NewsSource,
	[ShareSymbol] = @ShareSymbol,
	[UseUrl] = @UseUrl,
	[NewsUrl] = @NewsUrl,
	[LanguageID] = @LanguageID,
	[IsApproved] = @IsApproved,
	[ImageUrl] = @ImageUrl,
	[RejectedReason] = @RejectedReason,
	[LinkId] = @LinkId,
	[OriginalUrl] = @OriginalUrl,
	[RejectedDate] = @RejectedDate
WHERE
	[NewsId] = @NewsId

GO

/****** Object:  Stored Procedure [dbo].CustomerDelete    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerDelete]
GO

/****** Object:  Stored Procedure [dbo].CustomerGet    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerGet]
GO

/****** Object:  Stored Procedure [dbo].CustomerGetList    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerGetList]
GO

	
/****** Object:  Stored Procedure [dbo].CustomerInsert    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerInsert]
GO

/****** Object:  Stored Procedure [dbo].CustomerUpdate    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerDelete
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
**		Date: 10/08/2009 15:29:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerDelete
	@CustomerId varchar(10)
AS

DELETE FROM [dbo].[Customer]
WHERE
	[CustomerId] = @CustomerId
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerGet
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
**		Date: 10/08/2009 15:29:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerGet
	@CustomerId varchar(10)
AS

SELECT
	[BranchCode],
	[ContractNumber],
	[CustomerId],
	[BrokerId],
	[CustomerName],
	[CustomerNameViet],
	[CustomerType],
	[DomesticForeign],
	[Dob],
	[Sex],
	[SignatureImage1],
	[SignatureImage2],
	[OpenDate],
	[CloseDate],
	[CardType],
	[CardIdentity],
	[CardDate],
	[CardIssuer],
	[Address],
	[AddressViet],
	[Tel],
	[Fax],
	[Mobile],
	[Mobile2],
	[Email],
	[UserCreate],
	[DateCreate],
	[UserModify],
	[DateModify],
	[ProxyStatus],
	[AccountStatus],
	[Notes],
	[WorkingAddress],
	[UserIntroduce],
	[AttitudePoint],
	[DepositPoint],
	[ActionPoint],
	[Country],
	[Website],
	[TaxCode],
	[AccountType],
	[OrderType],
	[ReceiveReport],
	[ReceiveReportBy],
	[MarriageStatus],
	[KnowledgeLevel],
	[Job],
	[OfficeFunction],
	[OfficeTel],
	[OfficeFax],
	[HusbandWifeName],
	[HusbandWifeCardNumber],
	[HusbandWifeCardDate],
	[HusbandWifeCardLocation],
	[HusbandWifeTel],
	[HusbandWifeEmail],
	[JoinStockMarket],
	[InvestKnowledge],
	[InvestedIn],
	[InvestTarget],
	[RiskAccepted],
	[InvestFund],
	[DelegatePersonName],
	[DelegatePersonFunction],
	[DelegatePersonCardNumber],
	[DelegateCardDate],
	[DelegateCardLocation],
	[DelegatePersonTel],
	[DelegatePersonEmail],
	[ChiefAccountancyName],
	[ChiefAccountancyCI],
	[ChiefAccountancyCD],
	[ChiefAccountancyIssuer],
	[ChiefAccountancySign1],
	[ChiefAccountancySign2],
	[CaProxyName],
	[CaProxyCI],
	[CaProxyCD],
	[CaProxyIssuer],
	[CaProxySign1],
	[CaProxySign2],
	[CompanySign1],
	[CompanySign2],
	[TradeCode],
	[CustomerAccount],
	[MobileSms],
	[IsHere],
	[MoneyDepositeNumber],
	[MoneyDepositeLocation],
	[DepartmentId],
	[PublicCompanyManage],
	[PublicCompanyHolder],
	[ParentCompanyName],
	[ParentCompanyAddress],
	[ParentCompanyEmail],
	[ParentCompanyTel],
	[PostType],
	[ReOpenDate],
	[UserTakeCared],
	[TypeID]
FROM
	[dbo].[Customer]
WHERE
	[CustomerId] = @CustomerId

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerGetList
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
**		Date: 10/08/2009 15:29:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempCustomer (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	CustomerId varchar(10)	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomer ([CustomerId]) SELECT '
SET @sql = @sql + ' [CustomerId] FROM [dbo].[Customer]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Customer]

SELECT
	[BranchCode],
	[ContractNumber],
	[dbo].[Customer].[CustomerId],
	[BrokerId],
	[CustomerName],
	[CustomerNameViet],
	[CustomerType],
	[DomesticForeign],
	[Dob],
	[Sex],
	[SignatureImage1],
	[SignatureImage2],
	[OpenDate],
	[CloseDate],
	[CardType],
	[CardIdentity],
	[CardDate],
	[CardIssuer],
	[Address],
	[AddressViet],
	[Tel],
	[Fax],
	[Mobile],
	[Mobile2],
	[Email],
	[UserCreate],
	[DateCreate],
	[UserModify],
	[DateModify],
	[ProxyStatus],
	[AccountStatus],
	[Notes],
	[WorkingAddress],
	[UserIntroduce],
	[AttitudePoint],
	[DepositPoint],
	[ActionPoint],
	[Country],
	[Website],
	[TaxCode],
	[AccountType],
	[OrderType],
	[ReceiveReport],
	[ReceiveReportBy],
	[MarriageStatus],
	[KnowledgeLevel],
	[Job],
	[OfficeFunction],
	[OfficeTel],
	[OfficeFax],
	[HusbandWifeName],
	[HusbandWifeCardNumber],
	[HusbandWifeCardDate],
	[HusbandWifeCardLocation],
	[HusbandWifeTel],
	[HusbandWifeEmail],
	[JoinStockMarket],
	[InvestKnowledge],
	[InvestedIn],
	[InvestTarget],
	[RiskAccepted],
	[InvestFund],
	[DelegatePersonName],
	[DelegatePersonFunction],
	[DelegatePersonCardNumber],
	[DelegateCardDate],
	[DelegateCardLocation],
	[DelegatePersonTel],
	[DelegatePersonEmail],
	[ChiefAccountancyName],
	[ChiefAccountancyCI],
	[ChiefAccountancyCD],
	[ChiefAccountancyIssuer],
	[ChiefAccountancySign1],
	[ChiefAccountancySign2],
	[CaProxyName],
	[CaProxyCI],
	[CaProxyCD],
	[CaProxyIssuer],
	[CaProxySign1],
	[CaProxySign2],
	[CompanySign1],
	[CompanySign2],
	[TradeCode],
	[CustomerAccount],
	[MobileSms],
	[IsHere],
	[MoneyDepositeNumber],
	[MoneyDepositeLocation],
	[DepartmentId],
	[PublicCompanyManage],
	[PublicCompanyHolder],
	[ParentCompanyName],
	[ParentCompanyAddress],
	[ParentCompanyEmail],
	[ParentCompanyTel],
	[PostType],
	[ReOpenDate],
	[UserTakeCared],
	[TypeID]
FROM
	#TempCustomer AS tblTemp JOIN [dbo].[Customer] ON
	tblTemp.CustomerId = [dbo].[Customer].CustomerId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempCustomer

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerInsert
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
**		Date: 10/08/2009 15:29:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerInsert
	@BranchCode varchar(3),
	@ContractNumber varchar(20),
	@CustomerId varchar(10),
	@BrokerId int,
	@CustomerName varchar(100),
	@CustomerNameViet nvarchar(100),
	@CustomerType char(1),
	@DomesticForeign char(1),
	@Dob smalldatetime,
	@Sex char(1),
	@SignatureImage1 image,
	@SignatureImage2 image,
	@OpenDate smalldatetime,
	@CloseDate smalldatetime,
	@CardType int,
	@CardIdentity varchar(20),
	@CardDate smalldatetime,
	@CardIssuer varchar(200),
	@Address varchar(200),
	@AddressViet nvarchar(200),
	@Tel varchar(20),
	@Fax varchar(20),
	@Mobile varchar(20),
	@Mobile2 varchar(20),
	@Email varchar(100),
	@UserCreate int,
	@DateCreate smalldatetime,
	@UserModify int,
	@DateModify smalldatetime,
	@ProxyStatus bit,
	@AccountStatus char(1),
	@Notes nvarchar(250),
	@WorkingAddress nvarchar(200),
	@UserIntroduce int,
	@AttitudePoint int,
	@DepositPoint int,
	@ActionPoint int,
	@Country varchar(50),
	@Website nvarchar(200),
	@TaxCode varchar(50),
	@AccountType char(2),
	@OrderType char(3),
	@ReceiveReport char(2),
	@ReceiveReportBy char(2),
	@MarriageStatus char(1),
	@KnowledgeLevel char(1),
	@Job char(1),
	@OfficeFunction nvarchar(50),
	@OfficeTel varchar(20),
	@OfficeFax varchar(20),
	@HusbandWifeName nvarchar(100),
	@HusbandWifeCardNumber varchar(20),
	@HusbandWifeCardDate smalldatetime,
	@HusbandWifeCardLocation nvarchar(150),
	@HusbandWifeTel nvarchar(50),
	@HusbandWifeEmail nvarchar(50),
	@JoinStockMarket varchar(4),
	@InvestKnowledge char(1),
	@InvestedIn char(5),
	@InvestTarget char(1),
	@RiskAccepted char(1),
	@InvestFund char(1),
	@DelegatePersonName nvarchar(100),
	@DelegatePersonFunction nvarchar(50),
	@DelegatePersonCardNumber varchar(20),
	@DelegateCardDate smalldatetime,
	@DelegateCardLocation nvarchar(50),
	@DelegatePersonTel varchar(20),
	@DelegatePersonEmail nvarchar(100),
	@ChiefAccountancyName nvarchar(100),
	@ChiefAccountancyCI varchar(20),
	@ChiefAccountancyCD smalldatetime,
	@ChiefAccountancyIssuer varchar(20),
	@ChiefAccountancySign1 image,
	@ChiefAccountancySign2 image,
	@CaProxyName nvarchar(100),
	@CaProxyCI varchar(20),
	@CaProxyCD smalldatetime,
	@CaProxyIssuer varchar(20),
	@CaProxySign1 image,
	@CaProxySign2 image,
	@CompanySign1 image,
	@CompanySign2 image,
	@TradeCode varchar(30),
	@CustomerAccount int,
	@MobileSms varchar(20),
	@IsHere bit,
	@MoneyDepositeNumber varchar(50),
	@MoneyDepositeLocation nvarchar(200),
	@DepartmentId int,
	@PublicCompanyManage ntext,
	@PublicCompanyHolder ntext,
	@ParentCompanyName nvarchar(50),
	@ParentCompanyAddress nvarchar(100),
	@ParentCompanyEmail nvarchar(50),
	@ParentCompanyTel varchar(20),
	@PostType smallint,
	@ReOpenDate smalldatetime,
	@UserTakeCared int,
	@TypeID int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [dbo].[Customer] (
	[BranchCode],
	[ContractNumber],
	[CustomerId],
	[BrokerId],
	[CustomerName],
	[CustomerNameViet],
	[CustomerType],
	[DomesticForeign],
	[Dob],
	[Sex],
	[SignatureImage1],
	[SignatureImage2],
	[OpenDate],
	[CloseDate],
	[CardType],
	[CardIdentity],
	[CardDate],
	[CardIssuer],
	[Address],
	[AddressViet],
	[Tel],
	[Fax],
	[Mobile],
	[Mobile2],
	[Email],
	[UserCreate],
	[DateCreate],
	[UserModify],
	[DateModify],
	[ProxyStatus],
	[AccountStatus],
	[Notes],
	[WorkingAddress],
	[UserIntroduce],
	[AttitudePoint],
	[DepositPoint],
	[ActionPoint],
	[Country],
	[Website],
	[TaxCode],
	[AccountType],
	[OrderType],
	[ReceiveReport],
	[ReceiveReportBy],
	[MarriageStatus],
	[KnowledgeLevel],
	[Job],
	[OfficeFunction],
	[OfficeTel],
	[OfficeFax],
	[HusbandWifeName],
	[HusbandWifeCardNumber],
	[HusbandWifeCardDate],
	[HusbandWifeCardLocation],
	[HusbandWifeTel],
	[HusbandWifeEmail],
	[JoinStockMarket],
	[InvestKnowledge],
	[InvestedIn],
	[InvestTarget],
	[RiskAccepted],
	[InvestFund],
	[DelegatePersonName],
	[DelegatePersonFunction],
	[DelegatePersonCardNumber],
	[DelegateCardDate],
	[DelegateCardLocation],
	[DelegatePersonTel],
	[DelegatePersonEmail],
	[ChiefAccountancyName],
	[ChiefAccountancyCI],
	[ChiefAccountancyCD],
	[ChiefAccountancyIssuer],
	[ChiefAccountancySign1],
	[ChiefAccountancySign2],
	[CaProxyName],
	[CaProxyCI],
	[CaProxyCD],
	[CaProxyIssuer],
	[CaProxySign1],
	[CaProxySign2],
	[CompanySign1],
	[CompanySign2],
	[TradeCode],
	[CustomerAccount],
	[MobileSms],
	[IsHere],
	[MoneyDepositeNumber],
	[MoneyDepositeLocation],
	[DepartmentId],
	[PublicCompanyManage],
	[PublicCompanyHolder],
	[ParentCompanyName],
	[ParentCompanyAddress],
	[ParentCompanyEmail],
	[ParentCompanyTel],
	[PostType],
	[ReOpenDate],
	[UserTakeCared],
	[TypeID]
) VALUES (
	@BranchCode,
	@ContractNumber,
	@CustomerId,
	@BrokerId,
	@CustomerName,
	@CustomerNameViet,
	@CustomerType,
	@DomesticForeign,
	@Dob,
	@Sex,
	@SignatureImage1,
	@SignatureImage2,
	@OpenDate,
	@CloseDate,
	@CardType,
	@CardIdentity,
	@CardDate,
	@CardIssuer,
	@Address,
	@AddressViet,
	@Tel,
	@Fax,
	@Mobile,
	@Mobile2,
	@Email,
	@UserCreate,
	@DateCreate,
	@UserModify,
	@DateModify,
	@ProxyStatus,
	@AccountStatus,
	@Notes,
	@WorkingAddress,
	@UserIntroduce,
	@AttitudePoint,
	@DepositPoint,
	@ActionPoint,
	@Country,
	@Website,
	@TaxCode,
	@AccountType,
	@OrderType,
	@ReceiveReport,
	@ReceiveReportBy,
	@MarriageStatus,
	@KnowledgeLevel,
	@Job,
	@OfficeFunction,
	@OfficeTel,
	@OfficeFax,
	@HusbandWifeName,
	@HusbandWifeCardNumber,
	@HusbandWifeCardDate,
	@HusbandWifeCardLocation,
	@HusbandWifeTel,
	@HusbandWifeEmail,
	@JoinStockMarket,
	@InvestKnowledge,
	@InvestedIn,
	@InvestTarget,
	@RiskAccepted,
	@InvestFund,
	@DelegatePersonName,
	@DelegatePersonFunction,
	@DelegatePersonCardNumber,
	@DelegateCardDate,
	@DelegateCardLocation,
	@DelegatePersonTel,
	@DelegatePersonEmail,
	@ChiefAccountancyName,
	@ChiefAccountancyCI,
	@ChiefAccountancyCD,
	@ChiefAccountancyIssuer,
	@ChiefAccountancySign1,
	@ChiefAccountancySign2,
	@CaProxyName,
	@CaProxyCI,
	@CaProxyCD,
	@CaProxyIssuer,
	@CaProxySign1,
	@CaProxySign2,
	@CompanySign1,
	@CompanySign2,
	@TradeCode,
	@CustomerAccount,
	@MobileSms,
	@IsHere,
	@MoneyDepositeNumber,
	@MoneyDepositeLocation,
	@DepartmentId,
	@PublicCompanyManage,
	@PublicCompanyHolder,
	@ParentCompanyName,
	@ParentCompanyAddress,
	@ParentCompanyEmail,
	@ParentCompanyTel,
	@PostType,
	@ReOpenDate,
	@UserTakeCared,
	@TypeID
)

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerUpdate
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
**		Date: 10/08/2009 15:29:34
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerUpdate
	@BranchCode varchar(3), 
	@ContractNumber varchar(20), 
	@CustomerId varchar(10), 
	@BrokerId int, 
	@CustomerName varchar(100), 
	@CustomerNameViet nvarchar(100), 
	@CustomerType char(1), 
	@DomesticForeign char(1), 
	@Dob smalldatetime, 
	@Sex char(1), 
	@SignatureImage1 image, 
	@SignatureImage2 image, 
	@OpenDate smalldatetime, 
	@CloseDate smalldatetime, 
	@CardType int, 
	@CardIdentity varchar(20), 
	@CardDate smalldatetime, 
	@CardIssuer varchar(200), 
	@Address varchar(200), 
	@AddressViet nvarchar(200), 
	@Tel varchar(20), 
	@Fax varchar(20), 
	@Mobile varchar(20), 
	@Mobile2 varchar(20), 
	@Email varchar(100), 
	@UserCreate int, 
	@DateCreate smalldatetime, 
	@UserModify int, 
	@DateModify smalldatetime, 
	@ProxyStatus bit, 
	@AccountStatus char(1), 
	@Notes nvarchar(250), 
	@WorkingAddress nvarchar(200), 
	@UserIntroduce int, 
	@AttitudePoint int, 
	@DepositPoint int, 
	@ActionPoint int, 
	@Country varchar(50), 
	@Website nvarchar(200), 
	@TaxCode varchar(50), 
	@AccountType char(2), 
	@OrderType char(3), 
	@ReceiveReport char(2), 
	@ReceiveReportBy char(2), 
	@MarriageStatus char(1), 
	@KnowledgeLevel char(1), 
	@Job char(1), 
	@OfficeFunction nvarchar(50), 
	@OfficeTel varchar(20), 
	@OfficeFax varchar(20), 
	@HusbandWifeName nvarchar(100), 
	@HusbandWifeCardNumber varchar(20), 
	@HusbandWifeCardDate smalldatetime, 
	@HusbandWifeCardLocation nvarchar(150), 
	@HusbandWifeTel nvarchar(50), 
	@HusbandWifeEmail nvarchar(50), 
	@JoinStockMarket varchar(4), 
	@InvestKnowledge char(1), 
	@InvestedIn char(5), 
	@InvestTarget char(1), 
	@RiskAccepted char(1), 
	@InvestFund char(1), 
	@DelegatePersonName nvarchar(100), 
	@DelegatePersonFunction nvarchar(50), 
	@DelegatePersonCardNumber varchar(20), 
	@DelegateCardDate smalldatetime, 
	@DelegateCardLocation nvarchar(50), 
	@DelegatePersonTel varchar(20), 
	@DelegatePersonEmail nvarchar(100), 
	@ChiefAccountancyName nvarchar(100), 
	@ChiefAccountancyCI varchar(20), 
	@ChiefAccountancyCD smalldatetime, 
	@ChiefAccountancyIssuer varchar(20), 
	@ChiefAccountancySign1 image, 
	@ChiefAccountancySign2 image, 
	@CaProxyName nvarchar(100), 
	@CaProxyCI varchar(20), 
	@CaProxyCD smalldatetime, 
	@CaProxyIssuer varchar(20), 
	@CaProxySign1 image, 
	@CaProxySign2 image, 
	@CompanySign1 image, 
	@CompanySign2 image, 
	@TradeCode varchar(30), 
	@CustomerAccount int, 
	@MobileSms varchar(20), 
	@IsHere bit, 
	@MoneyDepositeNumber varchar(50), 
	@MoneyDepositeLocation nvarchar(200), 
	@DepartmentId int, 
	@PublicCompanyManage ntext, 
	@PublicCompanyHolder ntext, 
	@ParentCompanyName nvarchar(50), 
	@ParentCompanyAddress nvarchar(100), 
	@ParentCompanyEmail nvarchar(50), 
	@ParentCompanyTel varchar(20), 
	@PostType smallint, 
	@ReOpenDate smalldatetime, 
	@UserTakeCared int, 
	@TypeID int 
AS

UPDATE [dbo].[Customer] SET
	[BranchCode] = @BranchCode,
	[ContractNumber] = @ContractNumber,
	[BrokerId] = @BrokerId,
	[CustomerName] = @CustomerName,
	[CustomerNameViet] = @CustomerNameViet,
	[CustomerType] = @CustomerType,
	[DomesticForeign] = @DomesticForeign,
	[Dob] = @Dob,
	[Sex] = @Sex,
	[SignatureImage1] = @SignatureImage1,
	[SignatureImage2] = @SignatureImage2,
	[OpenDate] = @OpenDate,
	[CloseDate] = @CloseDate,
	[CardType] = @CardType,
	[CardIdentity] = @CardIdentity,
	[CardDate] = @CardDate,
	[CardIssuer] = @CardIssuer,
	[Address] = @Address,
	[AddressViet] = @AddressViet,
	[Tel] = @Tel,
	[Fax] = @Fax,
	[Mobile] = @Mobile,
	[Mobile2] = @Mobile2,
	[Email] = @Email,
	[UserCreate] = @UserCreate,
	[DateCreate] = @DateCreate,
	[UserModify] = @UserModify,
	[DateModify] = @DateModify,
	[ProxyStatus] = @ProxyStatus,
	[AccountStatus] = @AccountStatus,
	[Notes] = @Notes,
	[WorkingAddress] = @WorkingAddress,
	[UserIntroduce] = @UserIntroduce,
	[AttitudePoint] = @AttitudePoint,
	[DepositPoint] = @DepositPoint,
	[ActionPoint] = @ActionPoint,
	[Country] = @Country,
	[Website] = @Website,
	[TaxCode] = @TaxCode,
	[AccountType] = @AccountType,
	[OrderType] = @OrderType,
	[ReceiveReport] = @ReceiveReport,
	[ReceiveReportBy] = @ReceiveReportBy,
	[MarriageStatus] = @MarriageStatus,
	[KnowledgeLevel] = @KnowledgeLevel,
	[Job] = @Job,
	[OfficeFunction] = @OfficeFunction,
	[OfficeTel] = @OfficeTel,
	[OfficeFax] = @OfficeFax,
	[HusbandWifeName] = @HusbandWifeName,
	[HusbandWifeCardNumber] = @HusbandWifeCardNumber,
	[HusbandWifeCardDate] = @HusbandWifeCardDate,
	[HusbandWifeCardLocation] = @HusbandWifeCardLocation,
	[HusbandWifeTel] = @HusbandWifeTel,
	[HusbandWifeEmail] = @HusbandWifeEmail,
	[JoinStockMarket] = @JoinStockMarket,
	[InvestKnowledge] = @InvestKnowledge,
	[InvestedIn] = @InvestedIn,
	[InvestTarget] = @InvestTarget,
	[RiskAccepted] = @RiskAccepted,
	[InvestFund] = @InvestFund,
	[DelegatePersonName] = @DelegatePersonName,
	[DelegatePersonFunction] = @DelegatePersonFunction,
	[DelegatePersonCardNumber] = @DelegatePersonCardNumber,
	[DelegateCardDate] = @DelegateCardDate,
	[DelegateCardLocation] = @DelegateCardLocation,
	[DelegatePersonTel] = @DelegatePersonTel,
	[DelegatePersonEmail] = @DelegatePersonEmail,
	[ChiefAccountancyName] = @ChiefAccountancyName,
	[ChiefAccountancyCI] = @ChiefAccountancyCI,
	[ChiefAccountancyCD] = @ChiefAccountancyCD,
	[ChiefAccountancyIssuer] = @ChiefAccountancyIssuer,
	[ChiefAccountancySign1] = @ChiefAccountancySign1,
	[ChiefAccountancySign2] = @ChiefAccountancySign2,
	[CaProxyName] = @CaProxyName,
	[CaProxyCI] = @CaProxyCI,
	[CaProxyCD] = @CaProxyCD,
	[CaProxyIssuer] = @CaProxyIssuer,
	[CaProxySign1] = @CaProxySign1,
	[CaProxySign2] = @CaProxySign2,
	[CompanySign1] = @CompanySign1,
	[CompanySign2] = @CompanySign2,
	[TradeCode] = @TradeCode,
	[CustomerAccount] = @CustomerAccount,
	[MobileSms] = @MobileSms,
	[IsHere] = @IsHere,
	[MoneyDepositeNumber] = @MoneyDepositeNumber,
	[MoneyDepositeLocation] = @MoneyDepositeLocation,
	[DepartmentId] = @DepartmentId,
	[PublicCompanyManage] = @PublicCompanyManage,
	[PublicCompanyHolder] = @PublicCompanyHolder,
	[ParentCompanyName] = @ParentCompanyName,
	[ParentCompanyAddress] = @ParentCompanyAddress,
	[ParentCompanyEmail] = @ParentCompanyEmail,
	[ParentCompanyTel] = @ParentCompanyTel,
	[PostType] = @PostType,
	[ReOpenDate] = @ReOpenDate,
	[UserTakeCared] = @UserTakeCared,
	[TypeID] = @TypeID
WHERE
	[CustomerId] = @CustomerId

GO
/****** Object:  Stored Procedure [dbo].CustomerTypeDelete    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeDelete]
GO

/****** Object:  Stored Procedure [dbo].CustomerTypeGet    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeGet]
GO

/****** Object:  Stored Procedure [dbo].CustomerTypeGetList    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeGetList]
GO

	
/****** Object:  Stored Procedure [dbo].CustomerTypeInsert    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeInsert]
GO

/****** Object:  Stored Procedure [dbo].CustomerTypeUpdate    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeDelete
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
**		Date: 10/08/2009 15:29:39
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeDelete
	@TypeID int
AS

DELETE FROM [dbo].[CustomerType]
WHERE
	[TypeID] = @TypeID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeGet
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
**		Date: 10/08/2009 15:29:39
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeGet
	@TypeID int
AS

SELECT
	[TypeID],
	[Description]
FROM
	[dbo].[CustomerType]
WHERE
	[TypeID] = @TypeID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeGetList
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
**		Date: 10/08/2009 15:29:39
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempCustomerType (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	TypeID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomerType ([TypeID]) SELECT '
SET @sql = @sql + ' [TypeID] FROM [dbo].[CustomerType]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [CustomerType]

SELECT
	[dbo].[CustomerType].[TypeID],
	[Description]
FROM
	#TempCustomerType AS tblTemp JOIN [dbo].[CustomerType] ON
	tblTemp.TypeID = [dbo].[CustomerType].TypeID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempCustomerType

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeInsert
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
**		Date: 10/08/2009 15:29:39
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeInsert
	@Description nvarchar(50),
	@TypeID int output
AS

INSERT INTO [dbo].[CustomerType] 
(
	[Description]
) 
VALUES 
(
	@Description
)

SELECT @TypeID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeUpdate
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
**		Date: 10/08/2009 15:29:39
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeUpdate
	@TypeID int, 
	@Description nvarchar(50) 
AS

UPDATE [dbo].[CustomerType] SET
	[Description] = @Description
WHERE
	[TypeID] = @TypeID

GO
