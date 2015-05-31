
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockNews]') AND type in (N'U'))
DROP TABLE [dbo].[StockNews]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApprovedStockNews]') AND type in (N'U'))
DROP TABLE [dbo].[ApprovedStockNews]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RejectedStockNews ]') AND type in (N'U'))
DROP TABLE [dbo].[RejectedStockNews ]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Link]') AND type in (N'U'))
DROP TABLE [dbo].[Link]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Source]') AND type in (N'U'))
DROP TABLE [dbo].[Source]
GO

CREATE TABLE Source (
    SourceId int not null,
    SiteName nvarchar(256) not null,
    URL nvarchar(256) not null)

ALTER TABLE Source ADD CONSTRAINT PK_Source PRIMARY KEY ( SourceId)
ALTER TABLE Source ADD CONSTRAINT U_SiteName UNIQUE(SiteName)
ALTER TABLE Source ADD CONSTRAINT U_URL UNIQUE(URL)

INSERT INTO Source(SourceId, SiteName , URL) VALUES(1,'Hose','http://www.hsx.vn/' )

CREATE TABLE Link (
    LinkId int not null,
    SourceId int,
    Link nvarchar(256) not null,
    LinkShortDescription nvarchar(256),
    LinkDescription nvarchar(256))

INSERT INTO Link(LinkId, SourceId, Link, LinkShortDescription)  VALUES(1,1,'hsx/Modules/News/News.aspx?type=TTGD','Tin tu so giao dich')

ALTER TABLE Link ADD CONSTRAINT PK_Link PRIMARY KEY (LinkId)
ALTER TABLE Link ADD CONSTRAINT FK_SourceId_Link_Source FOREIGN KEY (SourceId) REFERENCES Source(SourceId)
ALTER TABLE Link ADD CONSTRAINT U_Link UNIQUE(Link)

CREATE TABLE StockNews (
    NewsId bigint not null identity(1,1),
    NewsTitle nvarchar(256) not null,
    NewsDescription nvarchar(1024) not null,
    NewsContent nText,
    NewsDate DateTime not null,
    NewsSource nvarchar(256),
    ShareSymbol nvarchar(10),
    UseUrl bit,
    NewsUrl nvarchar(256),
    LanguageID int not null,
    IsApproved int not null,
    ImageUrl nvarchar(256),
    LinkId int,
    OriginalUrl nvarchar(256),
    FeedDate DateTime)

ALTER TABLE StockNews ADD CONSTRAINT PK_StockNews PRIMARY KEY(NewsId)
ALTER TABLE StockNews ADD CONSTRAINT FK_LinkId_Link_StockNews FOREIGN KEY (LinkId) REFERENCES Link(LinkId)
ALTER TABLE StockNews ADD CONSTRAINT U_OriginalUrl UNIQUE(OriginalUrl)

CREATE TABLE ApprovedStockNews (
    NewsId bigint not null,
    NewsTitle nvarchar(256) not null,
    NewsDescription nvarchar(1024) not null,
    NewsContent nText,
    NewsDate DateTime not null,
    NewsSource nvarchar(256),
    ShareSymbol nvarchar(10),
    UseUrl bit,
    NewsUrl nvarchar(256),
    LanguageID int not null,
    IsApproved int not null,
    ImageUrl nvarchar(256),
    Comment nvarchar(256),
    LinkId int,
    OriginalUrl nvarchar(256),
    ApprovedDate DateTime);

ALTER TABLE ApprovedStockNews ADD CONSTRAINT PK_ApprovedStockNews PRIMARY KEY(NewsId)
ALTER TABLE ApprovedStockNews ADD CONSTRAINT FK_LinkId_Link_ApprovedStockNews FOREIGN KEY (LinkId) REFERENCES Link(LinkId)
ALTER TABLE ApprovedStockNews ADD CONSTRAINT ApprovedStockNews_U_OriginalUrl UNIQUE(OriginalUrl)

CREATE TABLE RejectedStockNews (
    NewsId bigint not null,
    NewsTitle nvarchar(256) not null,
    NewsDescription nvarchar(1024) not null,
    NewsContent nText,
    NewsDate DateTime not null,
    NewsSource nvarchar(256),
    ShareSymbol nvarchar(10),
    UseUrl bit,
    NewsUrl nvarchar(256),
    LanguageID int not null,
    IsApproved int not null,
    ImageUrl nvarchar(256),
    RejectedReason nvarchar(256),
    LinkId int,
    OriginalUrl nvarchar(256),
    RejectedDate DateTime);

ALTER TABLE RejectedStockNews ADD CONSTRAINT PK_RejectedStockNews PRIMARY KEY(NewsId)
ALTER TABLE RejectedStockNews ADD CONSTRAINT FK_LinkId_Link_RejectedStockNews FOREIGN KEY (LinkId) REFERENCES Link(LinkId)
ALTER TABLE RejectedStockNews ADD CONSTRAINT RejectedStockNews_U_OriginalUrl UNIQUE(OriginalUrl)


/*--- End of webcrawler database---*/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageContentSentAttachement]') AND type in (N'U'))
DROP TABLE [dbo].[MessageContentSentAttachement]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageContentAttachement]') AND type in (N'U'))
DROP TABLE [dbo].[MessageContentAttachement]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentTemplateAttachement]') AND type in (N'U'))
DROP TABLE [dbo].[ContentTemplateAttachement]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageCommand]') AND type in (N'U'))
DROP TABLE [dbo].[MessageCommand]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageContent]') AND type in (N'U'))
DROP TABLE [dbo].[MessageContent]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageContentSent]') AND type in (N'U'))
DROP TABLE [dbo].[MessageContentSent]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[ContentTemplate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceType]') AND type in (N'U'))
DROP TABLE [dbo].[ServiceType]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentParameter]') AND type in (N'U'))
DROP TABLE [dbo].[ContentParameter]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerType]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerType]
GO

CREATE TABLE ServiceType (
	ServiceTypeID int  not null identity(1,1),
	ServiceTypeDescription nvarchar(50),
	CreatedDate DateTime,
	ModifiedDate DateTime
);

ALTER TABLE ServiceType ADD CONSTRAINT ServiceType_Pk  PRIMARY KEY(ServiceTypeID)

CREATE TABLE ContentTemplate (
    ContentTemplateID int not null identity(1,1),
    ServiceTypeID int,
    Description     nvarchar(400) not null,
    Sender nvarchar(127) not null,
    Receiver nvarchar(127) not null,
    Subject nvarchar(255) not null,
    BodyContentType nvarchar(50) not null,
    BodyEncoding nvarchar(15) not null,
    BodyMessage ntext not null,
    CreatedDate DateTime,
	ModifiedDate DateTime
    );

ALTER TABLE ContentTemplate ADD CONSTRAINT ContentTemplate_Pk PRIMARY KEY(ContentTemplateID);
ALTER TABLE ContentTemplate ADD CONSTRAINT ContentTemplate_ServiceTypeID_Fk FOREIGN KEY(ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)

CREATE TABLE MessageContent (
    MessageContentID int not null identity(1,1),
    ContentTemplateID int,
    ServiceTypeID int,
    Sender nvarchar(127) not null,
    Receiver nvarchar(127) not null,
    Subject nvarchar(255) not null,
    BodyContentType nvarchar(50) not null,
    BodyEncoding nvarchar(15) not null,
    BodyMessage ntext not null,
    Status int DEFAULT 0,
    CreatedDate DateTime,
	ModifiedDate DateTime,
	Status int,
	ServiceID nvarchar(10), 
	CommandCode nvarchar(10),
	Request nvarchar(20),
	MoID nvarchar(20),
	ChargeYN  nvarchar(1),
	TotalMessages Smallint	
    );

