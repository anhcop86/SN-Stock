/****** Object:  Stored Procedure [dbo].CustomersDelete    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersDelete
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
**		Date: 11/08/2009 13:25:06
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersDelete
	@CustomerId varchar(10)
AS

DELETE FROM [dbo].[Customers]
WHERE
	[CustomerId] = @CustomerId
GO
	
	

	
