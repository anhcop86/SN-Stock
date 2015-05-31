/****** Object:  Stored Procedure [dbo].IncomingMessageContentGet    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentGet
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
**		Date: 1/27/2010 11:49:26 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spIncomingMessageContentGet
	@IncomingMessageContentID int
AS

SELECT
	[IncomingMessageContentID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[Status],
	[CreatedDate],
	[ModifiedDate],
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID]
FROM
	[dbo].[IncomingMessageContent]
WHERE
	[IncomingMessageContentID] = @IncomingMessageContentID

GO
	

	
