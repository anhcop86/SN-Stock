/****** Object:  Stored Procedure [dbo].RejectedStockNewsGet    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsGet]
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
**		Date: 10/02/2009 18:23:32
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsGet
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
	[RejectedReason],
	[LinkId],
	[OriginalUrl],
	[RejectedDate]
FROM
	[dbo].[RejectedStockNews]
WHERE
	[NewsId] = @NewsId

GO
	

	
