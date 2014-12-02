Create table IPProxy(
	Id int Identity(1,1) Primary Key,
	IPAddress char(16),
	IPPort int,
	StatusIP bit,
	CreateDate int
)
CREATE TABLE [dbo].[Membership](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MemberName] [nvarchar](128)  NULL,
	[Password] [nvarchar](128)  NULL,	
	[Email] [nvarchar](256) NOT NULL UNIQUE,
	[IsApproved] [bit]  NULL,
	[IsLockedOut] [bit]  NULL,
	[CreateDate] [int] NOT NULL,
	[LastLoginDate] [int]  NULL,
	[LoginCount] [int]  NULL,
	
	 
)
CREATE TABLE GLStockCode(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Code] char(3) UNIQUE,
	[Name] nvarchar(125),
	[Remark] nvarchar(256),
	[CreateDate] int NOT NULL,
	IdMenber int FOREIGN KEY REFERENCES Membership(id)
)



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[dnn_AGNews_Articles](
	[Id] [uniqueidentifier] NOT NULL,
	[ArticleId] [int]  NOT NULL,
	[Company] [nvarchar](50) NULL,
	[Title] [nvarchar](300) NULL,
	[Lead] [nvarchar](2000) NULL,
	[Content] [ntext] NULL,
	[Source] [nvarchar](100) NULL,
	[ImageFile] [nvarchar](200) NULL,
	[ImageNote] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[DisplayDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[UserId] [int] NULL,
	[IsHot] [bit] NULL,
	[HotPeriod] [int] NULL,
	[PortalId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsExpired] [bit] NULL,
	[NewsID] [int] NULL,
	[NewsCatBriefCode] [varchar](2) NULL,
	[AttachedFileLocation] [varchar](900) NULL,
	[Data] [xml] NULL,
	[Created] [datetime2](7) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Modified] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ReadCount] [int] NULL,
 
)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO