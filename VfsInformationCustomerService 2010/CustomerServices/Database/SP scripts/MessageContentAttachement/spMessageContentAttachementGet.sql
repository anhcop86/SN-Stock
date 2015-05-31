/****** Object:  Stored Procedure [dbo].MessageContentAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentAttachementGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentAttachementGet
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
**		Date: 09/06/2009 16:46:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentAttachementGet
	@MessageContentAttachementID int
AS

SELECT
	[MessageContentAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[MessageContentID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageContentAttachement]
WHERE
	[MessageContentAttachementID] = @MessageContentAttachementID

GO
	

	
