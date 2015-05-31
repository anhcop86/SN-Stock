	
/****** Object:  Stored Procedure [dbo].IncomingMessageContentSentInsert    Script Date: Wednesday, January 27, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spIncomingMessageContentSentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spIncomingMessageContentSentInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spIncomingMessageContentSentInsert
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
CREATE PROCEDURE [dbo].spIncomingMessageContentSentInsert
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
	@MoID nvarchar(20),
	@IncomingMessageContentSentID bigint output
AS

INSERT INTO [dbo].[IncomingMessageContentSent] 
(
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
) 
VALUES 
(
	@IncomingMessageContentID,
	@ServiceTypeID,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@Status,
	@CreatedDate,
	@ModifiedDate,
	@ServiceID,
	@CommandCode,
	@Request,
	@MoID
)

SELECT @IncomingMessageContentSentID = @@IDENTITY

GO
	
