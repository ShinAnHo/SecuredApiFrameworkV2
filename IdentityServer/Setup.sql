USE [IdentityServer]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [varchar](15) PRIMARY KEY NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[SaltText] [varchar](100) NOT NULL,
	[PasswordExpiredDate] [datetime] NOT NULL,
	[ShortName] [varchar](20) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](100) NOT NULL,
	[PasswordErrorCounter] [smallint] NOT NULL,
	[IsResetPassword] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO
INSERT INTO [User] (UserId, UserName, Password, SaltText, PasswordExpiredDate, ShortName, FullName, Email, Phone, PasswordErrorCounter, IsResetPassword, IsLocked, IsActive, CreatedBy, CreatedDate)
VALUES ('admin-GBVHH', 'admin', 'PaslEAbYcKf3gtiHE7Yq2bkDEp15LgvNwZISOD2KqvFrqxY9j2SBAJBnevEueQGp', 'TB31J+U1PVhpOHNSYOqi4RpjwsZsIYCDTbGaQghLx8sxMC62iwXWb3XmymwB3Y1n23VmVP1lAch0jR3XNdZNtA==', '2021-09-08 09:53:27.033', 'admin', 'Kevin Kohar', 'kevin.kohar@int.mizuho-cb.com', '-', 0, 0, 0, 1, 'admin-GBVHH', '2021-03-08 09:53:27.033')
INSERT INTO [User] (UserId, UserName, Password, SaltText, PasswordExpiredDate, ShortName, FullName, Email, Phone, PasswordErrorCounter, IsResetPassword, IsLocked, IsActive, CreatedBy, CreatedDate)
VALUES ('reader-KJF69', 'reader', 'mAoaqWMUSHXXagO2Kj+WfzZ4ksAKKfRw+F8gUCq3f7IjQzR3Mycb84jxvIBH17Lr', 'RA7veTWVf97Hu9wZVi6HzgqfTrOsGy3Om/CyXQgFg+llESOkbzShgtTEX4OwZI84BeLWSsVzrYopWU1TuxZGbA==', '2021-09-08 09:53:27.033', 'reader', 'Reader', '-', '-', 0, 0, 0, 1, 'admin-GBVHH', '2021-03-08 09:53:27.033')
INSERT INTO [User] (UserId, UserName, Password, SaltText, PasswordExpiredDate, ShortName, FullName, Email, Phone, PasswordErrorCounter, IsResetPassword, IsLocked, IsActive, CreatedBy, CreatedDate)
VALUES ('writerA-4FKQO', 'writerA', 'XvsDWEgizpMOaEa/9gu5NfQPH0pkehEm/fU/mBmyKCEaasiasNHDRUCcdqaQorsm', 'TZRIcgKrceBRgQBYiWDAdcyuVhyvoF2LG0bW2XL8dAjcIqqUvZJ+xQKk9MiP5L5s3eDo0QW6khsS8BXvLGQXhg==', '2021-09-08 09:53:27.033', 'writerA', 'Writer A', '-', '-', 0, 0, 0, 1, 'admin-GBVHH', '2021-03-08 09:53:27.033')
INSERT INTO [User] (UserId, UserName, Password, SaltText, PasswordExpiredDate, ShortName, FullName, Email, Phone, PasswordErrorCounter, IsResetPassword, IsLocked, IsActive, CreatedBy, CreatedDate)
VALUES ('writerB-JG3K6', 'writerB', 'n7E9hIAxsNXsnVRmkmaQO8E2tc3noGLcmo6tr0I1662wIAJiPiPgUHUL3q7a3ZGn', '869e06TojwaroGfgZD0J/miO6CzzMUmtgtbKlGDMieyrECLE9yZhHPZPI0TEs6gJS5wx5SFaN3HeyEXUwuM18g==', '2021-09-08 09:53:27.033', 'writerB', 'Writer B', '-', '-', 0, 0, 0, 1, 'admin-GBVHH', '2021-03-08 09:53:27.033')
INSERT INTO [User] (UserId, UserName, Password, SaltText, PasswordExpiredDate, ShortName, FullName, Email, Phone, PasswordErrorCounter, IsResetPassword, IsLocked, IsActive, CreatedBy, CreatedDate)
VALUES ('spv-CIFVW', 'spv', 'aQIuyCqc5Vd9ZED+qwdmJaMhSM5kw+4Ep9Qdoal7vyHzrrXfFImcDv1pdybi5Rl8', 'CEtBENriuQRz8raUm0lcQ5BGAnMxWk0+tBuJ4LKhSlpuww5WB5bHu6Lx1vUtXjzheDD6Qa02XfqaqAmmdMg+Vg==', '2021-09-08 09:53:27.033', 'supervisor', 'Supervisor', '-', '-', 0, 0, 0, 1, 'admin-GBVHH', '2021-03-08 09:53:27.033')
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[UserId] [varchar](15) FOREIGN KEY REFERENCES [User] (UserID) NOT NULL,
	[ClaimType] [varchar](100) NOT NULL,
	[ClaimValue] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
    CONSTRAINT UC_UserClaim UNIQUE ([UserId],[ClaimType])
)
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('admin-GBVHH', 'user.admin', '1', 1, 'admin', @NowDate)
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('admin-GBVHH', 'user.role', 'admin', 1, 'admin', @NowDate)
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('reader-KJF69', 'user.role', 'reader', 1, 'admin', @NowDate)
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('writerA-4FKQO', 'user.role', 'writer', 1, 'admin', @NowDate)
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('writerB-JG3K6', 'user.role', 'writer', 1, 'admin', @NowDate)
INSERT INTO [dbo].[UserClaim] ([UserId], [ClaimType], [ClaimValue], [IsActive], [CreatedBy], [CreatedDate]) VALUES ('spv-CIFVW', 'user.role', 'supervisor', 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[GlobalParameter](
	[ParameterID] [varchar](50) PRIMARY KEY NOT NULL,
	[Value] [varchar](255) NOT NULL,
	[Description] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('IdentityAddress', 'https://localhost:44310', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ClientId', 'sampleClient', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ClientSecret', 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', '', 1, 'admin', @NowDate)
INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ConsulAddress', 'http://localhost:8500', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceId', '1', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceName', 'IdentityServer', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceDisplayName', 'Identity Server', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceUrl', 'https://localhost:44300', '', 1, 'admin', @NowDate) 
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceAddress', 'localhost', '', 1, 'admin', @NowDate) 
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourcePort', '44300', '', 1, 'admin', @NowDate) 
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ResourceVersion', '1.0.0', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ConsulHost', 'localhost', '', 1, 'admin', @NowDate)
--INSERT INTO GlobalParameter ([ParameterID], [Value], [Description], [IsActive], [CreatedBy], [CreatedDate]) values ('ConsulPort', '8500', '', 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ApiResource](
	[ResourceId] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[DownstreamPathTemplate] [nvarchar](200) NULL,
	[DownstreamScheme] [nvarchar](10) NULL,
	[UpstreamPathTemplate] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
    CONSTRAINT UC_ApiResource UNIQUE ([Name])
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [dbo].[ApiResource] ([Name], DisplayName, [Url], [Description], IsActive, CreatedBy, CreatedDate) VALUES ('IdentityServer', 'Identity Server', 'https://localhost:44300', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResource] ([Name], DisplayName, [Url], [Description], IsActive, CreatedBy, CreatedDate) VALUES ('SampleNewsApi', 'Sample News API', 'https://localhost:44301', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResource] ([Name], DisplayName, [Url], [Description], IsActive, CreatedBy, CreatedDate) VALUES ('SampleCountryApi', 'Sample Country API', 'https://localhost:44302', '', 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ApiScope](
	[ScopeId] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('identityserver.read', 'Read Access to Identity Server', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('identityserver.write', 'Write Access to Identity Server', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('samplenewsapi.read', 'Read Access to Sample News API', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('samplenewsapi.write', 'Write Access to Sample News API', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('samplecountryapi.read', 'Read Access to Sample Country API', '', 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiScope] ([Name], DisplayName, [Description], IsActive, CreatedBy, CreatedDate) VALUES ('samplecountryapi.write', 'Write Access to Sample Country API', '', 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ApiResourceScope](
	[Id] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ResourceId] [bigint] FOREIGN KEY REFERENCES [ApiResource]([ResourceId]) NOT NULL,
	[ScopeId] [bigint] FOREIGN KEY REFERENCES [ApiScope]([ScopeId]) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
    CONSTRAINT UC_ApiResourceScope UNIQUE ([ResourceId],[ScopeId])
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (1, 1, 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (1, 2, 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (2, 3, 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (2, 4, 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (3, 5, 1, 'admin', @NowDate)
INSERT INTO [dbo].[ApiResourceScope] ([ResourceId], [ScopeId], IsActive, CreatedBy, CreatedDate) VALUES (3, 6, 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ClientType] (
	[Type] [varchar](20) PRIMARY KEY NOT NULL,
)
GO
INSERT INTO [ClientType] VALUES ('confidential')
INSERT INTO [ClientType] VALUES ('public')
GO
CREATE TABLE [dbo].[Client] (
	[ClientId] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](200)NOT NULL,
	[ClientType] [varchar](20) FOREIGN KEY REFERENCES [ClientType]([Type]) NOT NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[ClientUri] [nvarchar](2000) NULL,
	[LogoUri] [nvarchar](2000) NULL,
	[RedirectUri] [nvarchar](2000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
    CONSTRAINT UC_Client UNIQUE ([ClientId])
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [Client] ([Name], [ClientType], [DisplayName], [Description], [ClientUri], [LogoUri], [IsActive], [CreatedBy], [CreatedDate])
VALUES ('swaggerClient', 'public', 'Sample Swagger Client', NULL, '', NULL, 1, 'admin', @NowDate)
INSERT INTO [Client] ([Name], [ClientType], [DisplayName], [Description], [ClientUri], [LogoUri], [IsActive], [CreatedBy], [CreatedDate])
VALUES ('sampleClient', 'public', 'Sample Web Client', NULL, 'https://localhost:44320', NULL, 1, 'admin', @NowDate)
INSERT INTO [Client] ([Name], [ClientType], [DisplayName], [Description], [ClientUri], [LogoUri], [IsActive], [CreatedBy], [CreatedDate])
VALUES ('sampleClientWithCountry', 'public', 'Sample Web Client', NULL, 'https://localhost:44320', NULL, 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ClientSecret](
	[ClientId] [bigint] PRIMARY KEY FOREIGN KEY REFERENCES [Client]([ClientId]) NOT NULL,
	[Value] [nvarchar](4000) NOT NULL,
	[Expiration] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [ClientSecret] ([ClientId], [Value], [Expiration], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', '2022-01-01', 1, 'admin', @NowDate)
INSERT INTO [ClientSecret] ([ClientId], [Value], [Expiration], [IsActive], [CreatedBy], [CreatedDate])
VALUES (2, 'OaEa/9gu5NfQPH0pkehEm/fU/mBmyKCEaasiasNHDRU=', '2022-01-01', 1, 'admin', @NowDate)
INSERT INTO [ClientSecret] ([ClientId], [Value], [Expiration], [IsActive], [CreatedBy], [CreatedDate])
VALUES (3, 'OaEa/9gu5NfQPH0pkehEm/fU/mBmyKCEaasiasNHDRU=', '2022-01-01', 1, 'admin', @NowDate)
GO
CREATE TABLE [dbo].[ClientScope](
	[Id] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ClientId] [bigint] FOREIGN KEY REFERENCES [Client]([ClientId]) NOT NULL,
	[ScopeId] [bigint] FOREIGN KEY REFERENCES [ApiScope]([ScopeId]) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](15) NULL,
	[ModifiedDate] [datetime] NULL,
) ON [PRIMARY]
GO
DECLARE @NowDate DATETIME = GETDATE()
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 1, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 2, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 3, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 4, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 5, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (1, 6, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (2, 3, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (2, 4, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (3, 3, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (3, 4, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (3, 5, 1, 'admin', @NowDate)
INSERT INTO [ClientScope] ([ClientId], [ScopeId], [IsActive], [CreatedBy], [CreatedDate])
VALUES (3, 6, 1, 'admin', @NowDate)
GO
CREATE TABLE AuthorizationCode (
	Code VARCHAR(100) PRIMARY KEY NOT NULL,
	Expire DATETIME NOT NULL,
	UserId VARCHAR(15) NOT NULL
)
GO
CREATE TABLE [dbo].[RefreshToken](
	[Id] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Token] [varchar](100) NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[UserId] [varchar](15) NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
	[Revoked] [bit] NOT NULL,
    CONSTRAINT UC_RefreshToken UNIQUE ([Token])
) ON [PRIMARY]
GO
