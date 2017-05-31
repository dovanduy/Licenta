CREATE TABLE [dbo].[Review] (
    [ReviewId]          INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]         INT            NOT NULL,
    [UserId]            VARCHAR (100)  NOT NULL,
    [Rating]            TINYINT        NOT NULL,
    [Text]              VARCHAR (5000) NOT NULL,
    [UserBoughtProduct] BIT            NOT NULL,
    [UserNickname]      VARCHAR (100)  NOT NULL,
    [ProductDeleted]    BIT            CONSTRAINT [DF_Review_ProductDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED ([ReviewId] ASC)
);

