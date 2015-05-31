/****** Object:  Stored Procedure [dbo].RelatedMessagelogDelete    Script Date: Wednesday, July 07, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRelatedMessagelogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRelatedMessagelogDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spRelatedMessagelogDelete
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
**		Date: 7/7/2010 11:43:19 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRelatedMessagelogDelete
	@RelatedMessagelogID bigint
AS

DELETE FROM [dbo].[RelatedMessagelog]
WHERE
	[RelatedMessagelogID] = @RelatedMessagelogID
GO
	
	

	
