	
/****** Object:  Stored Procedure [dbo].RejectedStockNewsInsert    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsInsert]
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
**		Date: 10/02/2009 18:23:32
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
	@NewsId bigint 
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
	[RejectedDate],
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
	@RejectedReason,
	@LinkId,
	@OriginalUrl,
	@RejectedDate,
	@NewsId
)

SELECT @NewsId = @@IDENTITY

GO
	
