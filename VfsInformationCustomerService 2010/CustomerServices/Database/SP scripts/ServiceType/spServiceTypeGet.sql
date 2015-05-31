/****** Object:  Stored Procedure [dbo].ServiceTypeGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeGet
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
CREATE PROCEDURE [dbo].spServiceTypeGet
	@ServiceTypeID int
AS

SELECT
	[ServiceTypeID],
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
FROM
	[dbo].[ServiceType]
WHERE
	[ServiceTypeID] = @ServiceTypeID

GO
	

	
