USE [Atypical]
GO

/****** Object:  Table [db_owner].[User]    Script Date: 7/11/2021 3:24:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](45) NOT NULL,
	[FirstName] [varchar](45) NOT NULL,
	[ProfileImageUrl] [varchar](50) NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[IsEmailConfirmed] [bit] NOT NULL,
	[UserType] [int] NOT NULL,
	[AccountStatus] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [db_owner].[User] ADD  CONSTRAINT [DF_User_IsEmailConfirmed]  DEFAULT ((0)) FOR [IsEmailConfirmed]
GO

ALTER TABLE [db_owner].[User] ADD  CONSTRAINT [DF_User_UserType]  DEFAULT ((0)) FOR [UserType]
GO

ALTER TABLE [db_owner].[User] ADD  CONSTRAINT [DF_User_AccountStatus]  DEFAULT ((0)) FOR [AccountStatus]
GO


