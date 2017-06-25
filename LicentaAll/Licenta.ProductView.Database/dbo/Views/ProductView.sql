CREATE VIEW [dbo].[ProductView]
	AS SELECT
		p.Id,
		p.Name,
		p.Description,
		p.Price,
		IIF(p.Inventory>0,CAST(1 as bit),CAST(1 as bit)) as IsInStock,
		c.Id as CategoryId,
		c.Name as CategoryName,
		p.Rating 
	FROM [Products] p
	INNER JOIN Category c ON p.CategoryId = c.Id
