/****** Object:  Stored Procedure [dbo].CustomerTypeDelete    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeDelete
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
CREATE PROCEDURE [dbo].spCustomerTypeDelete
	@TypeID int
AS

DELETE FROM [dbo].[CustomerType]
WHERE
	[TypeID] = @TypeID
GO
	
	

	
