	
/****** Object:  Stored Procedure [dbo].CustomerTypeUpdate    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeUpdate
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
CREATE PROCEDURE [dbo].spCustomerTypeUpdate
	@TypeID int, 
	@Description nvarchar(50) 
AS

UPDATE [dbo].[CustomerType] SET
	[Description] = @Description
WHERE
	[TypeID] = @TypeID

GO
