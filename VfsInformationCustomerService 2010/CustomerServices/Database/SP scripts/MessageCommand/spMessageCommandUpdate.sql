	
/****** Object:  Stored Procedure [dbo].MessageCommandUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandUpdate
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
CREATE PROCEDURE [dbo].spMessageCommandUpdate
	@MessageCommandID int, 
	@MessageContentID int, 
	@Status int, 
	@ProcessedDateTime datetime, 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[MessageCommand] SET
	[MessageContentID] = @MessageContentID,
	[Status] = @Status,
	[ProcessedDateTime] = @ProcessedDateTime,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[MessageCommandID] = @MessageCommandID

GO
