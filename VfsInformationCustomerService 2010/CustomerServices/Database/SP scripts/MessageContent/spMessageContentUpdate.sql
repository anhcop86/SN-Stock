	
/****** Object:  Stored Procedure [dbo].MessageContentUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentUpdate
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
CREATE PROCEDURE [dbo].spMessageContentUpdate
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
	@Status int,
	@ServiceID nvarchar(10),
	@CommandCode nvarchar(10),
	@Request nvarchar(20),
	@MoID nvarchar(20),
	@ChargeYN nvarchar(1),
	@TotalMessages smallint	
AS

UPDATE [dbo].[MessageContent] SET
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
	[Status] = @Status,
	[ServiceID] = @ServiceID,
	[CommandCode] = @CommandCode,
	[Request] = @Request,
	[MoID] = @MoID,
	[ChargeYN] = @ChargeYN,
	[TotalMessages] = @TotalMessages
WHERE
	[MessageContentID] = @MessageContentID

GO


 