/****** Object:  Stored Procedure [dbo].ServiceTypeGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spServiceTypeGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spServiceTypeGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spServiceTypeGetList
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
**		Date: 28/05/2009 16:59:44
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spServiceTypeGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempServiceType (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ServiceTypeID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempServiceType ([ServiceTypeID]) SELECT '
SET @sql = @sql + ' [ServiceTypeID] FROM [dbo].[ServiceType]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ServiceType]

SELECT
	[dbo].[ServiceType].[ServiceTypeID],
	[ServiceTypeDescription],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempServiceType AS tblTemp JOIN [dbo].[ServiceType] ON
	tblTemp.ServiceTypeID = [dbo].[ServiceType].ServiceTypeID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempServiceType

GO

	
