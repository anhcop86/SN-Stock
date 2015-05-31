	
/****** Object:  Stored Procedure [dbo].ApprovedStockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsInsert]
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
**		Date: 10/02/2009 18:23:31
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
	@NewsId bigint 
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
	[ApprovedDate],
	[NewsId]
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
	@ApprovedDate,
	@NewsId
)

--SELECT @NewsId = @@IDENTITY

GO
	
