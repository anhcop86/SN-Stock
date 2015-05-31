/****** Object:  Stored Procedure [dbo].MessageContentDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageContentDeleteYear]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageContentDeleteYear]
GO

CREATE PROCEDURE [dbo].spMessageContentDeleteYear
	@ModifiedDate datetime
AS
BEGIN TRANSACTION
	DELETE FROM dbo.MessageContentAttachement
		WHERE YEAR(MessageContentAttachement.ModifiedDate) = YEAR(@ModifiedDate)

	DELETE FROM dbo.MessageContent
		WHERE YEAR(MessageContent.ModifiedDate) = YEAR(@ModifiedDate)

COMMIT TRANSACTION

GO