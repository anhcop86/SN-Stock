	
/****** Object:  Stored Procedure [dbo].StockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockNewsInsert]
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
**		Date: 10/02/2009 18:23:29
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
	@NewsId bigint output
AS
Declare @countItem int;
Declare @countApprovedItem int;
Declare @countRejectedItem int;

Set @countItem = 0;
Set @countApprovedItem = 0;
Set @countRejectedItem = 0;

select @countItem = count(1) From [dbo].[StockNews] 
Where  OriginalUrl = @OriginalUrl

select @countApprovedItem = count(1) From [dbo].[ApprovedStockNews] 
Where  OriginalUrl = @OriginalUrl

select @countRejectedItem = count(1) From [dbo].[RejectedStockNews] 
Where  OriginalUrl = @OriginalUrl

If(@countItem = 0 And @countApprovedItem = 0 And @countRejectedItem = 0)
Begin
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
End

Else

SELECT @NewsId = 0


GO
	
