﻿CREATE VIEW dbo.ReviewList
AS
WITH 

PositiveReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(Id) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 1)
	GROUP BY ReviewId
), 

NegativeReactions(ReviewId, NumberOfReactions) AS (
	SELECT ReviewId, COUNT(Id) AS Expr1
	FROM      dbo.Reaction
	WHERE   (Reaction = 0)
	GROUP BY ReviewId
)
    
SELECT 
	dbo.Review.Rating,
	dbo.Review.Text, 
	dbo.Review.ProductId, 
	dbo.Review.Id AS ReviewId, 
	dbo.Review.UserNickname, 
	dbo.Review.UserBoughtProduct, 
	PositiveReactions.NumberOfReactions AS PositiveReactions, 
	NegativeReactions.NumberOfReactions AS NegativeReactions
FROM dbo.Review 
LEFT OUTER JOIN PositiveReactions ON dbo.Review.Id = PositiveReactions.ReviewId
LEFT OUTER JOIN NegativeReactions ON dbo.Review.Id = NegativeReactions.ReviewId
