/****** Object:  Stored Procedure [dbo].MessageCommandGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMessageCommandGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMessageCommandGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spMessageCommandGet
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
CREATE PROCEDURE [dbo].spMessageCommandGet
	@MessageCommandID int
AS

SELECT
	[MessageCommandID],
	[MessageContentID],
	[Status],
	[ProcessedDateTime],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[MessageCommand]
WHERE
	[MessageCommandID] = @MessageCommandID

GO
	

	
