CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
	[Visible] bit NOT NULL DEFAULT 0,
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Date_Deleted] DATE NULL
)
