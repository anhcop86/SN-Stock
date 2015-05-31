/****** Object:  Stored Procedure [dbo].RelatedMessagelogGet    Script Date: Wednesday, July 07, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRelatedMessagelogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRelatedMessagelogGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spRelatedMessagelogGet
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
CREATE PROCEDURE [dbo].spRelatedMessagelogGet
	@RelatedMessagelogID bigint
AS

SELECT
	[RelatedMessagelogID],
	[NewsID],
	[CustomerID]
FROM
	[dbo].[RelatedMessagelog]
WHERE
	[RelatedMessagelogID] = @RelatedMessagelogID

GO
	

	
