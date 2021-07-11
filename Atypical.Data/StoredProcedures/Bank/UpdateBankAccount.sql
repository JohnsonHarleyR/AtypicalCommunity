CREATE PROCEDURE [db_owner].UpdateBankAccount
(@UserId INT, @Checking INT, @SAVINGS INT)
AS
BEGIN
	UPDATE [db_owner].Bank
    SET Checking = @Checking, Savings = @Savings
    WHERE UserId = @UserId;
END