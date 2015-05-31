/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementDelete
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
CREATE PROCEDURE [dbo].spContentTemplateAttachementDelete
	@ContentTemplateAttachementID int
AS

DELETE FROM [dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateAttachementID] = @ContentTemplateAttachementID
GO
	
	

	
