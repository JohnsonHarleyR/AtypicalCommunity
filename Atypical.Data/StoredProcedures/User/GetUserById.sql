CREATE PROCEDURE [db_owner].GetUserById
(@Id INT)
AS
BEGIN
	SELECT * FROM [db_owner].[User]
	WHERE Id = @Id;
END