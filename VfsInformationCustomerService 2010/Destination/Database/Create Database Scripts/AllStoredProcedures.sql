/****** Object:  Stored Procedure [dbo].stock_NewsDelete    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsDelete]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsGet    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGet]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsGetList    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].stock_NewsInsert    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsInsert]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsUpdate    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsDelete
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsDelete
	@NewsID int
AS

DELETE FROM [dbo].[stock_News]
WHERE
	[NewsID] = @NewsID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGet
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGet
	@NewsID int
AS

SELECT
	[NewsID],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[SymbolID],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl]
FROM
	[dbo].[stock_News]
WHERE
	[NewsID] = @NewsID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGetList
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_News (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	NewsID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_News ([NewsID]) SELECT '
SET @sql = @sql + ' [NewsID] FROM [dbo].[stock_News]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_News]

SELECT
	[dbo].[stock_News].[NewsID],
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[SymbolID],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl]
FROM
	#Tempstock_News AS tblTemp JOIN [dbo].[stock_News] ON
	tblTemp.NewsID = [dbo].[stock_News].NewsID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempstock_News

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsInsert
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsInsert
	@NewsTitle nvarchar(256),
	@NewsDescription nvarchar(1024),
	@NewsContent ntext,
	@NewsDate datetime,
	@NewsSource nvarchar(256),
	@SymbolID int,
	@UseUrl bit,
	@NewsUrl nvarchar(256),
	@LanguageID int,
	@IsApproved bit,
	@ImageUrl nvarchar(256),
	@NewsID int output
AS

INSERT INTO [dbo].[stock_News] 
(
	[NewsTitle],
	[NewsDescription],
	[NewsContent],
	[NewsDate],
	[NewsSource],
	[SymbolID],
	[UseUrl],
	[NewsUrl],
	[LanguageID],
	[IsApproved],
	[ImageUrl]
) 
VALUES 
(
	@NewsTitle,
	@NewsDescription,
	@NewsContent,
	@NewsDate,
	@NewsSource,
	@SymbolID,
	@UseUrl,
	@NewsUrl,
	@LanguageID,
	@IsApproved,
	@ImageUrl
)

SELECT @NewsID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsUpdate
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsUpdate
	@NewsID int, 
	@NewsTitle nvarchar(256), 
	@NewsDescription nvarchar(1024), 
	@NewsContent ntext, 
	@NewsDate datetime, 
	@NewsSource nvarchar(256), 
	@SymbolID int, 
	@UseUrl bit, 
	@NewsUrl nvarchar(256), 
	@LanguageID int, 
	@IsApproved bit, 
	@ImageUrl nvarchar(256) 
AS

UPDATE [dbo].[stock_News] SET
	[NewsTitle] = @NewsTitle,
	[NewsDescription] = @NewsDescription,
	[NewsContent] = @NewsContent,
	[NewsDate] = @NewsDate,
	[NewsSource] = @NewsSource,
	[SymbolID] = @SymbolID,
	[UseUrl] = @UseUrl,
	[NewsUrl] = @NewsUrl,
	[LanguageID] = @LanguageID,
	[IsApproved] = @IsApproved,
	[ImageUrl] = @ImageUrl
WHERE
	[NewsID] = @NewsID

GO
/****** Object:  Stored Procedure [dbo].stock_NewsGroupsDelete    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsDelete]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsGroupsGet    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsGet]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsGroupsGetList    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].stock_NewsGroupsInsert    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsInsert]
GO

/****** Object:  Stored Procedure [dbo].stock_NewsGroupsUpdate    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsDelete
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsDelete
	@ID int
AS

DELETE FROM [dbo].[stock_NewsGroups]
WHERE
	[ID] = @ID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsGet
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsGet
	@ID int
AS

SELECT
	[ID],
	[NewsID],
	[NewsGroup]
FROM
	[dbo].[stock_NewsGroups]
WHERE
	[ID] = @ID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsGetList
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_NewsGroups (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_NewsGroups ([ID]) SELECT '
SET @sql = @sql + ' [ID] FROM [dbo].[stock_NewsGroups]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_NewsGroups]

SELECT
	[dbo].[stock_NewsGroups].[ID],
	[NewsID],
	[NewsGroup]
FROM
	#Tempstock_NewsGroups AS tblTemp JOIN [dbo].[stock_NewsGroups] ON
	tblTemp.ID = [dbo].[stock_NewsGroups].ID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempstock_NewsGroups

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsInsert
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsInsert
	@NewsID int,
	@NewsGroup int,
	@ID int output
AS

INSERT INTO [dbo].[stock_NewsGroups] 
(
	[NewsID],
	[NewsGroup]
) 
VALUES 
(
	@NewsID,
	@NewsGroup
)

SELECT @ID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_NewsGroupsUpdate
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsUpdate
	@ID int, 
	@NewsID int, 
	@NewsGroup int 
AS

UPDATE [dbo].[stock_NewsGroups] SET
	[NewsID] = @NewsID,
	[NewsGroup] = @NewsGroup
WHERE
	[ID] = @ID

GO
/****** Object:  Stored Procedure [dbo].stock_SymbolsDelete    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsDelete]
GO

/****** Object:  Stored Procedure [dbo].stock_SymbolsGet    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsGet]
GO

/****** Object:  Stored Procedure [dbo].stock_SymbolsGetList    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsGetList]
GO

	
/****** Object:  Stored Procedure [dbo].stock_SymbolsInsert    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsInsert]
GO

/****** Object:  Stored Procedure [dbo].stock_SymbolsUpdate    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsDelete
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsDelete
	@SymbolID int
AS

DELETE FROM [dbo].[stock_Symbols]
WHERE
	[SymbolID] = @SymbolID
GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsGet
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsGet
	@SymbolID int
AS

SELECT
	[SymbolID],
	[SourceID],
	[Symbol],
	[MarketID],
	[IndustryID],
	[CompanyType],
	[SecType],
	[IsListing]
FROM
	[dbo].[stock_Symbols]
WHERE
	[SymbolID] = @SymbolID

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsGetList
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_Symbols (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	SymbolID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_Symbols ([SymbolID]) SELECT '
SET @sql = @sql + ' [SymbolID] FROM [dbo].[stock_Symbols]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_Symbols]

SELECT
	[dbo].[stock_Symbols].[SymbolID],
	[SourceID],
	[Symbol],
	[MarketID],
	[IndustryID],
	[CompanyType],
	[SecType],
	[IsListing]
FROM
	#Tempstock_Symbols AS tblTemp JOIN [dbo].[stock_Symbols] ON
	tblTemp.SymbolID = [dbo].[stock_Symbols].SymbolID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempstock_Symbols

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsInsert
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsInsert
	@SourceID varchar(50),
	@Symbol varchar(20),
	@MarketID int,
	@IndustryID int,
	@CompanyType varchar(50),
	@SecType int,
	@IsListing bit,
	@SymbolID int output
AS

INSERT INTO [dbo].[stock_Symbols] 
(
	[SourceID],
	[Symbol],
	[MarketID],
	[IndustryID],
	[CompanyType],
	[SecType],
	[IsListing]
) 
VALUES 
(
	@SourceID,
	@Symbol,
	@MarketID,
	@IndustryID,
	@CompanyType,
	@SecType,
	@IsListing
)

SELECT @SymbolID = @@IDENTITY

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsUpdate
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
**		Date: 08/03/2009 17:05:15
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsUpdate
	@SymbolID int, 
	@SourceID varchar(50), 
	@Symbol varchar(20), 
	@MarketID int, 
	@IndustryID int, 
	@CompanyType varchar(50), 
	@SecType int, 
	@IsListing bit 
AS

UPDATE [dbo].[stock_Symbols] SET
	[SourceID] = @SourceID,
	[Symbol] = @Symbol,
	[MarketID] = @MarketID,
	[IndustryID] = @IndustryID,
	[CompanyType] = @CompanyType,
	[SecType] = @SecType,
	[IsListing] = @IsListing
WHERE
	[SymbolID] = @SymbolID

GO
