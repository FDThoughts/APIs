USE [ToDoApp]
GO
CREATE TABLE [dbo].[Detail]
(
	[TaskId] [bigint] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Title] [varchar](200) NOT NULL,
	[Description] [varchar](500) NULL,
	[DueDate] [date] NULL,
	[Done] [bit] NOT NULL DEFAULT 0
)
GO