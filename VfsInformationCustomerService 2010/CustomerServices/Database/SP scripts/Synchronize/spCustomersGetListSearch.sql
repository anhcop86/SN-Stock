 /****** Object:  Stored Procedure [dbo].CustomersGetList    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersGetListSearch]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].spCustomersGetListSearch
GO


CREATE PROCEDURE [dbo].spCustomersGetListSearch
	@TypeId int,
	@CustomerId nvarchar(10),
	@CustomerName nvarchar(100),
	@CustomerNameViet nvarchar(100),
	@Email nvarchar(100),
	@Mobile nvarchar(20),
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #TempCustomers (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	CustomerId varchar(10)	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomers ([CustomerId]) SELECT '
SET @sql = @sql + ' [CustomerId] FROM [dbo].[Customer]  
					WHERE TypeId = '+''''+ convert(nvarchar(1),@TypeId) +''''+'	
					AND (CustomerId LIKE ''%'+@CustomerId+'%'' OR '+''''+ @CustomerId +''''+' = ''All'')
					AND (CustomerName LIKE ''%'+@CustomerName+'%'' OR '+''''+ @CustomerName +''''+' = ''All'')
					AND (CustomerNameViet LIKE N''%'+@CustomerNameViet+'%'' OR '+''''+ @CustomerNameViet +''''+' = ''All'')
					AND (Email LIKE ''%'+@Email+'%'' OR '+''''+ @Email +''''+' = ''All'')					
					AND (Mobile LIKE ''%'+@Mobile+'%'' OR '+''''+ @Mobile +''''+' = ''All'')					
					ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM #TempCustomers

SELECT
	[BranchCode],
	[ContractNumber],
	[Customer].[CustomerId],
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
	[TypeId],
	[SendYN],
	[ReceiveRelatedStockEmail],
	[ReceiveRelatedStockSms]
	
FROM
	#TempCustomers AS tblTemp JOIN [dbo].[Customer] ON
	tblTemp.CustomerId = [dbo].[Customer].CustomerId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempCustomers

GO	
