	
/****** Object:  Stored Procedure [dbo].ContentParameterUpdate    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterUpdate
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
**		Date: 28/05/2009 16:59:42
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentParameterUpdate
	@ContentParameterID int, 
	@ContentParameterName nvarchar(127), 
	@ContentParameterDescription nvarchar(127), 
	@ContentParameterActive nvarchar(1), 
	@CreatedDate datetime, 
	@ModifiedDate datetime,
	@ContentParameterValue nvarchar(255)
AS

UPDATE [dbo].[ContentParameter] SET
	[ContentParameterName] = @ContentParameterName,
	[ContentParameterDescription] = @ContentParameterDescription,
	[ContentParameterActive] = @ContentParameterActive,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[ContentParameterValue] = @ContentParameterValue
WHERE
	[ContentParameterID] = @ContentParameterID

GO
