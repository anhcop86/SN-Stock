 /****** Object:  Stored Procedure [dbo].ContentTemplateAttachementDelete    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[spDeleteContentTemplateAttachementByContentTemplate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [spDeleteContentTemplateAttachementByContentTemplate]
GO


CREATE PROCEDURE [dbo].spDeleteContentTemplateAttachementByContentTemplate
	@ContentTemplateID int
AS

DELETE FROM [dbo].[ContentTemplateAttachement]
WHERE
	[ContentTemplateID] = @ContentTemplateID
GO
	
	

	
