CREATE TABLE [dbo].[AditionalDetails]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Text] VARCHAR(5000) NOT NULL, 
    [Date_Deleted] DATE NULL, 
    [Row_Version] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_AditionalDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
