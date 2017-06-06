﻿CREATE TABLE [dbo].[Review] (
    [Id]          INT            IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [ProductId]         INT            NOT NULL,
    [UserId]            VARCHAR (100)  NOT NULL,
    [Rating]            INT        NOT NULL,
    [Text]              VARCHAR (5000) NULL,
    [UserNickname]      VARCHAR (100)  NOT NULL,
    [Date_Deleted] DATE NULL, 
	[Row_Version] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Review_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
);