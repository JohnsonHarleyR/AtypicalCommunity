CREATE PROCEDURE [db_owner].CreateUserTable
AS
BEGIN
	SET ANSI_NULLS ON;

	SET QUOTED_IDENTIFIER ON;

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
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY];

	ALTER TABLE [db_owner].[User] ADD  CONSTRAINT [DF_User_IsEmailConfirmed]  DEFAULT ((0)) FOR [IsEmailConfirmed];

	ALTER TABLE [db_owner].[User] ADD  CONSTRAINT [DF_User_UserType]  DEFAULT ((0)) FOR [UserType];
END