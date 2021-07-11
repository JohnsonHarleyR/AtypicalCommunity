CREATE PROCEDURE [db_owner].GetUserByUsername
(@Username VARCHAR(45))
AS
BEGIN
	SELECT * FROM [db_owner].[User]
	WHERE Username = @Username;
END