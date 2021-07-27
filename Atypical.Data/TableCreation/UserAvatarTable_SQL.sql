USE [Atypical]
GO

/****** Object:  Table [db_owner].[UserAvatar]    Script Date: 7/22/2021 3:22:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [db_owner].[UserAvatar](
	[UserId] [int] NOT NULL,
	[IsCreated] [bit] NOT NULL,
	[Background] [int] NULL,
	[SecondaryBackground] [int] NULL,
	[Foreground] [int] NULL,
	[Base] [int] NULL,
	[Tattoos] [int] NULL,
	[Marks] [int] NULL,
	[Eyes] [int] NULL,
	[Nose] [int] NULL,
	[Mouth] [int] NULL,
	[Makeup] [int] NULL,
	[FacialHair] [int] NULL,
	[EarRings] [int] NULL,
	[FacePiercings] [int] NULL,
	[Necklace] [int] NULL,
	[LeftArm] [int] NULL,
	[RightArm] [int] NULL,
	[Hair] [int] NULL,
	[HairAccessory] [int] NULL,
	[Hat] [int] NULL,
	[Top] [int] NULL,
	[FullBody] [int] NULL,
	[Neck] [int] NULL,
	[Bottom] [int] NULL,
	[Shoes] [int] NULL,
	[LeftAccessory] [int] NULL,
	[RightAccessory] [int] NULL,
	[LeftHand] [int] NULL,
	[RightHand] [int] NULL,
 CONSTRAINT [PK_UserAvatar] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


