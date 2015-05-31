	
/****** Object:  Stored Procedure [dbo].StockPriceInsert    Script Date: Tuesday, November 02, 2010 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spStockPriceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spStockPriceInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spStockPriceInsert
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
**		Date: 11/2/2010 2:57:46 PM
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spStockPriceInsert
	@TradingDate varchar(10),
	@StockCode varchar(10),
	@StockNo int,
	@StockType char(1),
	@BoardType char(1),
	@OpenPrice decimal(18, 1),
	@ClosePrice decimal(18, 1),
	@BasicPrice decimal(18, 1),
	@CeilingPrice decimal(18, 1),
	@FloorPrice decimal(18, 1),
	@AveragePrice decimal(18, 1),
	@TransactionDate smalldatetime,
	@TotalRoom decimal(18, 0),
	@CurrentRoom decimal(18, 0),
	@Suspension char(1),
	@Delisted char(1),
	@Halted char(1),
	@Split char(1),
	@Benefit char(1),
	@Meeting char(1),
	@Notice char(1)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [dbo].[StockPrice] (
	[TradingDate],
	[StockCode],
	[StockNo],
	[StockType],
	[BoardType],
	[OpenPrice],
	[ClosePrice],
	[BasicPrice],
	[CeilingPrice],
	[FloorPrice],
	[AveragePrice],
	[TransactionDate],
	[TotalRoom],
	[CurrentRoom],
	[Suspension],
	[Delisted],
	[Halted],
	[Split],
	[Benefit],
	[Meeting],
	[Notice]
) VALUES (
	@TradingDate,
	@StockCode,
	@StockNo,
	@StockType,
	@BoardType,
	@OpenPrice,
	@ClosePrice,
	@BasicPrice,
	@CeilingPrice,
	@FloorPrice,
	@AveragePrice,
	@TransactionDate,
	@TotalRoom,
	@CurrentRoom,
	@Suspension,
	@Delisted,
	@Halted,
	@Split,
	@Benefit,
	@Meeting,
	@Notice
)

GO
	
