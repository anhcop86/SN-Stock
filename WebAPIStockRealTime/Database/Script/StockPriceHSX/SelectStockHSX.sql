IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'dbo.HN_AGStock_Session_SelectStock') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE dbo.HN_AGStock_Session_SelectStock
END
GO



CREATE PROCEDURE dbo.HN_AGStock_Session_SelectStock		
	@TickerList varchar(2000)
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempStock (		
		CompanyID nvarchar(50)
)

DECLARE @sql nvarchar(2000)

--SET @TickerList = 'TV3'+''',''' + 'KCE'

-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempStock (CompanyID) SELECT  DISTINCT '
SET @sql = @sql + ' CompanyID FROM dbo.dnn_AGStock_SessionCompany WHERE  CompanyID IN (''' + @TickerList + ''')
					'
					
--print(@sql)
EXEC (@sql)

SET ROWCOUNT 0

SELECT Stock.CompanyID     
      ,FinishPrice
      ,FinishAmount      
	  ,Diff
	  ,[RefPrice]
      ,[TotalShare] AS TotalAmount
      
FROM  #TempStock AS tblTemp  JOIN dbo.dnn_AGStock_SessionCompany AS Stock ON
  tblTemp.CompanyID = Stock.CompanyID
  And cast(UpdateLETime as date) = (SELECT MAX(cast(UpdateLETime as date) ) FROM dnn_AGStock_SessionCompany)



GO