/****** Object:  Stored Procedure [dbo].ExtensionMessageGet    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageGet
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
**		Date: 10/24/2011 5:23:36 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spExtensionMessageGet
	@ExtensionMessageID bigint
AS

SELECT
	[ExtensionMessageID],
	[Title],
	[Content],
	[CreatedDate]
FROM
	[dbo].[ExtensionMessage]
WHERE
	[ExtensionMessageID] = @ExtensionMessageID

GO
	

	
