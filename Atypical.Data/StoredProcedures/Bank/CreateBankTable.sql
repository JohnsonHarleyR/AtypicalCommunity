CREATE PROCEDURE [db_owner].CreateBankTable
AS
BEGIN
	CREATE TABLE [db_owner].[Bank] (
    UserId   INTEGER PRIMARY KEY,
    Checking INT     DEFAULT (0) 
                     NOT NULL,
    Savings  INT     NOT NULL
                     DEFAULT (100) 
);
END