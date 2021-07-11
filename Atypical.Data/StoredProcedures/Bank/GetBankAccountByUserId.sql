CREATE PROCEDURE [db_owner].GetBankAccountByUserId
(@UserId INT)
AS
BEGIN
	SELECT * FROM [db_owner].Bank WHERE UserId = @UserId;
END