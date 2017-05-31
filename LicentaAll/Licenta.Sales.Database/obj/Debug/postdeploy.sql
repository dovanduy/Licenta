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

MERGE INTO dbo.SaleStatusLookup AS Target  
USING (VALUES (1,'Projected'), 
			(2, 'Definitive'), 
			(3, 'Canceled'))  
       AS Source (SaleStatusId, Name)  
ON Target.SaleStatusId = Source.SaleStatusId  
WHEN MATCHED THEN  
	UPDATE SET Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (SaleStatusId, Name) VALUES (SaleStatusId, Name)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
GO
