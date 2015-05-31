	
/****** Object:  Stored Procedure [dbo].BirthdayMessageLogInsert    Script Date: Thursday, April 08, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBirthdayMessageLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBirthdayMessageLogInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spBirthdayMessageLogInsert
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
CREATE PROCEDURE [dbo].spBirthdayMessageLogInsert
	@BirthdayMessageDay nvarchar(8),
	@ProccessYN nvarchar(1)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [dbo].[BirthdayMessageLog] (
	[BirthdayMessageDay],
	[ProccessYN]
) VALUES (
	@BirthdayMessageDay,
	@ProccessYN
)

GO
	
