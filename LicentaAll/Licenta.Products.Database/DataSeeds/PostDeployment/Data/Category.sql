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