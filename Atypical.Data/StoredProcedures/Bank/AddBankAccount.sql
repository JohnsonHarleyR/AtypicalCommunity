CREATE PROCEDURE [db_owner].AddBankAccount
(@UserId INT, @Checking INT, @SAVINGS INT)
AS
BEGIN
	INSERT INTO [db_owner].Bank
    (UserId, Checking, Savings)
    VALUES (@UserId, @Checking, @Savings);
END