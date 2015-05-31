/****** Object:  Stored Procedure [dbo].stock_NewsGet    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGet]
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
**		Date: 06/03/2009 17:16:25
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
	

	
