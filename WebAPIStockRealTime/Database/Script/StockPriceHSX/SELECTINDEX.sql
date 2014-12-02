IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[HN_AGStock_Session_SelectIndex]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].HN_AGStock_Session_SelectIndex
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_GLStockCode_SelectByPK] @Id = 1

CREATE PROCEDURE [dbo].HN_AGStock_Session_SelectIndex		

AS

SET NOCOUNT ON;

SELECT TOP 1 
		'VnIndex' as IndexName,
		SessionDate,
		[Index],
		Diff,
		DiffRate,
		Total,
		[TotalShare],
		[Advances]
      ,[Declines]
      ,[NoChange]
FROM  dbo.dnn_AGStock_Session
WHERE cast(LastUpdate as date) = cast(getdate() as date)



GO