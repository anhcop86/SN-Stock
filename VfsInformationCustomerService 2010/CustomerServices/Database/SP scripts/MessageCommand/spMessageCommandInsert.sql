	
/****** Object:  Stored Procedure [dbo].MessageCommandInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandInsert
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
CREATE PROCEDURE [dbo].spMessageCommandInsert
	@MessageContentID int,
	@Status int,
	@ProcessedDateTime datetime,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@MessageCommandID int output
AS

INSERT INTO [dbo].[MessageCommand] 
(
	[MessageContentID],
	[Status],
	[ProcessedDateTime],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@MessageContentID,
	@Status,
	@ProcessedDateTime,
	@CreatedDate,
	@ModifiedDate
)

SELECT @MessageCommandID = @@IDENTITY

GO
	
