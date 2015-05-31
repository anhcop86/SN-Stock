	
/****** Object:  Stored Procedure [dbo].ExtensionMessageLogUpdate    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageLogUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageLogUpdate
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
CREATE PROCEDURE [dbo].spExtensionMessageLogUpdate
	@ExtensionMessageLogID bigint, 
	@ExtensionMessageID bigint, 
	@CustomerId varchar(10) 
AS

UPDATE [dbo].[ExtensionMessageLog] SET
	[ExtensionMessageID] = @ExtensionMessageID,
	[CustomerId] = @CustomerId
WHERE
	[ExtensionMessageLogID] = @ExtensionMessageLogID

GO
