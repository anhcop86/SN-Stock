/****** Object:  Stored Procedure [dbo].RejectedStockNewsDelete    Script Date: 10 February 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRejectedStockNewsDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRejectedStockNewsDelete]
GO

	
/******************************************************************************
**		File: 
**		Name: [dbo].spRejectedStockNewsDelete
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
**		Date: 10/02/2009 18:23:32
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spRejectedStockNewsDelete
	@NewsId bigint
AS

DELETE FROM [dbo].[RejectedStockNews]
WHERE
	[NewsId] = @NewsId
GO
	
	

	
