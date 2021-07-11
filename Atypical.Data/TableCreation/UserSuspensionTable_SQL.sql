USE [Atypical]
GO

/****** Object:  Table [db_owner].[UserSuspension]    Script Date: 7/11/2021 3:05:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[UserSuspension](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SuspensionDate] [datetime] NOT NULL,
	[ResumeDate] [datetime] NOT NULL,
	[AdminId] [int] NOT NULL,
 CONSTRAINT [PK_UserSuspension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [db_owner].[UserSuspension]  WITH CHECK ADD  CONSTRAINT [FK_UserSuspension_User] FOREIGN KEY([UserId])
REFERENCES [db_owner].[User] ([Id])
GO

ALTER TABLE [db_owner].[UserSuspension] CHECK CONSTRAINT [FK_UserSuspension_User]
GO

ALTER TABLE [db_owner].[UserSuspension]  WITH CHECK ADD  CONSTRAINT [FK_UserSuspension_User1] FOREIGN KEY([AdminId])
REFERENCES [db_owner].[User] ([Id])
GO

ALTER TABLE [db_owner].[UserSuspension] CHECK CONSTRAINT [FK_UserSuspension_User1]
GO


