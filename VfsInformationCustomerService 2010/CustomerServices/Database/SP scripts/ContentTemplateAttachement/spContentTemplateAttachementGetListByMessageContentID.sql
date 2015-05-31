/****** Object:  Stored Procedure [dbo].ContentTemplateAttachementGetList    Script Date: 09 June 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContentTemplateAttachementGetListByMessageContentID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spContentTemplateAttachementGetListByMessageContentID]
GO

	
	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spContentTemplateAttachementGetList
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
**		Date: 09/06/2009 16:46:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spContentTemplateAttachementGetListByMessageContentID
	@ContentTemplateID int,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempContentTemplateAttachement (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ContentTemplateAttachementID int	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempContentTemplateAttachement ([ContentTemplateAttachementID]) SELECT '
SET @sql = @sql + ' [ContentTemplateAttachementID] FROM [dbo].[ContentTemplateAttachement] WHERE ContentTemplateID = '  + Convert( nvarchar(10), @ContentTemplateID) + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [ContentTemplateAttachement]

SELECT
	[dbo].[ContentTemplateAttachement].[ContentTemplateAttachementID],
	[AttachementDocument],
	[AttachementDescription],
	[ContentTemplateID],
	[CreatedDate],
	[ModifiedDate]
FROM
	#TempContentTemplateAttachement AS tblTemp JOIN [dbo].[ContentTemplateAttachement] ON
	tblTemp.ContentTemplateAttachementID = [dbo].[ContentTemplateAttachement].ContentTemplateAttachementID 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
AND ContentTemplateID = @ContentTemplateID
ORDER BY RowNumber

DROP TABLE #TempContentTemplateAttachement

GO

	
 