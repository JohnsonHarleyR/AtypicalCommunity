CREATE PROCEDURE [db_owner].GetAllDiaryEntries
AS
BEGIN
	SELECT * FROM [db_owner].Diary;
END