ALTER TABLE MessageContent ADD CONSTRAINT MessageContent_Pk PRIMARY KEY(MessageContentID);
ALTER TABLE MessageContent ADD CONSTRAINT MessageContent_ContentTemplateID_Fk FOREIGN KEY(ContentTemplateID) REFERENCES ContentTemplate(ContentTemplateID);
ALTER TABLE MessageContent ADD CONSTRAINT MessageContent_ServiceTypeID_Fk FOREIGN KEY(ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)

/*ALTER TABLE MessageContent ADD ServiceID nvarchar(10)
ALTER TABLE MessageContent ADD CommandCode nvarchar(10)
ALTER TABLE MessageContent ADD Request nvarchar(20)
ALTER TABLE MessageContent ADD MoID nvarchar(20)
ALTER TABLE MessageContent ADD ChargeYN  nvarchar(1)
ALTER TABLE MessageContent ADD TotalMessages Smallint*/

CREATE TABLE MessageContentSent (
    MessageContentSentID bigint not null identity(1,1)
    MessageContentID int,
    ContentTemplateID int,
    ServiceTypeID int,
    Sender nvarchar(127) not null,
    Receiver nvarchar(127) not null,
    Subject nvarchar(255) not null,
    BodyContentType nvarchar(50) not null,
    BodyEncoding nvarchar(15) not null,
    BodyMessage ntext not null,
    CreatedDate DateTime,
	ModifiedDate DateTime,
	ServiceID nvarchar(10), 
	CommandCode nvarchar(10),
	Request nvarchar(20),
	MoID nvarchar(20),
	ChargeYN  nvarchar(1),
	TotalMessages Smallint,
	CreatedDate DateTime,
	ModifiedDate DateTime
    );

ALTER TABLE MessageContentSent ADD CONSTRAINT MessageContentSent_Pk PRIMARY KEY(MessageContentSentID);
ALTER TABLE MessageContentSent ADD CONSTRAINT MessageContentSent_ContentTemplateID_Fk FOREIGN KEY(ContentTemplateID) REFERENCES ContentTemplate(ContentTemplateID);
ALTER TABLE MessageContentSent ADD CONSTRAINT MessageContentSent_ServiceTypeID_Fk FOREIGN KEY(ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)

/*ALTER TABLE MessageContentSent ADD ServiceID nvarchar(10)
ALTER TABLE MessageContentSent ADD CommandCode nvarchar(10)
ALTER TABLE MessageContentSent ADD Request nvarchar(20)
ALTER TABLE MessageContentSent ADD MoID nvarchar(20)
ALTER TABLE MessageContentSent ADD ChargeYN  nvarchar(1)
ALTER TABLE MessageContentSent ADD TotalMessages Smallint*/

CREATE TABLE MessageCommand (
    MessageCommandID int not null identity(1,1),
    MessageContentID int not null,
    Status int DEFAULT 0,
    ProcessedDateTime DateTime not null,
    CreatedDate DateTime,
	ModifiedDate DateTime
    );
    
/****************** 0: Not Start *****/
/****************** 1: Success And Finish *****/
/****************** 2: Failed And Success *****/

ALTER TABLE MessageCommand ADD CONSTRAINT MessageCommand_Pk PRIMARY KEY(MessageCommandID);
ALTER TABLE MessageCommand ADD CONSTRAINT MessageCommand_MessageContentID_Fk FOREIGN KEY(MessageContentID) REFERENCES MessageContent(MessageContentID);

CREATE TABLE ContentParameter (
    ContentParameterID int not null identity(1,1),
    ContentParameterName nvarchar(127),
    ContentParameterDescription nvarchar(127),
    ContentParameterValue nvarchar(255),    
    ContentParameterActive nvarchar(1),    
    CreatedDate DateTime,
	ModifiedDate DateTime
    );
    
ALTER TABLE ContentParameter ADD CONSTRAINT ContentParameter_Pk PRIMARY KEY(ContentParameterID);
ALTER TABLE ContentParameter ADD CONSTRAINT ContentParameter_U_ContentParameterName UNIQUE(ContentParameterName)

