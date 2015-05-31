	
/****** Object:  Stored Procedure [dbo].ServiceTypeInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeInsert
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
CREATE PROCEDURE [dbo].spServiceTypeInsert
	@ServiceTypeDescription nvarchar(50),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ServiceTypeID int output
AS

INSERT INTO [dbo].[ServiceType] 
(
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
) 
VALUES 
(
	@ServiceTypeDescription,
	@CreatedDate,
	@ModifiedDate
)

SELECT @ServiceTypeID = @@IDENTITY

GO
	
