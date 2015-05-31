	
/****** Object:  Stored Procedure [dbo].RelatedMessagelogUpdate    Script Date: Wednesday, July 07, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRelatedMessagelogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRelatedMessagelogUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spRelatedMessagelogUpdate
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
**		Date: 7/7/2010 11:43:18 AM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRelatedMessagelogUpdate
	@RelatedMessagelogID bigint, 
	@NewsID bigint,
	@CustomerID nvarchar(10)
AS

UPDATE [dbo].[RelatedMessagelog] SET
	[NewsID] = @NewsID,
	CustomerID = @CustomerID
WHERE
	[RelatedMessagelogID] = @RelatedMessagelogID

GO
