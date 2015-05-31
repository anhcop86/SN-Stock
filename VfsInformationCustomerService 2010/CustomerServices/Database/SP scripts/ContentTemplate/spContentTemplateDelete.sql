/****** Object:  Stored Procedure [dbo].ContentTemplateDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateDelete
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
CREATE PROCEDURE [dbo].spContentTemplateDelete
	@ContentTemplateID int
AS

DELETE FROM [dbo].[ContentTemplate]
WHERE
	[ContentTemplateID] = @ContentTemplateID
GO
	
	

	
