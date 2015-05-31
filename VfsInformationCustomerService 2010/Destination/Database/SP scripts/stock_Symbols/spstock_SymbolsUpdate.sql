	
/****** Object:  Stored Procedure [dbo].stock_SymbolsUpdate    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsUpdate]
GO

	
	

	
/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsUpdate
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
**		Date: 08/03/2009 17:05:13
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spstock_SymbolsUpdate
	@SymbolID int, 
	@SourceID varchar(50), 
	@Symbol varchar(20), 
	@MarketID int, 
	@IndustryID int, 
	@CompanyType varchar(50), 
	@SecType int, 
	@IsListing bit 
AS

UPDATE [dbo].[stock_Symbols] SET
	[SourceID] = @SourceID,
	[Symbol] = @Symbol,
	[MarketID] = @MarketID,
	[IndustryID] = @IndustryID,
	[CompanyType] = @CompanyType,
	[SecType] = @SecType,
	[IsListing] = @IsListing
WHERE
	[SymbolID] = @SymbolID

GO
