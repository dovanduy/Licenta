CREATE TABLE [dbo].[Product] (
    [ProductId] INT   NOT NULL,
    [Price]     MONEY NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

