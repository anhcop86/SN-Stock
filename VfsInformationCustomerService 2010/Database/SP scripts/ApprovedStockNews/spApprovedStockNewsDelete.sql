/****** Object:  Stored Procedure [dbo].ApprovedStockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spApprovedStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spApprovedStockNewsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spApprovedStockNewsDelete
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
**		Date: 10/02/2009 18:23:31
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spApprovedStockNewsDelete
	@NewsId bigint
AS

DELETE FROM [dbo].[ApprovedStockNews]
WHERE
	[NewsId] = @NewsId
GO
	
	

	
