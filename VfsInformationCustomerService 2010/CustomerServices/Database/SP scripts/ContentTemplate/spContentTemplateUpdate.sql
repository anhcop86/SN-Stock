	
/****** Object:  Stored Procedure [dbo].ContentTemplateUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateUpdate
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
CREATE PROCEDURE [dbo].spContentTemplateUpdate
	@ContentTemplateID int, 
	@ServiceTypeID int, 
	@Description nvarchar(400), 
	@Sender nvarchar(127), 
	@Receiver nvarchar(127), 
	@Subject nvarchar(255), 
	@BodyContentType nvarchar(50), 
	@BodyEncoding nvarchar(15), 
	@BodyMessage ntext, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ContentTemplate] SET
	[ServiceTypeID] = @ServiceTypeID,
	[Description] = @Description,
	[Sender] = @Sender,
	[Receiver] = @Receiver,
	[Subject] = @Subject,
	[BodyContentType] = @BodyContentType,
	[BodyEncoding] = @BodyEncoding,
	[BodyMessage] = @BodyMessage,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
