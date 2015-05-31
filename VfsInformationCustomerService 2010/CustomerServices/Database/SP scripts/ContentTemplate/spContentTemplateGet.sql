/****** Object:  Stored Procedure [dbo].ContentTemplateGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateGet
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
CREATE PROCEDURE [dbo].spContentTemplateGet
	@ContentTemplateID int
AS

SELECT
	[ContentTemplateID],
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
FROM
	[dbo].[ContentTemplate]
WHERE
	[ContentTemplateID] = @ContentTemplateID

GO
	

	
