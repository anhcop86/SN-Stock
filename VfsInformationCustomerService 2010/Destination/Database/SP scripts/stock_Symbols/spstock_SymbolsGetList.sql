/****** Object:  Stored Procedure [dbo].stock_SymbolsGetList    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsGetList]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsGetList
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
**		Date: 08/03/2009 17:05:14
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsGetList
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #Tempstock_Symbols (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	SymbolID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #Tempstock_Symbols ([SymbolID]) SELECT '
SET @sql = @sql + ' [SymbolID] FROM [dbo].[stock_Symbols]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [stock_Symbols]

SELECT
	[dbo].[stock_Symbols].[SymbolID],
	[SourceID],
	[Symbol],
	[MarketID],
	[IndustryID],
	[CompanyType],
	[SecType],
	[IsListing]
FROM
	#Tempstock_Symbols AS tblTemp JOIN [dbo].[stock_Symbols] ON
	tblTemp.SymbolID = [dbo].[stock_Symbols].SymbolID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #Tempstock_Symbols

GO

	
