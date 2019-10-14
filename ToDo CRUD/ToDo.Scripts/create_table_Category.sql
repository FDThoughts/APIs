USE [ToDoApp]
GO
CREATE TABLE [dbo].[Category]
(
	[CatId] [bigint] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] [varchar](200) NOT NULL
)
GO
INSERT INTO [dbo].[Category]([Name]) VALUES ('General')
GO