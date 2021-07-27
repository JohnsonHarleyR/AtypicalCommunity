CREATE PROCEDURE [db_owner].AddAvatarItem
(@Category INT, @SubCategory INT, @Name VARCHAR(50), @Description VARCHAR(100),
@Url VARCHAR(100) ,@IconUrl VARCHAR(100), @Color INT, @Gender INT, @Tags VARCHAR(100))
AS
	INSERT INTO [db_owner].[AvatarItem]
	(Category, SubCategory, [Name], [Description], [Url], IconUrl, Color, Gender, Tags)
	VALUES
	(@Category, @SubCategory, @Name, @Description, @Url, @IconUrl, @Color, @Gender, @Tags);
GO