CREATE TABLE [dbo].[Review] (
    [ReviewId]          INT            IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [ProductId]         INT            NOT NULL,
    [UserId]            VARCHAR (100)  NOT NULL,
    [Rating]            TINYINT        NOT NULL,
    [Text]              VARCHAR (5000) NULL,
    [UserBoughtProduct] BIT            NOT NULL,
    [UserNickname]      VARCHAR (100)  NOT NULL,
    [ProductDeleted]    BIT            CONSTRAINT [DF_Review_ProductDeleted] DEFAULT ((0)) NOT NULL,
    [Deletion_Date] DATE NULL, 
);

