CREATE TABLE [dbo].[Reaction] (
    [Id] INT           IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [Reaction]   BIT           NULL,
    [UserId]     VARCHAR (100) NOT NULL,
    [ReviewId]   INT           NOT NULL,
    [Date_Deleted] DATE NULL, 
    CONSTRAINT [FK_Reaction_Review] FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Review] ([Id])
);

