/****** Object:  Stored Procedure [dbo].CustomersDelete    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersDelete]
GO

/****** Object:  Stored Procedure [dbo].CustomersGet    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersGet]
GO

/****** Object:  Stored Procedure [dbo].CustomersGetList    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersGetList]
GO

	
/****** Object:  Stored Procedure [dbo].CustomersInsert    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersInsert]
GO

/****** Object:  Stored Procedure [dbo].CustomersUpdate    Script Date: 11 August 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCustomersUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spCustomersUpdate]
GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersDelete
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
**		Date: 11/08/2009 13:25:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersDelete
	@CustomerId varchar(10)
AS

DELETE FROM [dbo].[Customers]
WHERE
	[CustomerId] = @CustomerId
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
**		Date: 11/08/2009 13:25:07
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
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersGetList
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
**		Date: 11/08/2009 13:25:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersGetList
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

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #TempCustomers ([CustomerId]) SELECT '
SET @sql = @sql + ' [CustomerId] FROM [dbo].[Customers]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)
SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM [Customers]

SELECT
	[BranchCode],
	[ContractNumber],
	[dbo].[Customers].[CustomerId],
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
	#TempCustomers AS tblTemp JOIN [dbo].[Customers] ON
	tblTemp.CustomerId = [dbo].[Customers].CustomerId 	
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #TempCustomers

GO

/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersInsert
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
**		Date: 11/08/2009 13:25:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersInsert
	@BranchCode varchar(3),
	@ContractNumber varchar(20),
	@CustomerId varchar(10),
	@BrokerId int,
	@CustomerName varchar(100),
	@CustomerNameViet nvarchar(100),
	@CustomerType char(1),
	@DomesticForeign char(1),
	@Dob smalldatetime,
	@Sex char(1),
	@SignatureImage1 image,
	@SignatureImage2 image,
	@OpenDate smalldatetime,
	@CloseDate smalldatetime,
	@CardType int,
	@CardIdentity varchar(20),
	@CardDate smalldatetime,
	@CardIssuer varchar(200),
	@Address varchar(200),
	@AddressViet nvarchar(200),
	@Tel varchar(20),
	@Fax varchar(20),
	@Mobile varchar(20),
	@Mobile2 varchar(20),
	@Email varchar(100),
	@UserCreate int,
	@DateCreate smalldatetime,
	@UserModify int,
	@DateModify smalldatetime,
	@ProxyStatus bit,
	@AccountStatus char(1),
	@Notes nvarchar(250),
	@WorkingAddress nvarchar(200),
	@UserIntroduce int,
	@AttitudePoint int,
	@DepositPoint int,
	@ActionPoint int,
	@Country varchar(50),
	@Website nvarchar(200),
	@TaxCode varchar(50),
	@AccountType char(2),
	@OrderType char(3),
	@ReceiveReport char(2),
	@ReceiveReportBy char(2),
	@MarriageStatus char(1),
	@KnowledgeLevel char(1),
	@Job char(1),
	@OfficeFunction nvarchar(50),
	@OfficeTel varchar(20),
	@OfficeFax varchar(20),
	@HusbandWifeName nvarchar(100),
	@HusbandWifeCardNumber varchar(20),
	@HusbandWifeCardDate smalldatetime,
	@HusbandWifeCardLocation nvarchar(150),
	@HusbandWifeTel nvarchar(50),
	@HusbandWifeEmail nvarchar(50),
	@JoinStockMarket varchar(4),
	@InvestKnowledge char(1),
	@InvestedIn char(5),
	@InvestTarget char(1),
	@RiskAccepted char(1),
	@InvestFund char(1),
	@DelegatePersonName nvarchar(100),
	@DelegatePersonFunction nvarchar(50),
	@DelegatePersonCardNumber varchar(20),
	@DelegateCardDate smalldatetime,
	@DelegateCardLocation nvarchar(50),
	@DelegatePersonTel varchar(20),
	@DelegatePersonEmail nvarchar(100),
	@ChiefAccountancyName nvarchar(100),
	@ChiefAccountancyCI varchar(20),
	@ChiefAccountancyCD smalldatetime,
	@ChiefAccountancyIssuer varchar(20),
	@ChiefAccountancySign1 image,
	@ChiefAccountancySign2 image,
	@CaProxyName nvarchar(100),
	@CaProxyCI varchar(20),
	@CaProxyCD smalldatetime,
	@CaProxyIssuer varchar(20),
	@CaProxySign1 image,
	@CaProxySign2 image,
	@CompanySign1 image,
	@CompanySign2 image,
	@TradeCode varchar(30),
	@CustomerAccount int,
	@MobileSms varchar(20),
	@IsHere bit,
	@MoneyDepositeNumber varchar(50),
	@MoneyDepositeLocation nvarchar(200),
	@DepartmentId int,
	@PublicCompanyManage ntext,
	@PublicCompanyHolder ntext,
	@ParentCompanyName nvarchar(50),
	@ParentCompanyAddress nvarchar(100),
	@ParentCompanyEmail nvarchar(50),
	@ParentCompanyTel varchar(20),
	@PostType smallint,
	@ReOpenDate smalldatetime,
	@UserTakeCared int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [dbo].[Customers] (
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
) VALUES (
	@BranchCode,
	@ContractNumber,
	@CustomerId,
	@BrokerId,
	@CustomerName,
	@CustomerNameViet,
	@CustomerType,
	@DomesticForeign,
	@Dob,
	@Sex,
	@SignatureImage1,
	@SignatureImage2,
	@OpenDate,
	@CloseDate,
	@CardType,
	@CardIdentity,
	@CardDate,
	@CardIssuer,
	@Address,
	@AddressViet,
	@Tel,
	@Fax,
	@Mobile,
	@Mobile2,
	@Email,
	@UserCreate,
	@DateCreate,
	@UserModify,
	@DateModify,
	@ProxyStatus,
	@AccountStatus,
	@Notes,
	@WorkingAddress,
	@UserIntroduce,
	@AttitudePoint,
	@DepositPoint,
	@ActionPoint,
	@Country,
	@Website,
	@TaxCode,
	@AccountType,
	@OrderType,
	@ReceiveReport,
	@ReceiveReportBy,
	@MarriageStatus,
	@KnowledgeLevel,
	@Job,
	@OfficeFunction,
	@OfficeTel,
	@OfficeFax,
	@HusbandWifeName,
	@HusbandWifeCardNumber,
	@HusbandWifeCardDate,
	@HusbandWifeCardLocation,
	@HusbandWifeTel,
	@HusbandWifeEmail,
	@JoinStockMarket,
	@InvestKnowledge,
	@InvestedIn,
	@InvestTarget,
	@RiskAccepted,
	@InvestFund,
	@DelegatePersonName,
	@DelegatePersonFunction,
	@DelegatePersonCardNumber,
	@DelegateCardDate,
	@DelegateCardLocation,
	@DelegatePersonTel,
	@DelegatePersonEmail,
	@ChiefAccountancyName,
	@ChiefAccountancyCI,
	@ChiefAccountancyCD,
	@ChiefAccountancyIssuer,
	@ChiefAccountancySign1,
	@ChiefAccountancySign2,
	@CaProxyName,
	@CaProxyCI,
	@CaProxyCD,
	@CaProxyIssuer,
	@CaProxySign1,
	@CaProxySign2,
	@CompanySign1,
	@CompanySign2,
	@TradeCode,
	@CustomerAccount,
	@MobileSms,
	@IsHere,
	@MoneyDepositeNumber,
	@MoneyDepositeLocation,
	@DepartmentId,
	@PublicCompanyManage,
	@PublicCompanyHolder,
	@ParentCompanyName,
	@ParentCompanyAddress,
	@ParentCompanyEmail,
	@ParentCompanyTel,
	@PostType,
	@ReOpenDate,
	@UserTakeCared
)

GO
	
/******************************************************************************
**		File: 
**		Name: [dbo].spCustomersUpdate
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
**		Date: 11/08/2009 13:25:07
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE [dbo].spCustomersUpdate
	@BranchCode varchar(3), 
	@ContractNumber varchar(20), 
	@CustomerId varchar(10), 
	@BrokerId int, 
	@CustomerName varchar(100), 
	@CustomerNameViet nvarchar(100), 
	@CustomerType char(1), 
	@DomesticForeign char(1), 
	@Dob smalldatetime, 
	@Sex char(1), 
	@SignatureImage1 image, 
	@SignatureImage2 image, 
	@OpenDate smalldatetime, 
	@CloseDate smalldatetime, 
	@CardType int, 
	@CardIdentity varchar(20), 
	@CardDate smalldatetime, 
	@CardIssuer varchar(200), 
	@Address varchar(200), 
	@AddressViet nvarchar(200), 
	@Tel varchar(20), 
	@Fax varchar(20), 
	@Mobile varchar(20), 
	@Mobile2 varchar(20), 
	@Email varchar(100), 
	@UserCreate int, 
	@DateCreate smalldatetime, 
	@UserModify int, 
	@DateModify smalldatetime, 
	@ProxyStatus bit, 
	@AccountStatus char(1), 
	@Notes nvarchar(250), 
	@WorkingAddress nvarchar(200), 
	@UserIntroduce int, 
	@AttitudePoint int, 
	@DepositPoint int, 
	@ActionPoint int, 
	@Country varchar(50), 
	@Website nvarchar(200), 
	@TaxCode varchar(50), 
	@AccountType char(2), 
	@OrderType char(3), 
	@ReceiveReport char(2), 
	@ReceiveReportBy char(2), 
	@MarriageStatus char(1), 
	@KnowledgeLevel char(1), 
	@Job char(1), 
	@OfficeFunction nvarchar(50), 
	@OfficeTel varchar(20), 
	@OfficeFax varchar(20), 
	@HusbandWifeName nvarchar(100), 
	@HusbandWifeCardNumber varchar(20), 
	@HusbandWifeCardDate smalldatetime, 
	@HusbandWifeCardLocation nvarchar(150), 
	@HusbandWifeTel nvarchar(50), 
	@HusbandWifeEmail nvarchar(50), 
	@JoinStockMarket varchar(4), 
	@InvestKnowledge char(1), 
	@InvestedIn char(5), 
	@InvestTarget char(1), 
	@RiskAccepted char(1), 
	@InvestFund char(1), 
	@DelegatePersonName nvarchar(100), 
	@DelegatePersonFunction nvarchar(50), 
	@DelegatePersonCardNumber varchar(20), 
	@DelegateCardDate smalldatetime, 
	@DelegateCardLocation nvarchar(50), 
	@DelegatePersonTel varchar(20), 
	@DelegatePersonEmail nvarchar(100), 
	@ChiefAccountancyName nvarchar(100), 
	@ChiefAccountancyCI varchar(20), 
	@ChiefAccountancyCD smalldatetime, 
	@ChiefAccountancyIssuer varchar(20), 
	@ChiefAccountancySign1 image, 
	@ChiefAccountancySign2 image, 
	@CaProxyName nvarchar(100), 
	@CaProxyCI varchar(20), 
	@CaProxyCD smalldatetime, 
	@CaProxyIssuer varchar(20), 
	@CaProxySign1 image, 
	@CaProxySign2 image, 
	@CompanySign1 image, 
	@CompanySign2 image, 
	@TradeCode varchar(30), 
	@CustomerAccount int, 
	@MobileSms varchar(20), 
	@IsHere bit, 
	@MoneyDepositeNumber varchar(50), 
	@MoneyDepositeLocation nvarchar(200), 
	@DepartmentId int, 
	@PublicCompanyManage ntext, 
	@PublicCompanyHolder ntext, 
	@ParentCompanyName nvarchar(50), 
	@ParentCompanyAddress nvarchar(100), 
	@ParentCompanyEmail nvarchar(50), 
	@ParentCompanyTel varchar(20), 
	@PostType smallint, 
	@ReOpenDate smalldatetime, 
	@UserTakeCared int 
AS

UPDATE [dbo].[Customers] SET
	[BranchCode] = @BranchCode,
	[ContractNumber] = @ContractNumber,
	[BrokerId] = @BrokerId,
	[CustomerName] = @CustomerName,
	[CustomerNameViet] = @CustomerNameViet,
	[CustomerType] = @CustomerType,
	[DomesticForeign] = @DomesticForeign,
	[Dob] = @Dob,
	[Sex] = @Sex,
	[SignatureImage1] = @SignatureImage1,
	[SignatureImage2] = @SignatureImage2,
	[OpenDate] = @OpenDate,
	[CloseDate] = @CloseDate,
	[CardType] = @CardType,
	[CardIdentity] = @CardIdentity,
	[CardDate] = @CardDate,
	[CardIssuer] = @CardIssuer,
	[Address] = @Address,
	[AddressViet] = @AddressViet,
	[Tel] = @Tel,
	[Fax] = @Fax,
	[Mobile] = @Mobile,
	[Mobile2] = @Mobile2,
	[Email] = @Email,
	[UserCreate] = @UserCreate,
	[DateCreate] = @DateCreate,
	[UserModify] = @UserModify,
	[DateModify] = @DateModify,
	[ProxyStatus] = @ProxyStatus,
	[AccountStatus] = @AccountStatus,
	[Notes] = @Notes,
	[WorkingAddress] = @WorkingAddress,
	[UserIntroduce] = @UserIntroduce,
	[AttitudePoint] = @AttitudePoint,
	[DepositPoint] = @DepositPoint,
	[ActionPoint] = @ActionPoint,
	[Country] = @Country,
	[Website] = @Website,
	[TaxCode] = @TaxCode,
	[AccountType] = @AccountType,
	[OrderType] = @OrderType,
	[ReceiveReport] = @ReceiveReport,
	[ReceiveReportBy] = @ReceiveReportBy,
	[MarriageStatus] = @MarriageStatus,
	[KnowledgeLevel] = @KnowledgeLevel,
	[Job] = @Job,
	[OfficeFunction] = @OfficeFunction,
	[OfficeTel] = @OfficeTel,
	[OfficeFax] = @OfficeFax,
	[HusbandWifeName] = @HusbandWifeName,
	[HusbandWifeCardNumber] = @HusbandWifeCardNumber,
	[HusbandWifeCardDate] = @HusbandWifeCardDate,
	[HusbandWifeCardLocation] = @HusbandWifeCardLocation,
	[HusbandWifeTel] = @HusbandWifeTel,
	[HusbandWifeEmail] = @HusbandWifeEmail,
	[JoinStockMarket] = @JoinStockMarket,
	[InvestKnowledge] = @InvestKnowledge,
	[InvestedIn] = @InvestedIn,
	[InvestTarget] = @InvestTarget,
	[RiskAccepted] = @RiskAccepted,
	[InvestFund] = @InvestFund,
	[DelegatePersonName] = @DelegatePersonName,
	[DelegatePersonFunction] = @DelegatePersonFunction,
	[DelegatePersonCardNumber] = @DelegatePersonCardNumber,
	[DelegateCardDate] = @DelegateCardDate,
	[DelegateCardLocation] = @DelegateCardLocation,
	[DelegatePersonTel] = @DelegatePersonTel,
	[DelegatePersonEmail] = @DelegatePersonEmail,
	[ChiefAccountancyName] = @ChiefAccountancyName,
	[ChiefAccountancyCI] = @ChiefAccountancyCI,
	[ChiefAccountancyCD] = @ChiefAccountancyCD,
	[ChiefAccountancyIssuer] = @ChiefAccountancyIssuer,
	[ChiefAccountancySign1] = @ChiefAccountancySign1,
	[ChiefAccountancySign2] = @ChiefAccountancySign2,
	[CaProxyName] = @CaProxyName,
	[CaProxyCI] = @CaProxyCI,
	[CaProxyCD] = @CaProxyCD,
	[CaProxyIssuer] = @CaProxyIssuer,
	[CaProxySign1] = @CaProxySign1,
	[CaProxySign2] = @CaProxySign2,
	[CompanySign1] = @CompanySign1,
	[CompanySign2] = @CompanySign2,
	[TradeCode] = @TradeCode,
	[CustomerAccount] = @CustomerAccount,
	[MobileSms] = @MobileSms,
	[IsHere] = @IsHere,
	[MoneyDepositeNumber] = @MoneyDepositeNumber,
	[MoneyDepositeLocation] = @MoneyDepositeLocation,
	[DepartmentId] = @DepartmentId,
	[PublicCompanyManage] = @PublicCompanyManage,
	[PublicCompanyHolder] = @PublicCompanyHolder,
	[ParentCompanyName] = @ParentCompanyName,
	[ParentCompanyAddress] = @ParentCompanyAddress,
	[ParentCompanyEmail] = @ParentCompanyEmail,
	[ParentCompanyTel] = @ParentCompanyTel,
	[PostType] = @PostType,
	[ReOpenDate] = @ReOpenDate,
	[UserTakeCared] = @UserTakeCared
WHERE
	[CustomerId] = @CustomerId

GO
