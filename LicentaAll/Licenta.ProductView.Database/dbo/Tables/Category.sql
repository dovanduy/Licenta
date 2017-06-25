CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Visible] BIT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Row_Version] INT NOT NULL DEFAULT 0, 
    [Date_Deleted] DATE NULL
)
