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
PRINT N'Altering [dbo].[Reaction]...';


GO
ALTER TABLE [dbo].[Reaction] ALTER COLUMN [Reaction] BIT NULL;


GO
PRINT N'Altering [dbo].[Review]...';


GO
ALTER TABLE [dbo].[Review]
    ADD [Deletion_Date] DATE NULL;


GO
PRINT N'Refreshing [dbo].[ReviewList]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ReviewList]';


GO
PRINT N'Altering [dbo].[ProductRatings]...';


GO
ALTER VIEW dbo.ProductRatings
AS
SELECT ProductId, AVG(Cast(Rating as Float)) AS AverageRating
FROM     dbo.Review
WHERE Review.Deletion_Date IS NOT NULL
GROUP BY ProductId
GO
PRINT N'Update complete.';


GO