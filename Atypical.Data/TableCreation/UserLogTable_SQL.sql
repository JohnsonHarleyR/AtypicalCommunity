USE [Atypical]
GO

/****** Object:  Table [db_owner].[UserLog]    Script Date: 7/11/2021 3:21:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[UserLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DateAndTime] [datetime] NOT NULL,
	[ActivityCategory] [int] NOT NULL,
	[Message] [varchar](max) NOT NULL,
 CONSTRAINT [PK_UserLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [db_owner].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_User] FOREIGN KEY([UserId])
REFERENCES [db_owner].[User] ([Id])
GO

ALTER TABLE [db_owner].[UserLog] CHECK CONSTRAINT [FK_UserLog_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recording logs of user activity.' , @level0type=N'SCHEMA',@level0name=N'db_owner', @level1type=N'TABLE',@level1name=N'UserLog'
GO


