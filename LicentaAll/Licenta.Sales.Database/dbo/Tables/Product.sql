CREATE TABLE [dbo].[Product] (
    [Id] INT   NOT NULL PRIMARY KEY,
    [Price]     MONEY NOT NULL, 
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Date_Deleted] DATE NULL
);

