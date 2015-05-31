 /****** Object:  Stored Procedure [dbo].CustomerGet    Script Date: 10 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomerGetListByBirthDay]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomerGetListByBirthDay]
GO

	
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomerGet
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
**		Date: 10/08/2009 15:29:33
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomerGetListByBirthDay
	@BirthDay Datetime,
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempCustomer (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	CustomerId varchar(10)	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomer ([CustomerId]) SELECT '
SET @sql = @sql + ' [CustomerId] FROM [dbo].[Customer] WHERE substring(convert(nvarchar(10),[Dob],112), 5, 4) =' +  '''' + substring(convert(nvarchar(10),@BirthDay ,112), 5, 4) + '''' + ' ORDER BY [' + @OrderBy + N'] ' + @OrderDirection

EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Customer]
Where substring(convert(nvarchar(10),[Dob],112), 5, 4) =  substring(convert(nvarchar(10),@BirthDay,112), 5, 4)

SELECT
	[BranchCode],
	[ContractNumber],
	[dbo].[Customer].[CustomerId],
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
	[UserTakeCared],
	[TypeID],
	[SendYN],
	[ReceiveRelatedStockEmail],
	[ReceiveRelatedStockSms]
FROM
	#TempCustomer AS tblTemp JOIN [dbo].[Customer] ON
	tblTemp.CustomerId = [dbo].[Customer].CustomerId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
		and substring(convert(nvarchar(10),[Dob],112), 5, 4) =  substring(convert(nvarchar(10),@BirthDay,112), 5, 4)
		
ORDER BY RowNumber

DROP TABLE #TempCustomer

GO