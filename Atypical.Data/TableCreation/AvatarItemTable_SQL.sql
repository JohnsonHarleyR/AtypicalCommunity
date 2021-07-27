USE [Atypical]
GO

/****** Object:  Table [db_owner].[AvatarItem]    Script Date: 7/22/2021 2:11:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[AvatarItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NOT NULL,
	[SubCategory] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](100) NULL,
	[Url] [varchar](100) NOT NULL,
	[IconUrl] [varchar](100) NOT NULL,
	[Color] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[Tags] [varchar](100) NULL,
 CONSTRAINT [PK_AvatarItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


