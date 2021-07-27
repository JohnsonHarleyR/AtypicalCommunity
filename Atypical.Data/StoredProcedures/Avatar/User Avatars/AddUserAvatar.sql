CREATE PROCEDURE [db_owner].AddUserAvatar
(@UserId INT, @IsCreated BIT, @Background INT, @SecondaryBackground INT, @Foreground INT,
@Base INT, @Tattoos INT, @Marks INT, @Eyes INT, @Nose INT, @Mouth INT, @Makeup INT,
@FacialHair INT, @EarRings INT, @FacePiercings INT, @Necklace INT, @LeftArm INT,
@RightArm INT, @Hair INT, @HairAccessory INT, @Hat INT, @Top INT, @FullBody INT,
@Neck INT, @Bottom INT, @Shoes INT, @LeftAccessory INT, @RightAccessory INT,
@LeftHand INT, @RightHand INT)
AS
	INSERT INTO [db_owner].[UserAvatar]
	(UserId, IsCreated, Background, SecondaryBackground, Foreground, Base, Tattoos, Marks,
	Eyes, Nose, Mouth, Makeup, FacialHair, EarRings, FacePiercings, Necklace,
	LeftArm, RightArm, Hair, HairAccessory, Hat, [Top], FullBody, Neck, Bottom,
	Shoes, LeftAccessory, RightAccessory, LeftHand, RightHand)
	VALUES
	(@UserId, @IsCreated, @Background, @SecondaryBackground, @Foreground, @Base, @Tattoos, @Marks,
	@Eyes, @Nose, @Mouth, @Makeup, @FacialHair, @EarRings, @FacePiercings, @Necklace,
	@LeftArm, @RightArm, @Hair, @HairAccessory, @Hat, @Top, @FullBody, @Neck, @Bottom,
	@Shoes, @LeftAccessory, @RightAccessory, @LeftHand, @RightHand);
GO