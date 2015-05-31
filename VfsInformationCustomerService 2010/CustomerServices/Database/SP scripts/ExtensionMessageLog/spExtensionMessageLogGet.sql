/****** Object:  Stored Procedure [dbo].ExtensionMessageLogGet    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageLogGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageLogGet
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
CREATE PROCEDURE [dbo].spExtensionMessageLogGet
	@ExtensionMessageLogID bigint
AS

SELECT
	[ExtensionMessageLogID],
	[ExtensionMessageID],
	[CustomerId]
FROM
	[dbo].[ExtensionMessageLog]
WHERE
	[ExtensionMessageLogID] = @ExtensionMessageLogID

GO
	

	
