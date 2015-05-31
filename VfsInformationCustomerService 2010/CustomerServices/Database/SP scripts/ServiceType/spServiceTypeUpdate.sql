	
/****** Object:  Stored Procedure [dbo].ServiceTypeUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeUpdate
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
CREATE PROCEDURE [dbo].spServiceTypeUpdate
	@ServiceTypeID int, 
	@ServiceTypeDescription nvarchar(50), 
	@CreatedDate datetime, 
	@ModifiedDate datetime 
AS

UPDATE [dbo].[ServiceType] SET
	[ServiceTypeDescription] = @ServiceTypeDescription,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
WHERE
	[ServiceTypeID] = @ServiceTypeID

GO
