CREATE PROCEDURE [db_owner].GetUserAvatarById
(@UserId INT)
AS
	SELECT * FROM [db_owner].UserAvatar
	WHERE UserId = @UserId;
GO