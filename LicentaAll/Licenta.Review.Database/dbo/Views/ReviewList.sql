CREATE VIEW dbo.ReviewList
AS
WITH PositiveReactions(ReviewId, NumberOfReactions) AS (SELECT ReviewId, COUNT(ReactionId) AS Expr1
                                                                                                                       FROM      dbo.Reaction
                                                                                                                       WHERE   (Reaction = 1)
                                                                                                                       GROUP BY ReviewId), NegativeReactions(ReviewId, NumberOfReactions) AS
    (SELECT ReviewId, COUNT(ReactionId) AS Expr1
     FROM      dbo.Reaction AS Reaction_1
     WHERE   (Reaction = 0)
     GROUP BY ReviewId)
    SELECT dbo.Review.Rating, dbo.Review.Text, dbo.Review.ProductId, dbo.Review.ReviewId, dbo.Review.UserNickname, dbo.Review.UserBoughtProduct, PositiveReactions_1.NumberOfReactions AS PositiveReactions, 
                      NegativeReactions_1.NumberOfReactions AS NegativeReactions
    FROM     dbo.Review LEFT OUTER JOIN
                      PositiveReactions AS PositiveReactions_1 ON dbo.Review.ReviewId = PositiveReactions_1.ReviewId LEFT OUTER JOIN
                      NegativeReactions AS NegativeReactions_1 ON dbo.Review.ReviewId = NegativeReactions_1.ReviewId

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Review"
            Begin Extent = 
               Top = 44
               Left = 52
               Bottom = 207
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PositiveReactions_1"
            Begin Extent = 
               Top = 7
               Left = 323
               Bottom = 126
               Right = 552
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NegativeReactions_1"
            Begin Extent = 
               Top = 7
               Left = 600
               Bottom = 126
               Right = 829
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1356
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ReviewList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ReviewList';

