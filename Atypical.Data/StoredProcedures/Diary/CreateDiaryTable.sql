CREATE PROCEDURE [db_owner].CreateDiaryTable
AS
BEGIN
	SET ANSI_NULLS ON;

	SET QUOTED_IDENTIFIER ON;

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
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

	ALTER TABLE [db_owner].[Diary]  WITH CHECK ADD FOREIGN KEY([UserId])
	REFERENCES [db_owner].[User] ([Id]);
END