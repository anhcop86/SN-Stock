if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_NewsGetListByStockCode]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_NewsGetListByStockCode]
GO
	
/******************************************************************************

*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**		12/07/2013		NGuyen Chi Hieu				Lấy danh mục tin tức gửi cho Khách hàng
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_NewsGetListByStockCode
    @StockCode nvarchar(10),
    @CustomerID nvarchar(10)

AS


SELECT 
	ArticleId as [NewsID],
	Title as [NewsTitle],
	Lead as [NewsDescription],
	Content as [NewsContent],
	UpdatedDate as [NewsDate],
	'TPHCM' as [NewsSource],Company,
	100 as [SymbolID],
	CAST(1 as bit) as [UseUrl],
	Null [NewsUrl],
	2 as [LanguageID],
	CAST(1 as bit) as IsApproved,
	'' as [ImageUrl]
  FROM dnn_AGNews_Articles NA
  Where  
  --AND CategoryId IN ('915','911','916','921','893','921')
  (Company IS NOT NULL AND Company <> '' AND LEN(Company) <=6)
  AND convert(nvarchar(10),UpdatedDate,112) = convert(nvarchar(10), GetDate(),112) 
  AND NA.ArticleId NOT IN (SELECT NewsID FROM RelatedMessagelog WHERE CustomerID = @CustomerID )
  AND NA.Company = @StockCode
  Order By UpdatedDate DESC  


GO