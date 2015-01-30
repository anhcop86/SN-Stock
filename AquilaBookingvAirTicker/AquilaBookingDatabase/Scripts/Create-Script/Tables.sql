-- =============================================
-- Script Template
-- =============================================
/****** Object:  Tables  ******/

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRANSACTION

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemParameter]') AND type in (N'U'))
DROP TABLE [dbo].SystemParameter
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logging]') AND type in (N'U'))
DROP TABLE [dbo].[Logging]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompareListDetail]') AND type in (N'U'))
DROP TABLE [dbo].CompareListDetail
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompareList]') AND type in (N'U'))
DROP TABLE [dbo].CompareList
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookingDetail]') AND type in (N'U'))
DROP TABLE [dbo].BookingDetail
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Booking]') AND type in (N'U'))
DROP TABLE [dbo].Booking
GO                 

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AvailabilityHist]') AND type in (N'U'))
DROP TABLE [dbo].[AvailabilityHist]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Availability]') AND type in (N'U'))
DROP TABLE [dbo].[Availability]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HotelImage]') AND type in (N'U'))
DROP TABLE [dbo].[HotelImage]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HotelFacility]') AND type in (N'U'))
DROP TABLE [dbo].[HotelFacility]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hotel]') AND type in (N'U'))
DROP TABLE [dbo].[Hotel] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Facility]') AND type in (N'U'))
DROP TABLE [dbo].[Facility]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Membership]') AND type in (N'U'))
DROP TABLE [dbo].[Membership]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Province]') AND type in (N'U'))
DROP TABLE [dbo].[Province]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemVersion]') AND type in (N'U'))
DROP TABLE [dbo].[SystemVersion]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Membership]') AND type in (N'U'))
DROP TABLE [dbo].[Membership]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ImagesStore') AND type in (N'U'))
DROP TABLE [dbo].[ImagesStore]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Province') AND type in (N'U'))
DROP TABLE [dbo].[Province]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Country') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RoomType') AND type in (N'U'))
DROP TABLE [dbo].[RoomType]
GO

