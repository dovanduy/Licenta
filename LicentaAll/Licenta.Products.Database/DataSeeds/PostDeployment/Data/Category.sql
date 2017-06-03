SET IDENTITY_INSERT dbo.Category ON
GO

MERGE INTO dbo.Category AS Target  
USING (VALUES (0,'Uncategorised',0))
       AS Source (CategoryId, Name, Visible)  
ON Target.CategoryId = Source.CategoryId  
WHEN MATCHED THEN  
	UPDATE SET 
			Name = Source.Name, 
			Visible = Source.Visible 
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (CategoryId, Name, Visible) VALUES (CategoryId, Name, Visible);
GO

SET IDENTITY_INSERT dbo.Category OFF
GO