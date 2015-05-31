	
/****** Object:  Stored Procedure [dbo].ApprovedStockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsUpdate]
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
**		Date: 10/02/2009 18:23:31
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsUpdate
	@NewsId bigint, 
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
