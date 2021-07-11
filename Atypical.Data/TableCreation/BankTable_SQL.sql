USE [Atypical]
GO

/****** Object:  Table [db_owner].[Bank]    Script Date: 7/11/2021 3:04:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[Bank](
	[UserId] [int] NOT NULL,
	[Checking] [int] NOT NULL,
	[Savings] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [db_owner].[Bank] ADD  DEFAULT ((0)) FOR [Checking]
GO

ALTER TABLE [db_owner].[Bank] ADD  DEFAULT ((100)) FOR [Savings]
GO


