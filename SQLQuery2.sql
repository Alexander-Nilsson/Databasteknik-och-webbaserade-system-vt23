
SELECT *
FROM [NotBlocket].[dbo].[Ads]
WHERE [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + @searchstring + '%'
ORDER BY 
  CASE 
    WHEN [NotBlocket].[dbo].[Ads].[Name] = @searchstring THEN 0
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE @searchstring + '%' THEN 1
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + @searchstring + '%' THEN 2
    WHEN [NotBlocket].[dbo].[Ads].[Name] LIKE '%' + @searchstring THEN 3
    ELSE 4
  END,
  [NotBlocket].[dbo].[Ads].[Name] ASC
