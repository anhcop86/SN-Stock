/****** Object:  Stored Procedure [dbo].ApprovedStockNewsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsGet]
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
**		Date: 10/02/2009 18:23:31
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsGet
	@NewsId bigint
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
	

	
