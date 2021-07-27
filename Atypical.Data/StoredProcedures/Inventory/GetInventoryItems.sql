CREATE PROCEDURE [db_owner].GetInventoryItems
(@UserId INT)
AS
	SELECT * FROM [db_owner].[Inventory]
	WHERE UserId = @UserId;
GO