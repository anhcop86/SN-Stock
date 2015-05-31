	
/****** Object:  Stored Procedure [dbo].StockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsUpdate]
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
**		Date: 10/02/2009 18:23:29
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockNewsUpdate
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
