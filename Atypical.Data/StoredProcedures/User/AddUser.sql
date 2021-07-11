CREATE PROCEDURE [db_owner].AddUser
(@Username VARCHAR(45), @FirstName VARCHAR(45), 
@ProfileImageUrl VARCHAR(50), @DateOfBirth DATETIME,
@Email VARCHAR(50), @Password VARCHAR(100),
@IsEmailConfirmed BIT, @UserType INT)
AS
BEGIN
	INSERT INTO [db_owner].[User]
    (Username, FirstName, ProfileImageUrl, DateOfBirth, Email,
	[Password], IsEmailConfirmed, UserType)
    VALUES (@Username, @FirstName, @ProfileImageUrl, @DateOfBirth, 
	@Email, @Password, @IsEmailConfirmed, @UserType)
END