/****** Object:  Stored Procedure [dbo].ContentTemplateGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spExistContentTemplateByContentTemplate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spExistContentTemplateByContentTemplate
GO

CREATE PROCEDURE [dbo].spExistContentTemplateByContentTemplate
	@Description nvarchar(400)
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
	[Description] = @Description

GO
	

	
