CREATE PROCEDURE [db_owner].AddDiaryEntry
(@UserId INT, @DateAndTime DATETIME, 
@Happy INT, @Sad INT, @Confident INT,
@Mad INT, @Hopeful INT, @Scared INT,
@Title VARCHAR(100), @Text VARCHAR(MAX))
AS
BEGIN
	INSERT INTO [db_owner].Diary 
    (UserId, DateAndTime, Title, [Text], Happy, Sad, 
	Confident, Mad, Hopeful, Scared)
    VALUES (@UserId, @DateAndTime, @Title, @Text, @Happy, 
	@Sad, @Confident, @Mad, @Hopeful, @Scared);
END