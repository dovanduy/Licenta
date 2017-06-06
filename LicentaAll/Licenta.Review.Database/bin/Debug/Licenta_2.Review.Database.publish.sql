﻿/*
Deployment script for LicentaReview

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "LicentaReview"
:setvar DefaultFilePrefix "LicentaReview"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key 016b38c1-d1c0-4a63-99a4-3e5f7c76db78 is skipped, element [dbo].[Review].[Deletion_Date] (SqlSimpleColumn) will not be renamed to Date_Deleted';


GO
PRINT N'Rename refactoring operation with key 4e048813-b93a-4453-bf16-e471251442fc is skipped, element [dbo].[Review].[ReviewId] (SqlSimpleColumn) will not be renamed to Id';


GO
PRINT N'Rename refactoring operation with key 3a4f8788-5490-4ba2-b1fd-8c08f956f674 is skipped, element [dbo].[Reaction].[ReactionId] (SqlSimpleColumn) will not be renamed to Id';


GO
PRINT N'Creating [dbo].[Reaction]...';


GO
CREATE TABLE [dbo].[Reaction] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Reaction]     BIT           NULL,
    [UserId]       VARCHAR (100) NOT NULL,
    [ReviewId]     INT           NOT NULL,
    [Date_Deleted] DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Review]...';


GO
CREATE TABLE [dbo].[Review] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]         INT            NOT NULL,
    [UserId]            VARCHAR (100)  NOT NULL,
    [Rating]            TINYINT        NOT NULL,
    [Text]              VARCHAR (5000) NULL,
    [UserBoughtProduct] BIT            NOT NULL,
    [UserNickname]      VARCHAR (100)  NOT NULL,
    [ProductDeleted]    BIT            NOT NULL,
    [Date_Deleted]      DATE           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[DF_Review_ProductDeleted]...';


GO
ALTER TABLE [dbo].[Review]
    ADD CONSTRAINT [DF_Review_ProductDeleted] DEFAULT ((0)) FOR [ProductDeleted];


GO
PRINT N'Creating [dbo].[FK_Reaction_Review]...';


GO
ALTER TABLE [dbo].[Reaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Reaction_Review] FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Review] ([Id]);


GO
PRINT N'Creating [dbo].[ProductRatings]...';


GO
CREATE VIEW dbo.ProductRatings
AS
SELECT ProductId, AVG(Cast(Rating as Float)) AS AverageRating
FROM     dbo.Review
WHERE Review.Date_Deleted IS NOT NULL
GROUP BY ProductId
GO
PRINT N'Creating [dbo].[ReviewList]...';


GO
CREATE VIEW dbo.ReviewList
AS
WITH 

PositiveReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(Id) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 1)
	GROUP BY ReviewId
), 

NegativeReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(Id) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 0)
	GROUP BY ReviewId
)
    
SELECT 
	dbo.Review.Rating,
	dbo.Review.Text, 
	dbo.Review.ProductId, 
	dbo.Review.Id AS ReviewId, 
	dbo.Review.UserNickname, 
	dbo.Review.UserBoughtProduct, 
	PositiveReactions.NumberOfReactions AS PositiveReactions, 
	NegativeReactions.NumberOfReactions AS NegativeReactions
FROM dbo.Review 
LEFT OUTER JOIN PositiveReactions ON dbo.Review.Id = PositiveReactions.ReviewId
LEFT OUTER JOIN NegativeReactions ON dbo.Review.Id = NegativeReactions.ReviewId
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '016b38c1-d1c0-4a63-99a4-3e5f7c76db78')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('016b38c1-d1c0-4a63-99a4-3e5f7c76db78')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4e048813-b93a-4453-bf16-e471251442fc')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4e048813-b93a-4453-bf16-e471251442fc')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '3a4f8788-5490-4ba2-b1fd-8c08f956f674')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('3a4f8788-5490-4ba2-b1fd-8c08f956f674')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Reaction] WITH CHECK CHECK CONSTRAINT [FK_Reaction_Review];


GO
PRINT N'Update complete.';


GO
