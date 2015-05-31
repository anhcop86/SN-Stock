	
/****** Object:  Stored Procedure [dbo].CustomerTypeInsert    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeInsert
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
CREATE PROCEDURE [dbo].spCustomerTypeInsert
	@Description nvarchar(50),
	@TypeID int output
AS

INSERT INTO [dbo].[CustomerType] 
(
	[Description]
) 
VALUES 
(
	@Description
)

SELECT @TypeID = @@IDENTITY

GO
	
