 /****** Object:  Stored Procedure [dbo].stock_NewsGroupsGetList    Script Date: 06 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGroupsGetListByNewsId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGroupsGetListByNewsId]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: spstock_NewsGroupsGetListByNewsId
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
**		Date: 06/03/2009 17:16:25
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGroupsGetListByNewsId
	@NewsId varchar(50),
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_NewsGroups (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_NewsGroups ([ID]) SELECT '
SET @sql = @sql + ' [ID] FROM [dbo].[stock_NewsGroups] WHERE NewsId=' + convert(nvarchar(15), @NewsId) + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_NewsGroups] WHERE NewsID = @NewsId

SELECT
	[dbo].[stock_NewsGroups].[ID],
	[NewsID],
	[NewsGroup]
FROM
	#Tempstock_NewsGroups AS tblTemp JOIN [dbo].[stock_NewsGroups] ON
	tblTemp.ID = [dbo].[stock_NewsGroups].ID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND NewsID = @NewsId
ORDER BY RowNumber

DROP TABLE #Tempstock_NewsGroups

GO

	
