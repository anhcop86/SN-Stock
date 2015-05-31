/****** Object:  Stored Procedure [dbo].BirthdayMessageLogGet    Script Date: Thursday, April 08, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBirthdayMessageLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBirthdayMessageLogGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spBirthdayMessageLogGet
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
**		Date: 4/8/2010 6:15:22 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spBirthdayMessageLogGet
	@BirthdayMessageDay nvarchar(8)
AS

SELECT
	[BirthdayMessageDay],
	[ProccessYN]
FROM
	[dbo].[BirthdayMessageLog]
WHERE
	[BirthdayMessageDay] = @BirthdayMessageDay

GO
	

	
