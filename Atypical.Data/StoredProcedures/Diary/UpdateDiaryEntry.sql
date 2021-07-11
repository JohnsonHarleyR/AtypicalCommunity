CREATE PROCEDURE [db_owner].UpdateDiaryEntry
(@Id INT, @UserId INT, @DateAndTime DATETIME, 
@Happy INT, @Sad INT, @Confident INT,
@Mad INT, @Hopeful INT, @Scared INT,
@Title VARCHAR(100), @Text VARCHAR(MAX))
AS
BEGIN
	UPDATE [db_owner].Diary
    SET Title = @Title, [Text] = @Text, Happy = @Happy, Sad = @Sad,
    Confident = @Confident, Mad = @Mad, Hopeful = @Hopeful,
	Scared = @Scared WHERE Id = @Id;
END