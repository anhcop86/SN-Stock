	
/****** Object:  Stored Procedure [dbo].BirthdayMessageLogUpdate    Script Date: Thursday, April 08, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBirthdayMessageLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBirthdayMessageLogUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spBirthdayMessageLogUpdate
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
**		Date: 4/8/2010 6:15:21 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spBirthdayMessageLogUpdate
	@BirthdayMessageDay nvarchar(8), 
	@ProccessYN nvarchar(1) 
AS

UPDATE [dbo].[BirthdayMessageLog] SET
	[ProccessYN] = @ProccessYN
WHERE
	[BirthdayMessageDay] = @BirthdayMessageDay

GO
