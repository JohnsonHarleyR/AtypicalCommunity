USE [Atypical]
GO

/****** Object:  Table [db_owner].[Diary]    Script Date: 7/11/2021 3:05:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[Diary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DateAndTime] [datetime] NOT NULL,
	[Happy] [int] NOT NULL,
	[Sad] [int] NOT NULL,
	[Confident] [int] NOT NULL,
	[Mad] [int] NOT NULL,
	[Hopeful] [int] NOT NULL,
	[Scared] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Text] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Diary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [db_owner].[Diary]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [db_owner].[User] ([Id])
GO


