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
PRINT N'Dropping [dbo].[ProductRatings].[MS_DiagramPane1]...';


GO
EXECUTE sp_dropextendedproperty @name = N'MS_DiagramPane1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProductRatings';


GO
PRINT N'Dropping [dbo].[ProductRatings].[MS_DiagramPaneCount]...';


GO
EXECUTE sp_dropextendedproperty @name = N'MS_DiagramPaneCount', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProductRatings';


GO
PRINT N'Dropping [dbo].[ReviewList].[MS_DiagramPane1]...';


GO
EXECUTE sp_dropextendedproperty @name = N'MS_DiagramPane1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ReviewList';


GO
PRINT N'Dropping [dbo].[ReviewList].[MS_DiagramPaneCount]...';


GO
EXECUTE sp_dropextendedproperty @name = N'MS_DiagramPaneCount', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ReviewList';


GO
PRINT N'Altering [dbo].[Review]...';


GO
ALTER TABLE [dbo].[Review] ALTER COLUMN [Text] VARCHAR (5000) NULL;


GO
PRINT N'Altering [dbo].[ProductRatings]...';


GO
ALTER VIEW dbo.ProductRatings
AS
SELECT ProductId, AVG(Cast(Rating as Float)) AS AverageRating
FROM     dbo.Review
GROUP BY ProductId
GO
PRINT N'Altering [dbo].[ReviewList]...';


GO
ALTER VIEW dbo.ReviewList
AS
WITH 

PositiveReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(ReactionId) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 1)
	GROUP BY ReviewId
), 

NegativeReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(ReactionId) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 0)
	GROUP BY ReviewId
)
    
SELECT 
	dbo.Review.Rating,
	dbo.Review.Text, 
	dbo.Review.ProductId, 
	dbo.Review.ReviewId, 
	dbo.Review.UserNickname, 
	dbo.Review.UserBoughtProduct, 
	PositiveReactions.NumberOfReactions AS PositiveReactions, 
	NegativeReactions.NumberOfReactions AS NegativeReactions
FROM dbo.Review 
LEFT OUTER JOIN PositiveReactions ON dbo.Review.ReviewId = PositiveReactions.ReviewId 
LEFT OUTER JOIN NegativeReactions ON dbo.Review.ReviewId = NegativeReactions.ReviewId
GO
PRINT N'Update complete.';


GO
