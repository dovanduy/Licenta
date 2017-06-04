CREATE TABLE [dbo].[Reaction] (
    [ReactionId] INT           IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [Reaction]   BIT           NULL,
    [UserId]     VARCHAR (100) NOT NULL,
    [ReviewId]   INT           NOT NULL,
    CONSTRAINT [FK_Reaction_Review] FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Review] ([ReviewId])
);

