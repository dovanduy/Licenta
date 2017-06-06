MERGE INTO dbo.SaleStatusLookup AS Target  
USING (VALUES (1,'Projected'), 
			(2, 'Definitive'), 
			(3, 'Canceled'))  
       AS Source (Id, Name)  
ON Target.Id = Source.Id  
WHEN MATCHED THEN  
	UPDATE SET Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Name) VALUES (Id, Name)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;