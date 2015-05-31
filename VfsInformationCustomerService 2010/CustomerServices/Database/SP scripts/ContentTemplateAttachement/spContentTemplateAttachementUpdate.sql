	
/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementUpdate    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementUpdate
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
**		Date: 09/06/2009 16:46:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementUpdate
	@ContentTemplateAttachementID int, 
	@AttachementDocument nvarchar(1024), 
	@AttachementDescription nvarchar(500), 
	@ContentTemplateID int, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ContentTemplateAttachement] SET
	[AttachementDocument] = @AttachementDocument,
	[AttachementDescription] = @AttachementDescription,
	[ContentTemplateID] = @ContentTemplateID,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID

GO
