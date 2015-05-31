	
/****** Object:  Stored Procedure [dbo].stock_SymbolsInsert    Script Date: 08 March 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spstock_SymbolsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spstock_SymbolsInsert]
GO

	
	

/******************************************************************************
**		File: 
**		Name: [dbo].spstock_SymbolsInsert
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
CREATE PROCEDURE [dbo].spstock_SymbolsInsert
	@SourceID varchar(50),
	@Symbol varchar(20),
	@MarketID int,
	@IndustryID int,
	@CompanyType varchar(50),
	@SecType int,
	@IsListing bit,
	@SymbolID int output
AS

INSERT INTO [dbo].[stock_Symbols] 
(
	[SourceID],
	[Symbol],
	[MarketID],
	[IndustryID],
	[CompanyType],
	[SecType],
	[IsListing]
) 
VALUES 
(
	@SourceID,
	@Symbol,
	@MarketID,
	@IndustryID,
	@CompanyType,
	@SecType,
	@IsListing
)

SELECT @SymbolID = @@IDENTITY

GO
	
