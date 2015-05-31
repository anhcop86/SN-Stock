/****** Object:  Stored Procedure [dbo].ServiceTypeDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeDelete
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
**		Date: 28/05/2009 16:59:44
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeDelete
	@ServiceTypeID int
AS

DELETE FROM [dbo].[ServiceType]
WHERE
	[ServiceTypeID] = @ServiceTypeID
GO
	
	

	
