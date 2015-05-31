	
/****** Object:  Stored Procedure [dbo].MessageContentInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageContentInsert
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
CREATE PROCEDURE [dbo].spMessageContentInsert
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
	@MessageContentID int output,
	@Status int,
	@ServiceID nvarchar(10),
	@CommandCode nvarchar(10),
	@Request nvarchar(20),
	@MoID nvarchar(20),
	@ChargeYN nvarchar(1),
	@TotalMessages smallint	
AS

INSERT INTO [dbo].[MessageContent] 
(
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
	[ServiceID],
	[CommandCode],
	[Request],
	[MoID],
	[ChargeYN],
	[TotalMessages]	
)	
VALUES 
(
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
	@Status,
	@ServiceID,
	@CommandCode,
	@Request,
	@MoID,
	@ChargeYN,
	@TotalMessages
)

SELECT @MessageContentID = @@IDENTITY

GO