	
/****** Object:  Stored Procedure [dbo].ExtensionMessageLogInsert    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageLogInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageLogInsert
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
CREATE PROCEDURE [dbo].spExtensionMessageLogInsert
	@ExtensionMessageID bigint,
	@CustomerId varchar(10),
	@ExtensionMessageLogID bigint output
AS

INSERT INTO [dbo].[ExtensionMessageLog] 
(
	[ExtensionMessageID],
	[CustomerId]
) 
VALUES 
(
	@ExtensionMessageID,
	@CustomerId
)

SELECT @ExtensionMessageLogID = @@IDENTITY

GO
	
