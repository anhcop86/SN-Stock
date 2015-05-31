/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementGet
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
CREATE PROCEDURE [dbo].spContentTemplateAttachementGet
	@ContentTemplateAttachementID int
AS

SELECT
	[ContentTemplateAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID

GO
	

	
