	
/****** Object:  Stored Procedure [dbo].MessageContentSentInsert    Script Date: 06 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentSentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentSentInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentSentInsert
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
CREATE PROCEDURE [dbo].spMessageContentSentInsert
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
	@MessageContentSentID bigint output,
	@ServiceID nvarchar(10),
	@CommandCode nvarchar(10),
	@Request nvarchar(20),
	@MoID nvarchar(20),
	@ChargeYN nvarchar(1),
	@TotalMessages smallint

AS

INSERT INTO [dbo].[MessageContentSent] 
(
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
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]
) 
VALUES 
(
	@MessageContentID,
	@ContentTemplateID,
	@ServiceTypeID,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@CreatedDate,
	@ModifiedDate,
	@ServiceID,
	@CommandCode,
	@Request,
	@MoID,
	@ChargeYN,
	@TotalMessages
)

SELECT @MessageContentSentID = @@IDENTITY

GO
	

