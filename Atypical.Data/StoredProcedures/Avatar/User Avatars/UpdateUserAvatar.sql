CREATE PROCEDURE [db_owner].UpdateUserAvatar
(@UserId INT, @IsCreated BIT, @Background INT, @SecondaryBackground INT, @Foreground INT,
@Base INT, @Tattoos INT, @Marks INT, @Eyes INT, @Nose INT, @Mouth INT, @Makeup INT,
@FacialHair INT, @EarRings INT, @FacePiercings INT, @Necklace INT, @LeftArm INT,
@RightArm INT, @Hair INT, @HairAccessory INT, @Hat INT, @Top INT, @FullBody INT,
@Neck INT, @Bottom INT, @Shoes INT, @LeftAccessory INT, @RightAccessory INT,
@LeftHand INT, @RightHand INT)
AS
BEGIN
	UPDATE [db_owner].UserAvatar
    SET IsCreated = @IsCreated, Background = @Background,
	SecondaryBackground = @SecondaryBackground, Foreground = @Foreground,
    Base = @Base, Tattoos = @Tattoos, Marks = @Marks, Eyes = @Eyes, Nose = @Nose,
	Mouth = @Mouth, Makeup = @Makeup, FacialHair = @FacialHair, EarRings = @EarRings,
	FacePiercings = @FacePiercings, Necklack = @Necklace, LeftArm = @LeftArm,
	RightArm = @RightArm, Hair = @Hair, HairAccessory = @HairAccessory, Hat = @Hat,
	[Top] = @Top, FullBody = @FullBody, Neck = @Neck, Bottom = @Bottom, Shoes = @Shoes,
	LeftAccessory = @LeftAccessory, RightAccessory = @RightAccessory,
	LeftHand = @LeftHand, RightHand = @RightHand
	WHERE UserId = @UserId;
END