CREATE TABLE ContentTemplateAttachement (
    ContentTemplateAttachementID int not null identity(1,1),
    AttachementDocument nvarchar(1024),
    AttachementDescription nvarchar(500),    
    ContentTemplateID int,
    CreatedDate DateTime,
	ModifiedDate DateTime
    );

ALTER TABLE ContentTemplateAttachement ADD CONSTRAINT ContentTemplateAttachement_Pk PRIMARY KEY(ContentTemplateAttachementID);
ALTER TABLE ContentTemplateAttachement ADD CONSTRAINT ContentTemplateAttachement_ContentTemplateID_Fk FOREIGN KEY(ContentTemplateID) REFERENCES ContentTemplate(ContentTemplateID);    
    
CREATE TABLE MessageContentAttachement (
    MessageContentAttachementID int not null identity(1,1),
    AttachementDocument nvarchar(1024),
    AttachementDescription nvarchar(500),
    MessageContentID int,
    CreatedDate DateTime,
	ModifiedDate DateTime
    );
    
    ALTER TABLE MessageContentAttachement ADD CONSTRAINT MessageContentAttachement_Pk PRIMARY KEY(MessageContentAttachementID);
    ALTER TABLE MessageContentAttachement ADD CONSTRAINT MessageContentAttachement_MessageContentID_Fk FOREIGN KEY(MessageContentID) REFERENCES MessageContent(MessageContentID);
    
    CREATE TABLE MessageContentSentAttachement (
    MessageContentSentAttachementID int not null identity(1,1),
    AttachementDocument nvarchar(1024),
    AttachementDescription nvarchar(500),
    MessageContentID int,
    CreatedDate DateTime,
	ModifiedDate DateTime
    );
    
    ALTER TABLE MessageContentSentAttachement ADD CONSTRAINT MessageContentSentAttachement_Pk PRIMARY KEY(MessageContentSentAttachementID);
    ALTER TABLE MessageContentSentAttachement ADD CONSTRAINT MessageContentSentAttachement_MessageContentID_Fk FOREIGN KEY(MessageContentID) REFERENCES MessageContentSent(MessageContentID);
 
 CREATE TABLE CustomerType (
	TypeID int not null identity(1,1),
	Description nvarchar(50));
	
 	ALTER TABLE CustomerType ADD CONSTRAINT CustomerType_Pk PRIMARY KEY(TypeID);
 	
 	/****************** 1: Da Mo Tai Khoan *****/
	/****************** 2: Chua Mo Tai Khoan *****/	
 	
 	INSERT INTO CustomerType(Description)  VALUES('Da Mo Tai Khoan')
 	INSERT INTO CustomerType(Description)  VALUES('Chua Mo Tai Khoan')
  
