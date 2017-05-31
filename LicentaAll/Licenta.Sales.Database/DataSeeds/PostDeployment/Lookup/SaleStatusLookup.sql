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