CREATE PROCEDURE [db_owner].GetAllUsers
AS
BEGIN
	SELECT * FROM [db_owner].[User];
END