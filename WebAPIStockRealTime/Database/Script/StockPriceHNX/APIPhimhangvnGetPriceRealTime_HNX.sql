IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'dbo.VFS_HNX_GETALLStockRealTime_IncludeIndex') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE dbo.VFS_HNX_GETALLStockRealTime_IncludeIndex
END
GO
CREATE PROCEDURE dbo.VFS_HNX_GETALLStockRealTime_IncludeIndex
	
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT CompanyID      
	,CASE WHEN FinishPrice = 0 THEN RefPrice ELSE FinishPrice END AS FinishPrice    
      ,Diff
      ,CASE WHEN FinishPrice = 0 THEN 0 ELSE CAST(((FinishPrice - RefPrice)/ FinishPrice) *100 AS DECIMAL(18,2) ) END AS DiffRate 
      
FROM   dbo.[dnn_AGStock_HASTC_SessionCompany]
  
UNION ALL
  
SELECT TOP 1 
		'HNXIndex' as IndexName,
		 Index3 AS [Index],
		 Diff3 AS Diff,
		DiffRate3 as DiffRate 
FROM  dbo.dnn_AGStock_HASTC_Session
order by CompanyID
GO

