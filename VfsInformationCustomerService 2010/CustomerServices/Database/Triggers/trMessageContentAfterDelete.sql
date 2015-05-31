if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trMessageContentAfterDelete]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[trMessageContentAfterDelete]
GO

CREATE TRIGGER [dbo].[trMessageContentAfterDelete] 
   ON  [dbo].[MessageContent] 
   AFTER DELETE
AS 
 DECLARE @MessageContentID int;
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    INSERT INTO MessageContentSent(MessageContentID,
									ContentTemplateID,
									ServiceTypeID,
									Sender, 
									Receiver, 
									Subject, 
									BodyContentType, 
									BodyEncoding, 
									BodyMessage, 
									CreatedDate, 
									ModifiedDate,									
									ServiceID,
									CommandCode,
									Request,
									MoID,
									ChargeYN,
									TotalMessages)
    
    SELECT MessageContentID,
			ContentTemplateID,
			ServiceTypeID,
			Sender, 
			Receiver, 
			Subject, 
			BodyContentType, 
			BodyEncoding, 
			BodyMessage,
			GetDate(),	
			GetDate(),			
			ServiceID,
			CommandCode,
			Request,
			MoID,
			ChargeYN,
			TotalMessages
	From     DELETED		
			
END
 