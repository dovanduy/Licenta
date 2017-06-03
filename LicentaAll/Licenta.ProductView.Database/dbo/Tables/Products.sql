﻿CREATE TABLE [dbo].[Products] (
    [ProductId]   INT            NOT NULL PRIMARY KEY,
    [Name]        VARCHAR (400)  NOT NULL,
    [Description] VARCHAR (5000) NULL,
    [CategoryId]  INT            NOT NULL, 
    [Inventory] INT NOT NULL DEFAULT 0,
    [Price] MONEY NULL, 
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Rating] DECIMAL(4, 2) NULL, 
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([CategoryId])
);

