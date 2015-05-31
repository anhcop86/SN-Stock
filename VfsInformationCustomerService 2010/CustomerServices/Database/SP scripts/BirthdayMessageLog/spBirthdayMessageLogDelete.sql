/****** Object:  Stored Procedure [dbo].BirthdayMessageLogDelete    Script Date: Thursday, April 08, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBirthdayMessageLogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBirthdayMessageLogDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spBirthdayMessageLogDelete
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
CREATE PROCEDURE [dbo].spBirthdayMessageLogDelete
	@BirthdayMessageDay nvarchar(8)
AS

DELETE FROM [dbo].[BirthdayMessageLog]
WHERE
	[BirthdayMessageDay] = @BirthdayMessageDay
GO
	
	

	
