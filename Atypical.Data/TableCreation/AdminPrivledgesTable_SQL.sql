USE [Atypical]
GO

/****** Object:  Table [db_owner].[AdminPrivledges]    Script Date: 7/11/2021 3:02:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[AdminPrivledges](
	[UserId] [int] NOT NULL,
	[Executive] [bit] NOT NULL,
	[UserAccount] [bit] NOT NULL,
	[Forum] [bit] NOT NULL,
 CONSTRAINT [PK_AdminPrivledges] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [db_owner].[AdminPrivledges] ADD  CONSTRAINT [DF_AdminPrivledges_Executive]  DEFAULT ((0)) FOR [Executive]
GO

ALTER TABLE [db_owner].[AdminPrivledges] ADD  CONSTRAINT [DF_AdminPrivledges_UserAccount]  DEFAULT ((0)) FOR [UserAccount]
GO

ALTER TABLE [db_owner].[AdminPrivledges] ADD  CONSTRAINT [DF_AdminPrivledges_Forum]  DEFAULT ((0)) FOR [Forum]
GO

ALTER TABLE [db_owner].[AdminPrivledges]  WITH CHECK ADD  CONSTRAINT [FK_AdminPrivledges_User] FOREIGN KEY([UserId])
REFERENCES [db_owner].[User] ([Id])
GO

ALTER TABLE [db_owner].[AdminPrivledges] CHECK CONSTRAINT [FK_AdminPrivledges_User]
GO


