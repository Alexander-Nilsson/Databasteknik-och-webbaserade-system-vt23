
SELECT *
FROM [NotBlocket].[dbo].[Ads]
WHERE [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + 'Ipad' + '%'
ORDER BY 
  CASE 
    WHEN [NotBlocket].[dbo].[Ads].[Name] = 'Ipad' THEN 0
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE 'Ipad' + '%' THEN 1
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + 'Ipad' + '%' THEN 2
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + 'Ipad' THEN 3
    ELSE 4
  END,
  [NotBlocket].[dbo].[Ads].[Id] Desc
