CREATE PROCEDURE [db_owner].GetInventoryItemsByType
(@UserId INT, @Type INT)
AS
	SELECT * FROM [db_owner].[Inventory]
	WHERE UserId = @UserId AND [Type] = @Type;
GO