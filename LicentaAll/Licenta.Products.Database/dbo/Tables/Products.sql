﻿CREATE TABLE [dbo].[Products] (
    [Id]   INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Name]        VARCHAR (400)  NOT NULL,
    [Description] VARCHAR (5000) NULL,
    [CategoryId]  INT            NOT NULL, 
    [Row_Version] INT NOT NULL DEFAULT 1, 
    [Date_Deleted] DATE NULL, 
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES Category([Id])
);

