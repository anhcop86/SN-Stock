/****** Object:  Stored Procedure [dbo].IncomingMessageContentSentGet    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentSentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentSentGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentSentGet
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
CREATE PROCEDURE [dbo].spIncomingMessageContentSentGet
	@IncomingMessageContentSentID bigint
AS

SELECT
	[IncomingMessageContentSentID],
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
	[dbo].[IncomingMessageContentSent]
WHERE
	[IncomingMessageContentSentID] = @IncomingMessageContentSentID

GO
	

	
