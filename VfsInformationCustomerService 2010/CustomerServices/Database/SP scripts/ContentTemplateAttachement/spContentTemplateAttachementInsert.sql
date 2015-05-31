	
/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementInsert    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementInsert
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
CREATE PROCEDURE [dbo].spContentTemplateAttachementInsert
	@AttachementDocument nvarchar(1024),
	@AttachementDescription nvarchar(500),
	@ContentTemplateID int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentTemplateAttachementID int output
AS

INSERT INTO [dbo].[ContentTemplateAttachement] 
(
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@AttachementDocument,
	@AttachementDescription,
	@ContentTemplateID,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ContentTemplateAttachementID = @@IDENTITY

GO
	
