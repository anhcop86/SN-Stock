IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'dbo.HN_AGStock_Session_SelectHNXIndex') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE dbo.HN_AGStock_Session_SelectHNXIndex
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   dbo.PR_GLStockCode_SelectByPK @Id = 1

CREATE PROCEDURE dbo.HN_AGStock_Session_SelectHNXIndex		

AS

SET NOCOUNT ON;

SELECT TOP 1 
		'HNX Index' as IndexName,
		SessionDate,
		Index3 AS [Index],
		Diff3 AS Diff,
		DiffRate3 as DiffRate,
		Total3 as Total,
		TotalShare3 as TotalShare,
		Advances,
      NoChange,
      Declines
FROM  dbo.dnn_AGStock_HASTC_Session



GO