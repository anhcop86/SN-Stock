/****** Object:  Stored Procedure [dbo].MessageContentGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentGet
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
**		Date: 28/05/2009 16:59:43
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentGet
	@MessageContentID int
AS

SELECT
	[MessageContentID],
	[ContentTemplateID],
	[ServiceTypeID],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate],
	[Status],
	ServiceID,
	CommandCode,
	Request,
	MoID,
	ChargeYN,
	TotalMessages
FROM
	[dbo].[MessageContent]
WHERE
	[MessageContentID] = @MessageContentID

GO
	
