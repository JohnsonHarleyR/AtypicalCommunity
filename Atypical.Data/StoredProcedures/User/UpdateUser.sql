CREATE PROCEDURE [db_owner].UpdateUser
(@Username VARCHAR(45), @FirstName VARCHAR(45), 
@ProfileImageUrl VARCHAR(50), @DateOfBirth DATETIME,
@Email VARCHAR(50), @Password VARCHAR(100),
@IsEmailConfirmed BIT, @UserType INT, @Id INT)
AS
BEGIN
	UPDATE [db_owner].[User]
    SET Username = @Username, FirstName = @FirstName, ProfileImageUrl = ProfileImageUrl,
    DateOfBirth = @DateOfBirth, Email = @Email, [Password] = @Password,
    IsEmailConfirmed = @IsEmailConfirmed, UserType = @UserType 
    WHERE Id = @Id;
END