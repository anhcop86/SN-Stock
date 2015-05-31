/****** Object:  Stored Procedure [dbo].CustomerTypeGetList    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerTypeGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerTypeGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerTypeGetList
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
**		Date: 10/08/2009 15:29:33
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerTypeGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempCustomerType (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	TypeID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomerType ([TypeID]) SELECT '
SET @sql = @sql + ' [TypeID] FROM [dbo].[CustomerType]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [CustomerType]

SELECT
	[dbo].[CustomerType].[TypeID],
	[Description]
FROM
	#TempCustomerType AS tblTemp JOIN [dbo].[CustomerType] ON
	tblTemp.TypeID = [dbo].[CustomerType].TypeID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempCustomerType

GO

	
