	
/****** Object:  Stored Procedure [dbo].ExtensionMessageInsert    Script Date: Monday, October 24, 2011 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExtensionMessageInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spExtensionMessageInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spExtensionMessageInsert
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
CREATE PROCEDURE [dbo].spExtensionMessageInsert
	@Title nvarchar(255),
	@Content nvarchar(1024),
	@CreatedDate datetime,
	@ExtensionMessageID bigint output
AS

INSERT INTO [dbo].[ExtensionMessage] 
(
	[Title],
	[Content],
	[CreatedDate]
) 
VALUES 
(
	@Title,
	@Content,
	@CreatedDate
)

SELECT @ExtensionMessageID = @@IDENTITY

GO
	
