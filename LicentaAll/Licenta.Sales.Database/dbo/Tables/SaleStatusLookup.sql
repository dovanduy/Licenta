CREATE TABLE [dbo].[SaleStatusLookup] (
    [SaleStatusId] INT          NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SaleStatus] PRIMARY KEY CLUSTERED ([SaleStatusId] ASC)
);

