	
/****** Object:  Stored Procedure [dbo].IncomingMessageContentSentUpdate    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentSentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentSentUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentSentUpdate
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
CREATE PROCEDURE [dbo].spIncomingMessageContentSentUpdate
	@IncomingMessageContentSentID bigint, 
	@IncomingMessageContentID bigint, 
	@ServiceTypeID int, 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage ntext, 
	@Status int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime, 
	@ServiceID nvarchar(10), 
	@CommandCode nvarchar(10), 
	@Request nvarchar(20), 
	@MoID nvarchar(20) 
AS

UPDATE [dbo].[IncomingMessageContentSent] SET
	[IncomingMessageContentID] = @IncomingMessageContentID,
	[ServiceTypeID] = @ServiceTypeID,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[Status] = @Status,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[ServiceID] = @ServiceID,
	[CommandCode] = @CommandCode,
	[Request] = @Request,
	[MoID] = @MoID
WHERE
	[IncomingMessageContentSentID] = @IncomingMessageContentSentID

GO
