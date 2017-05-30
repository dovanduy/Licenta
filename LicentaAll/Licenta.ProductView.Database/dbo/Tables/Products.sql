CREATE TABLE [dbo].[Products] (
    [ProductId]   INT            NOT NULL PRIMARY KEY,
    [Name]        VARCHAR (400)  NOT NULL,
    [Description] VARCHAR (5000) NULL,
    [CategoryId]  INT            NOT NULL, 
    [Inventory] INT NOT NULL DEFAULT 0,
    [Price] MONEY NULL, 
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [CategoryLookup]([CategoryId])
);

