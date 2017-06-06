/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT dbo.Category ON
GO

MERGE INTO dbo.Category AS Target  
USING (VALUES (0,'Uncategorised',0))
       AS Source (Id, Name, Visible)  
ON Target.Id = Source.Id  
WHEN MATCHED THEN  
	UPDATE SET 
			Name = Source.Name, 
			Visible = Source.Visible 
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Name, Visible) VALUES (Id, Name, Visible);
GO

SET IDENTITY_INSERT dbo.Category OFF
GO
