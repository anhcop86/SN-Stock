 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'V_LatestThirdtySections')
	BEGIN
		DROP  View V_LatestThirdtySections
	END
GO

CREATE VIEW V_LatestThirdtySections AS
Select Distinct TOP 30 Convert(nvarchar(10), D.PermDate, 112) PermDate
FROM stock_SymbolPermLong D 
ORDER BY Convert(nvarchar(10), D.PermDate, 112)  DESC
GO
