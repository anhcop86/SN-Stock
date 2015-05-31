	
/****** Object:  Stored Procedure [dbo].ContentTemplateInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateInsert
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
**		Date: 28/05/2009 16:59:42
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateInsert
	@ServiceTypeID int,
	@Description nvarchar(400),
	@Sender nvarchar(127),
	@Receiver nvarchar(127),
	@Subject nvarchar(255),
	@BodyContentType nvarchar(50),
	@BodyEncoding nvarchar(15),
	@BodyMessage ntext,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentTemplateID int output
AS

INSERT INTO [dbo].[ContentTemplate] 
(
	[ServiceTypeID],
	[Description],
	[Sender],
	[Receiver],
	[Subject],
	[BodyContentType],
	[BodyEncoding],
	[BodyMessage],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@ServiceTypeID,
	@Description,
	@Sender,
	@Receiver,
	@Subject,
	@BodyContentType,
	@BodyEncoding,
	@BodyMessage,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ContentTemplateID = @@IDENTITY

GO
	
