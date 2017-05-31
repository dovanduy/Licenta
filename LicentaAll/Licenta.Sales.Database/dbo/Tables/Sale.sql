CREATE TABLE [dbo].[Sale] (
    [SaleId]             INT            NOT NULL,
    [ProductId]          INT            NOT NULL,
    [Items]              INT            NOT NULL,
    [Price]              MONEY          NOT NULL,
    [SpecialOfferId]     INT            NULL,
    [PercentageDiscount] DECIMAL (4, 2) NULL,
    [UserId]             VARCHAR (500)  NOT NULL,
    [OrderId]            INT            NOT NULL,
    [StatusId]           INT            CONSTRAINT [DF_Sale_StatusId] DEFAULT ((1)) NOT NULL,
    [Date_Created]       DATE           CONSTRAINT [DF_Sale_Date_Created] DEFAULT (getdate()) NOT NULL,
    [Date_StatusChanged] DATE           NULL,
    CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED ([SaleId] ASC),
    CONSTRAINT [FK_Sale_SaleStatus] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[SaleStatusLookup] ([SaleStatusId])
);

