/****** Object:  Stored Procedure [dbo].CustomerTypeGet    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeGet
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
**		Date: 10/08/2009 15:29:33
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeGet
	@TypeID int
AS

SELECT
	[TypeID],
	[Description]
FROM
	[dbo].[CustomerType]
WHERE
	[TypeID] = @TypeID

GO
	

	
