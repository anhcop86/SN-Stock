/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGet    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateByContentTemplateAttachement]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spContentTemplateByContentTemplateAttachement
GO


CREATE PROCEDURE [dbo].spContentTemplateByContentTemplateAttachement
	@ContentTemplateID int
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
	[ContentTemplateID] = @ContentTemplateID

GO
	

	
