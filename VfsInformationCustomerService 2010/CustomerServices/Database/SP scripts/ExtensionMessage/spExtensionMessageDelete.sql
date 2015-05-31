/****** Object:  Stored Procedure [dbo].ExtensionMessageDelete    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageDelete
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
CREATE PROCEDURE [dbo].spExtensionMessageDelete
	@ExtensionMessageID bigint
AS

DELETE FROM [dbo].[ExtensionMessage]
WHERE
	[ExtensionMessageID] = @ExtensionMessageID
GO
	
	

	
