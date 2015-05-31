if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spGetPosNochangeDownOfStock]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spGetPosNochangeDownOfStock
GO

CREATE PROCEDURE [dbo].spGetPosNochangeDownOfStock
	@FromDate datetime,
	@ToDate datetime,
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT  Date,
		pos,
		nochange, 
		down,
		market
FROM dbo.v_PosNochangeDownOfStock
WHERE Date between convert(nvarchar(10),@FromDate,112) and convert(nvarchar(10),@ToDate,112)
ORDER BY Date, market 