CREATE TABLE Customer (
	[BranchCode] [varchar](3) NOT NULL,
	[ContractNumber] [varchar](20) NULL,
	[CustomerId] [varchar](10) NOT NULL,
	[BrokerId] [int] NOT NULL,
	[CustomerName] [varchar](100) NULL,
	[CustomerNameViet] [nvarchar](100) NULL,
	[CustomerType] [char](1) NULL,
	[DomesticForeign] [char](1) NULL,
	[Dob] [smalldatetime] NULL,
	[Sex] [char](1) NULL,
	[SignatureImage1] [image] NULL,
	[SignatureImage2] [image] NULL,
	[OpenDate] [smalldatetime] NULL,
	[CloseDate] [smalldatetime] NULL,
	[CardType] [int] NOT NULL DEFAULT ((1)),
	[CardIdentity] [varchar](20) ,
	[CardDate] [smalldatetime] NULL,
	[CardIssuer] [varchar](200) NULL,
	[Address] [varchar](200) NULL,
	[AddressViet] [nvarchar](200) NULL,
	[Tel] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[Mobile2] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
	[UserCreate] [int] NULL,
	[DateCreate] [smalldatetime] NULL,
	[UserModify] [int] NULL,
	[DateModify] [smalldatetime] NULL,
	[ProxyStatus] [bit] NULL,
	[AccountStatus] [char](1) NULL DEFAULT ('O'),
	[Notes] [nvarchar](250) NULL,
	[WorkingAddress] [nvarchar](200) NULL,
	[UserIntroduce] [int] NULL,
	[AttitudePoint] [int] NULL DEFAULT ((0)),
	[DepositPoint] [int] NULL DEFAULT ((0)),
	[ActionPoint] [int] NULL DEFAULT ((0)),
	[Country] [varchar](50) NULL DEFAULT ('Viet nam'),
	[Website] [nvarchar](200) NULL,
	[TaxCode] [varchar](50) NULL,
	[AccountType] [char](2) NULL,
	[OrderType] [char](3) NULL,
	[ReceiveReport] [char](2) NULL,
	[ReceiveReportBy] [char](2) NULL,
	[MarriageStatus] [char](1) NULL,
	[KnowledgeLevel] [char](1) NULL,
	[Job] [char](1) NULL,
	[OfficeFunction] [nvarchar](50) NULL,
	[OfficeTel] [varchar](20) NULL,
	[OfficeFax] [varchar](20) NULL,
	[HusbandWifeName] [nvarchar](100) NULL,
	[HusbandWifeCardNumber] [varchar](20) NULL,
	[HusbandWifeCardDate] [smalldatetime] NULL,
	[HusbandWifeCardLocation] [nvarchar](150) NULL,
	[HusbandWifeTel] [nvarchar](50) NULL,
	[HusbandWifeEmail] [nvarchar](50) NULL,
	[JoinStockMarket] [varchar](4) NULL,
	[InvestKnowledge] [char](1) NULL,
	[InvestedIn] [char](5) NULL,
	[InvestTarget] [char](1) NULL,
	[RiskAccepted] [char](1) NULL,
	[InvestFund] [char](1) NULL,
	[DelegatePersonName] [nvarchar](100) NULL,
	[DelegatePersonFunction] [nvarchar](50) NULL,
	[DelegatePersonCardNumber] [varchar](20) NULL,
	[DelegateCardDate] [smalldatetime] NULL,
	[DelegateCardLocation] [nvarchar](50) NULL,
	[DelegatePersonTel] [varchar](20) NULL,
	[DelegatePersonEmail] [nvarchar](100) NULL,
	[ChiefAccountancyName] [nvarchar](100) NULL,
	[ChiefAccountancyCI] [varchar](20) NULL,
	[ChiefAccountancyCD] [smalldatetime] NULL,
	[ChiefAccountancyIssuer] [varchar](20) NULL,
	[ChiefAccountancySign1] [image] NULL,
	[ChiefAccountancySign2] [image] NULL,
	[CaProxyName] [nvarchar](100) NULL,
	[CaProxyCI] [varchar](20) NULL,
	[CaProxyCD] [smalldatetime] NULL,
	[CaProxyIssuer] [varchar](20) NULL,
	[CaProxySign1] [image] NULL,
	[CaProxySign2] [image] NULL,
	[CompanySign1] [image] NULL,
	[CompanySign2] [image] NULL,
	[TradeCode] [varchar](30) NULL,
	[CustomerAccount] [int] NOT NULL DEFAULT ((1)),
	[MobileSms] [varchar](20) NULL,
	[IsHere] [bit] NOT NULL DEFAULT ((1)),
	[MoneyDepositeNumber] [varchar](50) NULL,
	[MoneyDepositeLocation] [nvarchar](200) NULL,
	[DepartmentId] [int] NULL,
	[PublicCompanyManage] [ntext] NULL,
	[PublicCompanyHolder] [ntext] NULL,
	[ParentCompanyName] [nvarchar](50) NULL,
	[ParentCompanyAddress] [nvarchar](100) NULL,
	[ParentCompanyEmail] [nvarchar](50) NULL,
	[ParentCompanyTel] [varchar](20) NULL,
	[PostType] [smallint] NOT NULL DEFAULT ((0)),
	[ReOpenDate] [smalldatetime] NULL,
	[UserTakeCared] [int] NULL,
	[TypeID] int,
	[SendYN] [varchar](1) Default 'Y',
	[ReceiveRelatedStockEmail] (1) Default 'Y',
	[ReceiveRelatedStockSms] (1) Default 'Y');
	
	ALTER TABLE Customer ADD CONSTRAINT Customer_Pk PRIMARY KEY(CustomerId);
	ALTER TABLE Customer ADD CONSTRAINT Customer_TypeID_Fk FOREIGN KEY(TypeID) REFERENCES CustomerType(TypeID);
	
