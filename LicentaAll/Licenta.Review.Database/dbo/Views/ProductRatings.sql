CREATE VIEW dbo.ProductRatings
AS
SELECT ProductId, AVG(Cast(Rating as Float)) AS AverageRating
FROM     dbo.Review
WHERE Review.Deletion_Date IS NOT NULL
GROUP BY ProductId