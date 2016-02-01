IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'dbo.VFS_HSX_GETALLStockRealTime_IncludeIndex') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE dbo.VFS_HSX_GETALLStockRealTime_IncludeIndex
END
GO

CREATE PROCEDURE dbo.VFS_HSX_GETALLStockRealTime_IncludeIndex
	
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

DECLARE @Exist int
SET @Exist = (SELECT count([Index]) FROM  dbo.dnn_AGStock_Session)

 
	IF (@Exist = 0)
		SELECT CompanyID     
			,CASE WHEN FinishPrice = 0 THEN RefPrice ELSE FinishPrice END AS FinishPrice
			  ,Diff       
			  ,CASE WHEN FinishPrice = 0 THEN 0 ELSE CAST(((FinishPrice - RefPrice)/ FinishPrice) *100 AS DECIMAL(18,2) ) END AS DiffRate       
		FROM  dbo.dnn_AGStock_SessionCompany
		WHERE cast(UpdateLETime as date) = (SELECT MAX(cast(UpdateLETime as date) ) FROM dnn_AGStock_SessionCompany)

		UNION ALL		

		SELECT 
			'VNINDEX' AS [Index], 
			0 [Index],
			0 Diff,
			0 DiffRate		
		order by CompanyID
		
	ELSE
	BEGIN 
		SELECT CompanyID     
		,CASE WHEN FinishPrice = 0 THEN RefPrice ELSE FinishPrice END AS FinishPrice
		,Diff       
		,CASE WHEN FinishPrice = 0 THEN 0 ELSE CAST(((FinishPrice - RefPrice)/ FinishPrice) *100 AS DECIMAL(18,2) ) END AS DiffRate       
		FROM  dbo.dnn_AGStock_SessionCompany
		WHERE cast(UpdateLETime as date) = (SELECT MAX(cast(UpdateLETime as date) ) FROM dnn_AGStock_SessionCompany)
		 
		UNION ALL

		SELECT TOP 1 
		'VNINDEX', 
		[Index],
		Diff,
		DiffRate 	
		FROM  dbo.dnn_AGStock_Session
		--WHERE cast(LastUpdate as date) = cast(getdate() as date)			
		order by CompanyID
	END

GO
