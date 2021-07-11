CREATE PROCEDURE [db_owner].GetDiaryEntryById
(@Id INT)
AS
BEGIN
	SELECT * FROM [db_owner].[Diary]
	WHERE Id = @Id;
END