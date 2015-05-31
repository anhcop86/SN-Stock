 /****** Object:  Stored Procedure [dbo].CustomersGet    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAGStock_HOSE_SessionCompanyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAGStock_HOSE_SessionCompanyGet]
GO

CREATE PROCEDURE [dbo].spAGStock_HOSE_SessionCompanyGet
	@UpdateLETime datetime
AS

SELECT
	CompanyID,
	CeilingPrice,
	FloorPrice,
	RefPrice,
	BuyPrice1,
	BuyAmount1*10 AS BuyAmount1,
	BuyPrice2,
	BuyAmount2*10 AS BuyAmount2,
	BuyPrice3,
	BuyAmount3*10 AS BuyAmount3,
	SellPrice1,
	SellAmount1*10 AS SellAmount1,
	SellPrice2,
	SellAmount2*10 AS SellAmount2,
	SellPrice3,
	SellAmount3*10 AS SellAmount3
FROM
	dbo.dnn_AGStock_SessionCompany
WHERE
	Convert(nvarchar(10),UpdateLETime,112) = Convert(nvarchar(10),@UpdateLETime,112)
ORDER BY CompanyID
GO