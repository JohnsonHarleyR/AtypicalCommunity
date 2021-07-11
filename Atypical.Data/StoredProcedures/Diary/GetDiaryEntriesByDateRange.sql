CREATE PROCEDURE [db_owner].GetDiaryEntriesByDateRange
(@UserId INT, @Date DATETIME, @NextDate DATETIME)
AS
BEGIN
	SELECT * FROM [db_owner].[Diary] 
	WHERE (DateAndTime BETWEEN @Date AND @NextDate) AND UserId = @UserId;
END