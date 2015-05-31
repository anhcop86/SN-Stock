	
/****** Object:  Stored Procedure [dbo].RelatedMessagelogInsert    Script Date: Wednesday, July 07, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRelatedMessagelogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRelatedMessagelogInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spRelatedMessagelogInsert
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
CREATE PROCEDURE [dbo].spRelatedMessagelogInsert
	@NewsID bigint,
	@CustomerID nvarchar(10),
	@RelatedMessagelogID bigint output
AS

INSERT INTO [dbo].[RelatedMessagelog] 
(
	[NewsID],
	[CustomerID]
) 
VALUES 
(
	@NewsID,
	@CustomerID
)

SELECT @RelatedMessagelogID = @@IDENTITY

GO
	
