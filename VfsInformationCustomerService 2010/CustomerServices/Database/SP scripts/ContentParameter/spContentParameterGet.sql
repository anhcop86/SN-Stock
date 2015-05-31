/****** Object:  Stored Procedure [dbo].ContentParameterGet    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentParameterGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentParameterGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentParameterGet
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
CREATE PROCEDURE [dbo].spContentParameterGet
	@ContentParameterID int
AS

SELECT
	[ContentParameterID],
	[ContentParameterName],
	[ContentParameterDescription],
	[ContentParameterActive],
	[CreatedDate],
	[ModifiedDate],
	[ContentParameterValue]
FROM
	[dbo].[ContentParameter]
WHERE
	[ContentParameterID] = @ContentParameterID

GO
	

	
