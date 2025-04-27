SELECT [NotBlocket].[dbo].[Locations].Name, COUNT([NotBlocket].[dbo].[Profiles].Id) AS Profile_Count
FROM [NotBlocket].[dbo].[Locations]
JOIN [NotBlocket].[dbo].[Profiles] ON [NotBlocket].[dbo].[Locations].Location_Id = [NotBlocket].[dbo].[Profiles].Location_Id
GROUP BY [NotBlocket].[dbo].[Locations].Name
