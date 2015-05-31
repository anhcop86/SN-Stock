/****** Object:  Stored Procedure [dbo].ExtensionMessageLogDelete    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageLogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageLogDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageLogDelete
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
CREATE PROCEDURE [dbo].spExtensionMessageLogDelete
	@ExtensionMessageLogID bigint
AS

DELETE FROM [dbo].[ExtensionMessageLog]
WHERE
	[ExtensionMessageLogID] = @ExtensionMessageLogID
GO
	
	

	
