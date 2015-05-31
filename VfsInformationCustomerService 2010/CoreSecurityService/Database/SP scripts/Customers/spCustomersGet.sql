/****** Object:  Stored Procedure [dbo].CustomersGet    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersGet]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersGet
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
**		Date: 11/08/2009 13:25:06
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersGet
	@CustomerId varchar(10)
AS

SELECT
	[BranchCode],
	[ContractNumber],
	[CustomerId],
	[BrokerId],
	[CustomerName],
	[CustomerNameViet],
	[CustomerType],
	[DomesticForeign],
	[Dob],
	[Sex],
	[SignatureImage1],
	[SignatureImage2],
	[OpenDate],
	[CloseDate],
	[CardType],
	[CardIdentity],
	[CardDate],
	[CardIssuer],
	[Address],
	[AddressViet],
	[Tel],
	[Fax],
	[Mobile],
	[Mobile2],
	[Email],
	[UserCreate],
	[DateCreate],
	[UserModify],
	[DateModify],
	[ProxyStatus],
	[AccountStatus],
	[Notes],
	[WorkingAddress],
	[UserIntroduce],
	[AttitudePoint],
	[DepositPoint],
	[ActionPoint],
	[Country],
	[Website],
	[TaxCode],
	[AccountType],
	[OrderType],
	[ReceiveReport],
	[ReceiveReportBy],
	[MarriageStatus],
	[KnowledgeLevel],
	[Job],
	[OfficeFunction],
	[OfficeTel],
	[OfficeFax],
	[HusbandWifeName],
	[HusbandWifeCardNumber],
	[HusbandWifeCardDate],
	[HusbandWifeCardLocation],
	[HusbandWifeTel],
	[HusbandWifeEmail],
	[JoinStockMarket],
	[InvestKnowledge],
	[InvestedIn],
	[InvestTarget],
	[RiskAccepted],
	[InvestFund],
	[DelegatePersonName],
	[DelegatePersonFunction],
	[DelegatePersonCardNumber],
	[DelegateCardDate],
	[DelegateCardLocation],
	[DelegatePersonTel],
	[DelegatePersonEmail],
	[ChiefAccountancyName],
	[ChiefAccountancyCI],
	[ChiefAccountancyCD],
	[ChiefAccountancyIssuer],
	[ChiefAccountancySign1],
	[ChiefAccountancySign2],
	[CaProxyName],
	[CaProxyCI],
	[CaProxyCD],
	[CaProxyIssuer],
	[CaProxySign1],
	[CaProxySign2],
	[CompanySign1],
	[CompanySign2],
	[TradeCode],
	[CustomerAccount],
	[MobileSms],
	[IsHere],
	[MoneyDepositeNumber],
	[MoneyDepositeLocation],
	[DepartmentId],
	[PublicCompanyManage],
	[PublicCompanyHolder],
	[ParentCompanyName],
	[ParentCompanyAddress],
	[ParentCompanyEmail],
	[ParentCompanyTel],
	[PostType],
	[ReOpenDate],
	[UserTakeCared]
FROM
	[dbo].[Customers]
WHERE
	[CustomerId] = @CustomerId

GO
	

	
