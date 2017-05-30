﻿CREATE VIEW [dbo].[ProductView]
	AS SELECT
		p.ProductId,
		p.Name,
		p.Description,
		p.Price,
		IIF(p.Inventory>0,CAST(1 as bit),CAST(1 as bit)) as IsInStock,
		cl.Name as CategoryName 
	FROM [Products] p
	INNER JOIN CategoryLookup cl ON p.CategoryId = cl.CategoryId