CREATE PROCEDURE [db_owner].GetDiaryEntriesByUserId
(@UserId INT)
AS
BEGIN
	SELECT * FROM [db_owner].[Diary]
	WHERE UserId = @UserId;
END