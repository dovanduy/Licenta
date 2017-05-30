MERGE INTO dbo.CategoryLookup AS Target  
USING (VALUES (1,'Electronics'), 
			(2, 'Clothing'), 
			(3, 'Furniture'))  
       AS Source (CategoryId, Name)  
ON Target.CategoryId = Source.CategoryId  
WHEN MATCHED THEN  
	UPDATE SET Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (CategoryId, Name) VALUES (CategoryId, Name)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;