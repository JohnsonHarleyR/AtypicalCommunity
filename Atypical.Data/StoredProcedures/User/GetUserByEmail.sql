CREATE PROCEDURE [db_owner].GetUserByEmail
(@Email VARCHAR(50))
AS
BEGIN
	SELECT * FROM [db_owner].[User]
	WHERE Email = @Email;
END