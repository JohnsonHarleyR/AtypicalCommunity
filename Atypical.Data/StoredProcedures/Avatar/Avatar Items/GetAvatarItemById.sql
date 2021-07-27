CREATE PROCEDURE [db_owner].GetAvatarItemById
(@Id INT)
AS
	SELECT * FROM [db_owner].AvatarItem
	WHERE Id = @Id;
GO