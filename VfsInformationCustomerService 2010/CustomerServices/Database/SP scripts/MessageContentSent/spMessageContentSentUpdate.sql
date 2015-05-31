	
/****** Object:  Stored Procedure [dbo].MessageContentSentUpdate    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentUpdate
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
**		Date: 06/06/2009 16:44:22
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spMessageContentSentUpdate
	@MessageContentID int, 
	@ContentTemplateID int, 
	@ServiceTypeID int, 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage ntext, 
	@CreatedDate datetime, 
	@ModifiedDate datetime, 
	@MessageContentSentID bigint,
	@ServiceID nvarchar(10),
	@CommandCode nvarchar(10),
	@Request nvarchar(20),
	@MoID nvarchar(20),
	@ChargeYN nvarchar(1),
	@TotalMessages smallint
AS

UPDATE [dbo].[MessageContentSent] SET
	[MessageContentID] = @MessageContentID,
	[ContentTemplateID] = @ContentTemplateID,
	[ServiceTypeID] = @ServiceTypeID,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[ServiceID] = @ServiceID,
	[CommandCode] = @CommandCode,
	[Request] = @Request,
	[MoID] = @MoID,
	[ChargeYN] = @ChargeYN,
	[TotalMessages] = @TotalMessages
WHERE
	[MessageContentSentID] = @MessageContentSentID

GO
