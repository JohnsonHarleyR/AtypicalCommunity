CREATE PROCEDURE [db_owner].GetAllBankAccounts
AS
BEGIN
	SELECT * FROM [db_owner].[Bank];
END