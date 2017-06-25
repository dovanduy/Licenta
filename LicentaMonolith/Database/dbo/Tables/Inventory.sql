CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Date_Deleted] DATE NULL, 
    [WarehouseNumber] INT NOT NULL, 
    [AreaCode] INT NOT NULL, 
    [ShelveNumber] INT NOT NULL, 
    CONSTRAINT [FK_Inventory_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([Id])
)
