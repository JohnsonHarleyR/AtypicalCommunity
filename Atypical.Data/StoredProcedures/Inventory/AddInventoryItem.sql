CREATE PROCEDURE [db_owner].AddInventoryItem
(@UserId INT, @Type INT, @ItemId INT, @Name VARCHAR(50),
@Description VARCHAR(100), @IconUrl VARCHAR(100), @Color INT)
AS
	INSERT INTO [db_owner].[Inventory]
	(UserId, [Type], ItemId, [Name], [Description], IconUrl, Color)
	VALUES
	(@UserId, @Type, @ItemId, @Name, @Description, @IconUrl, @Color)
GO