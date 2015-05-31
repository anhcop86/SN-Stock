/****** Object:  Stored Procedure [dbo].CustomerDelete    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerDelete
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
CREATE PROCEDURE [dbo].spCustomerDelete
	@CustomerId varchar(10)
AS

DELETE FROM [dbo].[Customer]
WHERE
	[CustomerId] = @CustomerId
GO
	
	

	
