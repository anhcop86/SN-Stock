	
/****** Object:  Stored Procedure [dbo].ContentParameterInsert    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterInsert
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
CREATE PROCEDURE [dbo].spContentParameterInsert
	@ContentParameterName nvarchar(127),
	@ContentParameterDescription nvarchar(127),
	@ContentParameterActive nvarchar(1),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@ContentParameterID int output,
@ContentParameterValue nvarchar(255)
AS

INSERT INTO [dbo].[ContentParameter] 
(
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
) 
VALUES 
(
	@ContentParameterName,
	@ContentParameterDescription,
	@ContentParameterActive,
	@CreatedDate,
	@ModifiedDate,
	@ContentParameterValue
)

SELECT @ContentParameterID = @@IDENTITY

GO
	
