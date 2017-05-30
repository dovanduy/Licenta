CREATE TABLE [dbo].[AditionalDetails]
(
	[AditionalDetailId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Text] VARCHAR(5000) NOT NULL, 
    CONSTRAINT [FK_AditionalDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
)
