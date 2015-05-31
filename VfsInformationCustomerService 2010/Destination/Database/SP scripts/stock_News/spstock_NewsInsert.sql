	
/****** Object:  Stored Procedure [dbo].stock_NewsInsert    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsInsert]
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
**		Date: 06/03/2009 17:16:25
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
	
