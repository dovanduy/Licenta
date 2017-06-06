CREATE TABLE [dbo].[Product] (
    [Id] INT NOT NULL,
    [Items]     INT NOT NULL,
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Date_Deleted] DATE NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

