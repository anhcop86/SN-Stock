	
/****** Object:  Stored Procedure [dbo].ExtensionMessageUpdate    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageUpdate
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
CREATE PROCEDURE [dbo].spExtensionMessageUpdate
	@ExtensionMessageID bigint, 
	@Title nvarchar(255), 
	@Content nvarchar(1024), 
	@CreatedDate datetime 
AS

UPDATE [dbo].[ExtensionMessage] SET
	[Title] = @Title,
	[Content] = @Content,
	[CreatedDate] = @CreatedDate
WHERE
	[ExtensionMessageID] = @ExtensionMessageID

GO
