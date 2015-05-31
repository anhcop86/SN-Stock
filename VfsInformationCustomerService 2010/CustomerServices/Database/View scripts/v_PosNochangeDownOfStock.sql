IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'v_PosNochangeDownOfStock')
	BEGIN
		DROP  View v_PosNochangeDownOfStock
	END
GO

CREATE View v_PosNochangeDownOfStock AS

	SELECT  POS.Date ,POS.Status pos, NO_CHANGE.Status nochange, DOWN.Status  down, 'hnx' market
            
FROM  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 4
                              and PriceAverage > PricePreviousClose 							  
                  group by convert(nvarchar(10),PermDate,112)) POS,
                  
                  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 4
                              and PriceAverage = PricePreviousClose 
                  group by convert(nvarchar(10),PermDate,112)) NO_CHANGE,
                  
                  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 4
                              and PriceAverage < PricePreviousClose 
                  group by convert(nvarchar(10),PermDate,112)) DOWN

WHERE POS.Date = DOWN.Date
        AND POS.Date = NO_CHANGE.Date           

Union

SELECT  POS.Date ,POS.Status pos, NO_CHANGE.Status nochange, DOWN.Status  down,'hose' market
            
FROM  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 6
                              and PriceClose > PricePreviousClose							  
                  group by convert(nvarchar(10),PermDate,112)) POS,
                  
                  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 6
                              and PriceClose = PricePreviousClose 
                  group by convert(nvarchar(10),PermDate,112)) NO_CHANGE,
                  
                  (Select convert(nvarchar(10),PermDate,112) as Date, count(stock_SymbolPermLong.[SymbolID]) Status  
              FROM [stock_SymbolPermLong],  stock_Symbols, stock_markets
                  WHERE stock_Symbols.SymbolId = stock_SymbolPermLong.SymbolId
                              and stock_markets.marketid = stock_Symbols.marketid
                              and stock_markets.marketid  = 6
                              and PriceClose < PricePreviousClose 
                  group by convert(nvarchar(10),PermDate,112)) DOWN

WHERE POS.Date = DOWN.Date
        AND POS.Date = NO_CHANGE.Date


GO

/*
GRANT SELECT ON View_Name TO PUBLIC

GO
*/
