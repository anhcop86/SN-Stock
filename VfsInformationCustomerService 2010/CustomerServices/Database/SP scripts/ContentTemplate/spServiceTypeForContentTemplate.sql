 /****** Object:  Stored Procedure [dbo].ContentTemplateGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeForContentTemplate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeForContentTemplate]
GO

CREATE PROCEDURE [dbo].spServiceTypeForContentTemplate
	@ServiceTypeID int
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
	[ServiceTypeID] = @ServiceTypeID

GO
	

	