CREATE TABLE IncomingMessageContent (
    IncomingMessageContentID bigint not null identity(1,1),    
    ServiceTypeID int,
    Sender nvarchar(127) not null,
    Receiver nvarchar(127) null,
    Subject nvarchar(255) null,
    BodyContentType nvarchar(50) not null,
    BodyEncoding nvarchar(15) not null,
    BodyMessage ntext not null,
    Status int DEFAULT 0,
    CreatedDate DateTime,
	ModifiedDate DateTime,
	ServiceID nvarchar(10), 
	CommandCode nvarchar(10),
	Request nvarchar(20),
	MoID nvarchar(20)
    );
    
    ALTER TABLE IncomingMessageContent ADD CONSTRAINT IncomingMessageContentID_Pk PRIMARY KEY(IncomingMessageContentID);
    ALTER TABLE IncomingMessageContent ADD CONSTRAINT IncomingMessageContent_ServiceTypeID_Fk FOREIGN KEY(ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)
        
    CREATE TABLE IncomingMessageContentSent (
    IncomingMessageContentSentID bigint identity(1,1),    
    IncomingMessageContentID bigint,    
    ServiceTypeID int,
    Sender nvarchar(127) not null,
    Receiver nvarchar(127) null,
    Subject nvarchar(255) null,
    BodyContentType nvarchar(50) not null,
    BodyEncoding nvarchar(15) not null,
    BodyMessage ntext not null,
    Status int DEFAULT 0,
    CreatedDate DateTime,
	ModifiedDate DateTime,
	ServiceID nvarchar(10), 
	CommandCode nvarchar(10),
	Request nvarchar(20),
	MoID nvarchar(20)
    );
    
    ALTER TABLE IncomingMessageContentSent ADD CONSTRAINT IncomingMessageContentSent_Pk PRIMARY KEY(IncomingMessageContentSentID);    
    ALTER TABLE IncomingMessageContentSent ADD CONSTRAINT IncomingMessageContentSent_ServiceTypeID_Fk FOREIGN KEY(ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)
    
    
  CREATE TABLE RelatedMessagelog(
      RelatedMessagelogID bigint identity(1,1),
      NewsID bigint  NOT NULL,
      [CustomerId] [varchar](10) NOT NULL);

ALTER TABLE RelatedMessagelog ADD CONSTRAINT RelatedMessagelog_Pk PRIMARY KEY(RelatedMessagelogID)  
ALTER TABLE RelatedMessagelog ADD CONSTRAINT RelatedMessagelog_NewsID_U Unique(NewsID,CustomerId)

 CREATE TABLE ExtensionMessage(
      ExtensionMessageID bigint identity(1,1),
      Title  [nvarchar](255) NOT NULL,
      Content  [nvarchar](1024) NOT NULL,
      CreatedDate DateTime NOT NULL);

ALTER TABLE ExtensionMessage ADD CONSTRAINT ExtensionMessage_Pk PRIMARY KEY(ExtensionMessageID)

CREATE TABLE ExtensionMessageLog(
      ExtensionMessageLogID bigint identity(1,1),
      ExtensionMessageID bigint  NOT NULL,
      [CustomerId] [varchar](10) NOT NULL);

ALTER TABLE ExtensionMessageLog ADD CONSTRAINT ExtensionMessageLog_Pk PRIMARY KEY(ExtensionMessageLogID)  
ALTER TABLE ExtensionMessageLog ADD CONSTRAINT ExtensionMessageLog_ExtensionMessageID_CustID_U Unique(ExtensionMessageID,CustomerId)