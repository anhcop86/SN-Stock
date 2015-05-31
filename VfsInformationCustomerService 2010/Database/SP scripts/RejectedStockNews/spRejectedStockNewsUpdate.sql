	
/****** Object:  Stored Procedure [dbo].RejectedStockNewsUpdate    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsUpdate]
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
**		Date: 10/02/2009 18:23:32
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsUpdate
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