/****** Create  Tables  ******/
CREATE TABLE RoomType (
	RoomTypeId int IDENTITY(1,1) NOT NULL, 	
	Name nvarchar(127) NOT NULL,
	EnglishName nvarchar (70) NOT NULL,
	CreatedDate  nvarchar (50) NOT NULL ,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE RoomType ADD CONSTRAINT PK_RoomType PRIMARY KEY(RoomTypeId)
ALTER TABLE RoomType ADD CONSTRAINT U_RoomType_Name UNIQUE(Name)

CREATE TABLE Country (
	CountryId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(10) NOT NULL,
	EnglishName nvarchar (70) NOT NULL,
	CreatedDate nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Country ADD CONSTRAINT PK_Country PRIMARY KEY(CountryId)
ALTER TABLE Country ADD CONSTRAINT U_Country_Name UNIQUE(Name)

CREATE TABLE Province (
	ProvinceId int IDENTITY(1,1) NOT NULL,
    CountryId int, 
	Name nvarchar (70) NOT NULL,
	EnglishName nvarchar (70) NULL,
	CreatedDate nvarchar (14)  NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Province ADD CONSTRAINT PK_Province PRIMARY KEY(ProvinceId)
ALTER TABLE Province ADD CONSTRAINT U_Province_Name UNIQUE(Name)

CREATE TABLE SystemVersion (
	SysVersionId int IDENTITY(1,1) NOT NULL,
	SysVersion nvarchar(20) NOT NULL,
	VersionName nvarchar(20)  NOT NULL,
	UpdateDate smalldatetime,
	VersionDescription nvarchar(255))

ALTER TABLE SystemVersion ADD CONSTRAINT PK_SystemVersion PRIMARY KEY(SysVersionId)
ALTER TABLE SystemVersion ADD CONSTRAINT U_SystemVersion_VersionName UNIQUE(VersionName)

CREATE TABLE [Membership](	
	[MemberId] int NOT NULL,
	[MemberName] [nvarchar](128) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,	
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
	[LoginId]  [nvarchar](128) NOT NULL,
	[LoginCount] [int] NOT NULL)

GO

ALTER TABLE [Membership] ADD CONSTRAINT PK_Membership PRIMARY KEY([MemberId])
GO

ALTER TABLE Membership ADD  DEFAULT ((0)) FOR [PasswordFormat]
ALTER TABLE Membership ADD  DEFAULT ((0)) FOR [LoginCount]
ALTER TABLE Membership ADD CONSTRAINT U_Membership_Email  UNIQUE(Email)
ALTER TABLE Membership ADD CONSTRAINT U_Membership_LoginId  UNIQUE(LoginId)

CREATE TABLE ImagesStore (
	ImagesStoreId bigint NOT NULL,
	ImagePath nvarchar (512) NOT NULL,
	CreatedDate nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE ImagesStore ADD CONSTRAINT PK_ImagesStore PRIMARY KEY(ImagesStoreId)

CREATE TABLE Facility (
	FacilityId int NOT NULL,
	Name nvarchar(255) NULL,
	CreatedDate nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Facility ADD CONSTRAINT PK_Facility PRIMARY KEY(FacilityId)
ALTER TABLE Facility ADD CONSTRAINT U_Facility_Name UNIQUE(Name)


CREATE TABLE Hotel (
	HotelId int NOT NULL,
	Name varchar(255) NOT NULL,
    Start int,
	HotelAddress int  NOT NULL,
	ShortDesc nvarchar (255) NOT NULL,
	LongDesc Ntext NULL,
	Location nvarchar (512) NOT NULL,
    ProvinceId int,
    CreatedDate nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Hotel ADD CONSTRAINT PK_SubService PRIMARY KEY(HotelId)
ALTER TABLE Hotel ADD CONSTRAINT FK_Hotel_ProvinceId_Province FOREIGN KEY(ProvinceId) REFERENCES Province(ProvinceId)
ALTER TABLE Hotel ADD CONSTRAINT U_Hotel_Name UNIQUE(Name)

CREATE TABLE HotelImage (
	HotelImageID int NOT NULL,
    ImagesStoreId bigint,
    HotelId int,
    RoomTypeId int,
	SortOrder decimal(18,2) NOT NULL,
	ImagePath nvarchar (512) NOT NULL,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE HotelImage ADD CONSTRAINT PK_HotelImages PRIMARY KEY(HotelImageID)

CREATE TABLE HotelFacility (
	HotelFacilityId int IDENTITY(1,1) NOT NULL,
	HotelId int NOT NULL,
	FacilityId int NOT NULL,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE HotelFacility ADD CONSTRAINT PK_HotelFacility PRIMARY KEY(HotelFacilityId)
ALTER TABLE HotelFacility ADD CONSTRAINT FK_HotelFacility_HotelId_Hotel FOREIGN KEY(HotelId) REFERENCES Hotel(HotelId)
ALTER TABLE HotelFacility ADD CONSTRAINT FK_HotelFacility_FacilityId_Facility FOREIGN KEY(FacilityId) REFERENCES Facility(FacilityId)
ALTER TABLE HotelFacility ADD CONSTRAINT U_HotelFacility_HotelId_FacilityId UNIQUE(HotelId, FacilityId)


CREATE TABLE Availability(
	AvailabilityId int IDENTITY(1,1) NOT NULL,
	HotelId int NOT NULL,
	RoomTypeId int NOT NULL,
	FromDate nvarchar(255) NOT NULL,
	ToDate nvarchar(255) NULL,
    Quanity int,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Availability ADD CONSTRAINT PK_Availability PRIMARY KEY(AvailabilityId)
ALTER TABLE Availability ADD CONSTRAINT FK_Availability_HotelId_Hotel FOREIGN KEY(HotelId) REFERENCES Hotel(HotelId)
ALTER TABLE Availability ADD CONSTRAINT FK_Availability_RooTypeId_RoomType FOREIGN KEY(RoomTypeId) REFERENCES RoomType(RoomTypeId)

CREATE TABLE AvailabilityHist(
	AvailabilityHistId int IDENTITY(1,1) NOT NULL,
    AvailabilityId int,
	HotelId int NOT NULL,
	RoomTypeId int NOT NULL,
	FromDate nvarchar(255) NOT NULL,
	ToDate nvarchar(255) NULL,
    Quantity int,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE AvailabilityHist ADD CONSTRAINT PK_AvailabilityHist PRIMARY KEY(AvailabilityHistId)
ALTER TABLE AvailabilityHist ADD CONSTRAINT FK_AvailabilityHist_HotelId_Hotel FOREIGN KEY(HotelId) REFERENCES Hotel(HotelId)
ALTER TABLE AvailabilityHist ADD CONSTRAINT FK_AvailabilityHist_RooTypeId_RoomType FOREIGN KEY(RoomTypeId) REFERENCES RoomType(RoomTypeId)

CREATE TABLE Booking(
	BookingId int IDENTITY(1,1) NOT NULL,
	BookingDate nvarchar(14) NOT NULL,
    MemberId int,
    ViewCode nvarchar(50) NOT NULL, 
    IpAdress nvarchar(255) NOT NULL,
    BookDate nvarchar(255) NOT NULL,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Booking ADD CONSTRAINT PK_Booking PRIMARY KEY(BookingId)
ALTER TABLE Booking ADD CONSTRAINT FK_Booking_MemberId_MemberShip FOREIGN KEY(MemberId) REFERENCES Membership(MemberId)


CREATE TABLE BookingDetail (
	BookingDetailId int IDENTITY(1,1) NOT NULL,
	BookingId int  NOT NULL,
	HotelId int NOT NULL,
	RoomType nvarchar(255) NOT NULL,
	Quantity int,
	FromDate nvarchar(255) NOT NULL,
	ToDate nvarchar(255) NULL, 
	Remark nvarchar (255) NULL,
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE BookingDetail ADD CONSTRAINT PK_BookingDetail PRIMARY KEY(BookingDetailId)
ALTER TABLE BookingDetail ADD CONSTRAINT FK_BookingDetail_BookingId_Booking FOREIGN KEY (BookingId) REFERENCES Booking(BookingId)

CREATE TABLE CompareList (
	CompareListId int IDENTITY(1,1) NOT NULL,
	MemberId int NOT NULL,	
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE CompareList ADD CONSTRAINT PK_CompareList PRIMARY KEY(CompareListId)
ALTER TABLE CompareList ADD CONSTRAINT FK_CompareList_MemberId_MemberShip FOREIGN KEY(MemberId) REFERENCES Membership(MemberId)

CREATE TABLE CompareListDetail (
    CompareListDetailId int IDENTITY(1,1) NOT NULL,
	CompareListId int,
	MemberId int NOT NULL,	
	CreatedDate  nvarchar (14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE CompareListDetail ADD CONSTRAINT PK_CompareListDetail PRIMARY KEY(CompareListDetailId)
ALTER TABLE CompareListDetail ADD CONSTRAINT FK_CompareListDetail_CompareListId_CompareList FOREIGN KEY(CompareListId) REFERENCES CompareList(CompareListId) 
ALTER TABLE CompareListDetail ADD CONSTRAINT FK_CompareListDetail_MemberId_MemberShip FOREIGN KEY(MemberId) REFERENCES Membership(MemberId)

CREATE TABLE Logging (
	LoggingId uniqueidentifier NOT NULL,
	LoggingDate smalldatetime NOT NULL,
	MemberId int NOT NULL,
	LoggingDescription nvarchar (4000) NULL,
	IpAddress varchar (25) NULL,
	CreatedDate nvarchar NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE Logging ADD CONSTRAINT PK_Logging PRIMARY KEY(LoggingId)

CREATE TABLE SystemParameter (
	ParameterId uniqueidentifier NOT NULL,
	ParameterName nvarchar(200) NOT NULL,
	ParameterValue varchar(200) NOT NULL,
	ParameterType varchar(15) NULL,
	IpAddress varchar(25) NULL,
	CreatedDate nvarchar(14) NOT NULL,
    CreatedBy nvarchar (50) NOT NULL)

ALTER TABLE SystemParameter ADD CONSTRAINT PK_SystemParameter PRIMARY KEY(ParameterId)
ALTER TABLE SystemParameter ADD CONSTRAINT U_SystemParameter_Name UNIQUE(ParameterName)



IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION

INSERT INTO SystemVersion(SysVersion,VersionName,UpdateDate,VersionDescription)
VALUES('1.0','Deployment script',GETDATE(),'database scripts for Aquila Booking system')  
COMMIT TRANSACTION